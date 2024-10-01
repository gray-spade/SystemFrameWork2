using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class UserItemData
{
    public long SerialNumber;//전체아이템에서 특정 아이템을 식별하기위한 아이템 고유ID


    //해당 아이템의 아이템 데이터 테이블 상의 ItemId 아이템 타입에 가까움
    public int ItemId;

    public UserItemData(long serialNumber, int itemId)
    {
        SerialNumber = serialNumber;
        ItemId = itemId;
    }
}

public class UserInventoryItemDataListWrapper
{
    public List<UserItemData> UserInventoryItemDataList;
}

public class UserItemStats
{
    public int AttackPower;
    public int Defense;

    public UserItemStats(int attackPower, int defense)
    {
        AttackPower = attackPower;
        Defense = defense;
    }
}
public class UserInventoryData : IUserData
{
    public List<UserItemData> InventoryItemDataList { get; set; } = new List<UserItemData>();
    public UserItemData EquipmentWeaponData { get; set; }
    public UserItemData EquipmentShieldData { get; set; }
    public UserItemData EquipmenChestArmorData { get; set; }
    public UserItemData EquipmentBootsData { get; set; }
    public UserItemData EquipmentGloveData { get; set; }
    public UserItemData EquipmentAccessoryData { get; set; }

    public Dictionary<long, UserItemStats> EquippedItemDic { get; set; } = new Dictionary<long, UserItemStats>();


