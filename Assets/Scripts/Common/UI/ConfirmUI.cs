using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public enum ConfirmType { 
    OK,//Ȯ�ο� �˾�
    OK_CANCEL,//���� ������ ����
}
public class ConfirmUIData : BaseUIData {

    public ConfirmType ConfirmType;
    public string TitleTxt;
    public string DescTxt;
    public string OKBtnTxt;
    public Action OnclickOKButton;
    public string CancelTxt;
    public Action OnclickCancelButton;
}


public class ConfirmUI : BaseUI
{
    public TextMeshProUGUI TitleTxt = null;
    public TextMeshProUGUI DescTxt = null;
    public Button OKBtn;
    public Button CancelBtn;
    public TextMeshProUGUI OKBtnTxt = null;
    public TextMeshProUGUI CancelBtnTxt = null;

    ConfirmUIData m_ConfirmUIData = null;

    public Action m_OnclickOKButton = null;
    public Action m_OnclickCancelButton =null;

    public override void SetInfo(BaseUIData uiData)
    {//������ �ʱ�ȭ
        base.SetInfo(uiData);
        m_ConfirmUIData = uiData as ConfirmUIData;
        TitleTxt.text = m_ConfirmUIData.TitleTxt;
        DescTxt.text = m_ConfirmUIData.DescTxt;
        m_OnclickOKButton = m_ConfirmUIData.OnclickOKButton;
        m_OnclickCancelButton = m_ConfirmUIData.OnclickCancelButton;
        OKBtnTxt.text = m_ConfirmUIData.OKBtnTxt;
        CancelBtnTxt.text = m_ConfirmUIData.CancelTxt;
        // ok��ư�� cancel ��ư Ȱ��ȭ
        // ConfirmType�� OK�� OK��ư��, OK_CANCEL�̸� ��ư �Ѵ� Ȱ��ȭ
        OKBtn.gameObject.SetActive(true);
        CancelBtn.gameObject.SetActive(m_ConfirmUIData.ConfirmType == ConfirmType.OK_CANCEL);
    }

    public void OnClickOkBtn() {
        m_OnclickOKButton?.Invoke();
        CloseUI();
    }
    public void OnClickCancelcBtn()
    {
        m_OnclickCancelButton?.Invoke();
        CloseUI();
    }
}
