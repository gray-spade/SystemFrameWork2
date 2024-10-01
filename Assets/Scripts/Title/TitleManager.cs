using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Animation LogoAnim;
    public TextMeshProUGUI LogoTxt;

    public GameObject title;
    public TextMeshProUGUI LoadingProgressTxt;
    AsyncOperation operation;
    public Slider slider;

    private void Awake()
    {
        LogoAnim.gameObject.SetActive(true);
        title.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        UserDataManager.Instance.LoadUserData();

        if (!UserDataManager.Instance.ExistsSaveData) {
            UserDataManager.Instance.SetDefaultUserData();
            UserDataManager.Instance.SaveUserData();
        }
        Logger.Log(DataTableManager.Instance.name);
        /*
        var confirmUIData = new ConfirmUIData();
        confirmUIData.ConfirmType = ConfirmType.OK;
        confirmUIData.TitleTxt = "UI test";
        confirmUIData.DescTxt = "this is UI test";
        confirmUIData.OKBtnTxt = "Ok";
        confirmUIData.OnclickOKButton = () => Logger.Log("test");
        UIManager.Instance.OpenUI<ConfirmUI>(confirmUIData);
        */
        AudioManager.Instance.OnLoadUserData();
        UIManager.Instance.EnableStatsUI(false);
        StartCoroutine(LoadGameCo());
    }

    IEnumerator LoadGameCo()
    {
        //�� �ڷ�ƾ �Լ��� ������ �ε��� ó�� �����ϴ� �߿��� �Լ��̱� ������
        //�α׸� ����
        //GetType() Ŭ���� ���� ���
        //Ÿ��Ʋ �Ŵ������� ȣ���ϴ� �ε���� �ڷ�ƾ�̶�� �Լ��� Ȯ��
        Logger.Log($"{GetType()}:: LoadGameCo");

        LogoAnim.Play();
        yield return new WaitForSeconds(LogoAnim.clip.length);

        LogoAnim.gameObject.SetActive(false);
        title.SetActive(true);
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync() {
        operation=SceneLoader.Instance.LoadSceneAsync(SceneType.Lobby);
        if (operation == null) {
            Logger.Log("Lobby async loading error");
            yield break;
        }

        operation.allowSceneActivation = false;

        slider.value = 0.5f;
        LoadingProgressTxt.text = $"{(int)(slider.value * 100)}%";
        yield return new WaitForSeconds(0.5f);
        while (!operation.isDone) {
            slider.value = operation.progress < 0.5f ? 0.5f : operation.progress;
            LoadingProgressTxt.text = $"{(int)(slider.value * 100)}%";

            if (operation.progress >= 0.9f) {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
