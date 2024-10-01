using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SettingsUI : BaseUI
{
    public TextMeshProUGUI GameVersionTxt;//게임버전표시
    public GameObject SoundOnToggle;//사운드가 On일때 활성화해줄UI
    public GameObject SoundOffToggle;//사운드가 Off일때 활성화 해줄 UI
    //보완정책 명시
    //텍스트를 누르면 해당 웹사이트 링크로이동(아무 링크)

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

        AudioManager.Instance.PlaySFX(SFX.ui_button_click);//UI클릭시 사운드 플레이
        //설정데이터가져옴

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
