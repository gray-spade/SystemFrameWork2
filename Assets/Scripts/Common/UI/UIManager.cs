using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    public Transform UICanvasTrs;//UIȭ���� �������� ĳ�ʤ� ������Ʈ Ʈ������
    //UI ȭ���� �� UI ĵ���� Ʈ������ ������ ��ġ�����־�� �ϱ� ������ �ʿ���

    public Transform CloseUITrs;//UI ȭ���� ���� �� ��Ȱ��ȭ ��Ų UI ȭ����� ��ġ������ Ʈ������



    private BaseUI m_FrontUI; //UIȭ�鿡 �����ִ� ���� ��ܿ� �����ִ� UI
    //Ȱ��ȭ�� UI
    private Dictionary<System.Type, GameObject> m_OpenUIPool = new Dictionary<System.Type, GameObject>();
    //��Ȱ��ȭ�� UI
    private Dictionary<System.Type, GameObject> m_CloseUIPool = new Dictionary<System.Type, GameObject>();
    //UI ȭ���� �����ִ��� �����ִ��� ������ �ʿ��ϱ� ������ UI Ǯ�� 2���� ������ ����

    GoodsUI m_StatusUI;

    protected override void Init()
    {
        base.Init();

        m_StatusUI = FindObjectOfType<GoodsUI>();
        if (m_StatusUI == null) {
            Logger.Log("No stats ui component found");
        }
    }

    //������ ���ϴ� UIȭ���� �ν��Ͻ��� ���������Լ�
    //out �Լ������� �Ѱ��� ���̳� ������ ��ȯ�� �� �ֱ� ������
    //�������� ���̳� ������ ��ȯ�ϰ� ���� �� �̷��� out�Ű������� ���
    //�� �Լ��� BaseUI,IsAlreadyOpen �ΰ��� ���� ��ȯ �� �� �ִ�.
    private BaseUI GetUI<T>(out bool isAlreadyOpen) {
        System.Type uiType = typeof(T);

        BaseUI ui = null;
        isAlreadyOpen = false;

        if (m_OpenUIPool.ContainsKey(uiType))//Ȱ��ȭ�� UI��
        {
            ui = m_OpenUIPool[uiType].GetComponent<BaseUI>();
            isAlreadyOpen = true;
        }
        else if (m_CloseUIPool.ContainsKey(uiType))//��Ȱ��ȭ�� UI��
        {

            ui = m_CloseUIPool[uiType].GetComponent<BaseUI>();
            m_CloseUIPool.Remove(uiType);
        }
        else { //�ѹ��� ������ ���� ���� UI ��
            var uiObj = Instantiate(Resources.Load($"UI/{uiType}", typeof(GameObject))) as GameObject;
            //�������� �̸��� Ŭ�������̶� ���ƾ���
            //Ŭ�������� �������� �����ϱ� ����
            ui = uiObj.GetComponent<BaseUI>();
        }
        return ui;
    }

    public void OpenUI<T>(BaseUIData uidata) {
        System.Type uiType = typeof(T);

        Logger.Log($"{GetType()}::OpneUI({uiType})");

        bool isAlreadyOpen = false;


        var ui = GetUI<T>(out isAlreadyOpen);
        if (!ui) {
            Logger.Log($"{uiType} dose not exist");
            return;
        }

        if (isAlreadyOpen)//�̹� ���������� ���������� ��û�̶�� �α�
        {
            Logger.Log($"{uiType} is Already Open ");
            return;
        }

        var siblingIdx = UICanvasTrs.childCount-1;//���� ������Ʈ ����
        ui.Init(UICanvasTrs);//ȭ�� �ʱ�ȭ

        ui.transform.SetSiblingIndex(siblingIdx);
        //���̾��Ű â �켱��������

        ui.gameObject.SetActive(true);
        ui.SetInfo(uidata);
        ui.ShowUI();

        m_FrontUI = ui;
        m_OpenUIPool[uiType] = ui.gameObject;
    }

    public void CloseUI(BaseUI ui) {
        System.Type uiType = ui.GetType();
        Logger.Log($"{GetType()}::CloseUI({uiType})");

        ui.gameObject.SetActive(false);

        m_OpenUIPool.Remove(uiType);
        m_CloseUIPool[uiType] = ui.gameObject;
        ui.transform.SetParent(CloseUITrs);

        m_FrontUI = null;

        var lastChild = UICanvasTrs.GetChild(UICanvasTrs.childCount - 1);

        if (lastChild)
        {
            m_FrontUI = lastChild.gameObject.GetComponent<BaseUI>();
        }
    }
    //Ư�� UIȭ���� �����ִ��� Ȯ���ϰ� �� �����ִ� UIȭ���� �������� �Լ�
    public BaseUI GetActiveUI<T>() //�̸� �Ű澲�� ���Ŀ� �̸��� �޶� ������ �߻��߾���. -GetActivePopupUI�̷� �̸��̿���
    {
        var uiType = typeof(T);
        //m_OpenUIPool�� Ư�� ȭ�� �ν��Ͻ��� �����Ѵٸ� �� ȭ�� �ν��Ͻ��� ������ �ְ� �׷��� ������ �� ����
        return m_OpenUIPool.ContainsKey(uiType) ? m_OpenUIPool[uiType].GetComponent<BaseUI>() : null;

    }

    //UIȭ���� �������� �ϳ��� �ִ��� Ȯ���ϴ� �Լ�
    public bool ExistsOpenUI()
    {
        return m_FrontUI != null; //m_FrontUI�� null���� �ƴ��� Ȯ���ؼ� bool���� ��ȯ
    }

    //���� ���� �ֻ�ܿ� �ִ� �ν��Ͻ��� �����ϴ� �Լ�

    public BaseUI GetCurrentFrontUI()
    {
        return m_FrontUI;
    }

    //���� �ֻ�ܿ� �ִ� UIȭ�� �ν��Ͻ��� �ݴ� �Լ�
    public void CloseCurrFrontUI()
    {
        m_FrontUI.CloseUI();
    }

    //�����ִ� ��� UIȭ���� ������� �Լ�

    public void CloseAllOpenUI()
    {
        while (m_FrontUI)
        {
            m_FrontUI.CloseUI(true);
        }
    }

    public void EnableStatsUI(bool value) {
        m_StatusUI.gameObject.SetActive(value);

        if (value) {
            m_StatusUI.SetValues();
        }
    
    }
}
