using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserDataManager : SingletonBehaviour<UserDataManager>
{
    public bool ExistsSaveData { get; private set; } = false;

    public List<IUserData> UserDataList { get; private set; } = new List<IUserData>();

    public void LoadUserData()
    {
        ExistsSaveData = PlayerPrefs.GetInt("ExistsSaveData") == 1 ? true : false;
        if (ExistsSaveData) {
            for (int i = 0; i < UserDataList.Count; i++)
            {
                UserDataList[i].LoadData();
            }

        }
    }

    public void SaveUserData()
    {
        bool hasSaveError = false;
        for (int i = 0; i < UserDataList.Count; i++)
        {
            hasSaveError = UserDataList[i].SaveData();
        }
        ExistsSaveData = !hasSaveError;
        if (!hasSaveError) {
            PlayerPrefs.SetInt("ExistsSaveData", 1);
        }
        
    }

    public void SetDefaultUserData()
    {
        for (int i = 0; i < UserDataList.Count; i++) {
            UserDataList[i].SetDefaultData();
        }
    }

    protected override void Init()
    {
        base.Init();

        UserDataList.Add(new UserSettingData());
        UserDataList.Add(new UserGoodsData());
        UserDataList.Add(new UserInventoryData());
    }

    public T GetUserData<T>() where T : class, IUserData
    {
        return UserDataList.OfType<T>().FirstOrDefault();
    }
}