    public void SetDefaultData()
    {
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 11001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 12002));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 21001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 23002));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 31001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 34002));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 41001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 45002));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 51001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 52002));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 61001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 64002));

        EquipmentWeaponData = new UserItemData(InventoryItemDataList[0].SerialNumber, InventoryItemDataList[0].ItemId);
        EquipmentShieldData = new UserItemData(InventoryItemDataList[2].SerialNumber, InventoryItemDataList[2].ItemId);

        SetEquippedItemDic();
    }

    public void EquipItem(long serialNumber, int itemId)
    {
        var itemData = DataTableManager.Instance.GetItemData(itemId);
        if (itemData == null) { return; }

        var itemtype = (ItemType)((itemId / 10000) % 10);
        switch (itemtype)
        {
            case ItemType.Weapon:
                if (EquipmentWeaponData != null)
                {
                    EquippedItemDic.Remove(EquipmentWeaponData.SerialNumber);
                    EquipmentWeaponData = null;
                }
                EquipmentWeaponData = new UserItemData(serialNumber, itemId);
                break;
            case ItemType.ChestArmor:
                if (EquipmenChestArmorData != null)
                {
                    EquippedItemDic.Remove(EquipmenChestArmorData.SerialNumber);
                    EquipmenChestArmorData = null;
                }
                EquipmenChestArmorData = new UserItemData(serialNumber, itemId);
                break;
            case ItemType.Boots:
                if (EquipmentBootsData != null)
                {
                    EquippedItemDic.Remove(EquipmentBootsData.SerialNumber);
                    EquipmentBootsData = null;
                }
                EquipmentBootsData = new UserItemData(serialNumber, itemId);
                break;
            case ItemType.Accessory:
                if (EquipmentAccessoryData != null)
                {
                    EquippedItemDic.Remove(EquipmentAccessoryData.SerialNumber);
                    EquipmentAccessoryData = null;
                }
                EquipmentAccessoryData = new UserItemData(serialNumber, itemId);
                break;
            case ItemType.Shield:
                if (EquipmentShieldData != null)
                {
                    EquippedItemDic.Remove(EquipmentShieldData.SerialNumber);
                    EquipmentShieldData = null;
                }
                EquipmentShieldData = new UserItemData(serialNumber, itemId);
                break;
            case ItemType.Gloves:
                if (EquipmentGloveData != null)
                {
                    EquippedItemDic.Remove(EquipmentGloveData.SerialNumber);
                    EquipmentGloveData = null;
                }
                EquipmentGloveData = new UserItemData(serialNumber, itemId);
                break;

        }
        EquippedItemDic.Add(serialNumber, new UserItemStats(itemData.AttackPower, itemData.Defense));
    }

    public void UnequipItem(long serialNumber, int itemId)
    {
        var itemtype = (ItemType)((itemId / 10000) % 10);
        switch (itemtype)
        {
            case ItemType.Weapon:
                EquipmentWeaponData = null;
                break;
            case ItemType.ChestArmor:
                EquipmenChestArmorData = null;
                break;
            case ItemType.Boots:
                EquipmentBootsData = null;
                break;
            case ItemType.Accessory:
                EquipmentAccessoryData = null;
                break;
            case ItemType.Shield:
                EquipmentShieldData = null;
                break;
            case ItemType.Gloves:
                EquipmentGloveData = null;
                break;

        }
        EquippedItemDic.Remove(serialNumber);
    }
    public UserItemStats GetUserTotalItemStats() {
        var totalAttackPower = 0;
        var totalDefense = 0;

        foreach (var item in EquippedItemDic) {
            totalAttackPower += item.Value.AttackPower;
            totalDefense += item.Value.Defense;
        }
        return new UserItemStats(totalAttackPower, totalDefense);
    }


    public bool LoadData()
    {

        bool result = false;

        try
        {
            string weaponJson = PlayerPrefs.GetString("EquipmentWeaponData");
            if (!string.IsNullOrEmpty(weaponJson))
            {
                EquipmentWeaponData = JsonUtility.FromJson<UserItemData>(weaponJson);
            }
            string shieldJson = PlayerPrefs.GetString("EquipmentShieldData");
            if (!string.IsNullOrEmpty(shieldJson))
            {
                EquipmentWeaponData = JsonUtility.FromJson<UserItemData>(shieldJson);
            }
            string chestArmorJson = PlayerPrefs.GetString("EquipmenChestArmorData");
            if (!string.IsNullOrEmpty(chestArmorJson))
            {
                EquipmentWeaponData = JsonUtility.FromJson<UserItemData>(chestArmorJson);
            }
            string bootsJson = PlayerPrefs.GetString("EquipmentBootsData");
            if (!string.IsNullOrEmpty(bootsJson))
            {
                EquipmentWeaponData = JsonUtility.FromJson<UserItemData>(bootsJson);
            }
            string gloveJson = PlayerPrefs.GetString("EquipmentGloveData");
            if (!string.IsNullOrEmpty(gloveJson))
            {
                EquipmentWeaponData = JsonUtility.FromJson<UserItemData>(gloveJson);
            }
            string accessoryJson = PlayerPrefs.GetString("EquipmentAccessoryData");
            if (!string.IsNullOrEmpty(accessoryJson))
            {
                EquipmentWeaponData = JsonUtility.FromJson<UserItemData>(accessoryJson);
            }


            string inventoryItemDataListJson = PlayerPrefs.GetString("InventoryItemDataList");
            if (!string.IsNullOrEmpty(inventoryItemDataListJson))
            {
                UserInventoryItemDataListWrapper itemDataListWrapper =
                    JsonUtility.FromJson<UserInventoryItemDataListWrapper>(inventoryItemDataListJson);

                InventoryItemDataList = itemDataListWrapper.UserInventoryItemDataList;

                foreach (var item in InventoryItemDataList)
                {
                    Logger.Log($"SerialNumber:{item.SerialNumber} ItemId:{item.ItemId}");
                }

            }
            SetEquippedItemDic();
            result = true;
        }
        catch (Exception)
        {

            throw;
        }
        return result;
    }

    void SaveJson(string Json, UserItemData data)
    {
        string equipJson = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(Json, equipJson);
        if (data != null)
        {

            Logger.Log($"{Json}: SN:{data.SerialNumber} ItemId:{data.ItemId}");
        }
    }
    public bool SaveData()
    {
        bool result = false;

        try
        {
            SaveJson("EquipmentWeaponData", EquipmentWeaponData);
            SaveJson("EquipmentShieldData", EquipmentShieldData);
            SaveJson("EquipmenChestArmorData", EquipmenChestArmorData);
            SaveJson("EquipmentBootsData", EquipmentBootsData);
            SaveJson("EquipmentGloveData", EquipmentGloveData);
            SaveJson("EquipmentAccessoryData", EquipmentAccessoryData);


            UserInventoryItemDataListWrapper itemDataListWrapper = new UserInventoryItemDataListWrapper();

            itemDataListWrapper.UserInventoryItemDataList = InventoryItemDataList;

            string inventoryItemDataListJson = JsonUtility.ToJson(itemDataListWrapper);
            PlayerPrefs.SetString("InventoryItemDataList", inventoryItemDataListJson);
            foreach (var item in InventoryItemDataList)
            {
                Logger.Log($"SerialNumber:{item.SerialNumber} ItemId:{item.ItemId}");
            }
            PlayerPrefs.Save();
            result = true;
        }
        catch (Exception)
        {

            throw;
        }
        return result;
    }

    public void SetEquippedItemDic(UserItemData userInventoryData)
    {
        if (userInventoryData == null) { return; }
        var itemData = DataTableManager.Instance.GetItemData(userInventoryData.ItemId);
        if (itemData != null)
        {
            if(!EquippedItemDic.ContainsKey(userInventoryData.SerialNumber))
                EquippedItemDic.Add(userInventoryData.SerialNumber, new UserItemStats(itemData.AttackPower, itemData.Defense));
        }
    }
    public void SetEquippedItemDic()
    {
        SetEquippedItemDic(EquipmentWeaponData);
        SetEquippedItemDic(EquipmentShieldData);
        SetEquippedItemDic(EquipmentGloveData);
        SetEquippedItemDic(EquipmentAccessoryData);
        SetEquippedItemDic(EquipmentBootsData);
        SetEquippedItemDic(EquipmenChestArmorData);
    }

    public bool IsEquipped(long serialNumber)
    {
        return EquippedItemDic.ContainsKey(serialNumber);
    }
}
