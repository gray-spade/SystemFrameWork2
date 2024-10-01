using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SettingsUI : BaseUI
{
    public TextMeshProUGUI GameVersionTxt;//���ӹ���ǥ��
    public GameObject SoundOnToggle;//���尡 On�϶� Ȱ��ȭ����UI
    public GameObject SoundOffToggle;//���尡 Off�϶� Ȱ��ȭ ���� UI
    //������å ���
    //�ؽ�Ʈ�� ������ �ش� ������Ʈ ��ũ���̵�(�ƹ� ��ũ)

    private const string PRIVACY_POLICY_URL = "https://www.google.com/webhp?hl=ko";

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        SetVersionInfo();

        var userSettingData = UserDataManager.Instance.GetUserData<UserSettingData>();

        if (userSettingData != null)
        {
            SetSoundSetting(userSettingData.Sound);

        }
    }

    private void SetSoundSetting(bool sound)
    {
        SoundOnToggle.SetActive(sound);
        SoundOffToggle.SetActive(!sound);
    }

    private void SetVersionInfo()
    {
        GameVersionTxt.text = $"Version{Application.version}";
    }

    public void OnClickSoundToggle() {
        Logger.Log($"{GetType()}::OnClickSoundToggle");

        AudioManager.Instance.PlaySFX(SFX.ui_button_click);//UIŬ���� ���� �÷���
        //���������Ͱ�����

        var userSettingData = UserDataManager.Instance.GetUserData<UserSettingData>();

        if (userSettingData != null) {
            userSettingData.Sound = !userSettingData.Sound;
            UserDataManager.Instance.SaveUserData();
            AudioManager.Instance.Mute();

            SetSoundSetting(userSettingData.Sound);
        
        }
    }

    public void OnClickPrivacyPoicyBtn() {
        Logger.Log($"{GetType()}::OnClickPrivacyPoicyBtn");

        AudioManager.Instance.PlaySFX(SFX.ui_button_click);
        Application.OpenURL(PRIVACY_POLICY_URL);
    }
}
