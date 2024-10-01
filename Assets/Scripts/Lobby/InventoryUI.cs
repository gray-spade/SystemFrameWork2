using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gpm.Ui;
using System;

public enum InventorySortype { 
    Itemgrade,
    ItemType,
}

public class InventoryUI : BaseUI
{
    public InfiniteScroll InventoryScrollList;
    public TextMeshProUGUI SortBtnTxt;
    public EquipmentSlotUI Weapon;
    public EquipmentSlotUI Shield;
    public EquipmentSlotUI Chest;
    public EquipmentSlotUI Boots;
    public EquipmentSlotUI Glove;
    public EquipmentSlotUI Accessory;

    public TextMeshProUGUI AttackPowerTxt;
    public TextMeshProUGUI DefenseAmountTxt;
    private InventorySortype m_inventorySortype = InventorySortype.Itemgrade;



    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        SetUserStats();
        SetEquipmentItems();
        SetInventoty();
        SortInventory();
    }

    public void SetUserStats()
    {
        var userinventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userinventoryData == null) {
            return;
        }
        UserItemStats userItemData =  userinventoryData.GetUserTotalItemStats();
        AttackPowerTxt.text = userItemData.AttackPower.ToString() ;
        DefenseAmountTxt.text = userItemData.Defense.ToString();
    }

    void SetEquipmentItems() {
        var userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userInventoryData == null)
        {
            Logger.LogError("UserInventoryData dose not exist");
        }
            Weapon.SetItem(userInventoryData.EquipmentWeaponData);
        Shield.SetItem(userInventoryData.EquipmentShieldData);
        Chest.SetItem(userInventoryData.EquipmenChestArmorData);
        Boots.SetItem(userInventoryData.EquipmentBootsData);
        Glove.SetItem(userInventoryData.EquipmentGloveData);
        Accessory.SetItem(userInventoryData.EquipmentAccessoryData);
    }

    private void SetInventoty()
    {
        InventoryScrollList.Clear();

        var userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();

        if (userInventoryData != null) {
            foreach (var itemData in userInventoryData.InventoryItemDataList) {
                if (UserDataManager.Instance.GetUserData<UserInventoryData>().IsEquipped(itemData.SerialNumber))
                {
                    continue;
                }

                var itemSlotData = new InventoryItemSlotData();
                itemSlotData.ItemId = itemData.ItemId;
                itemSlotData.SerialNumber = itemData.SerialNumber;
                InventoryScrollList.InsertData(itemSlotData);
            }
        }
        
    }

    public void OnSortBtn()
    {


        switch (m_inventorySortype)
        {
            case InventorySortype.Itemgrade:
                m_inventorySortype = InventorySortype.ItemType;
                break;
            case InventorySortype.ItemType:
                m_inventorySortype = InventorySortype.Itemgrade;
                break;
        }
        SortInventory();
    }

    private void SortInventory() {
        switch (m_inventorySortype) {
            case InventorySortype.Itemgrade:
                SortBtnTxt.text = "등급";

                InventoryScrollList.SortDataList((x, y) =>
                {
                    var itemA = x.data as InventoryItemSlotData;
                    var itemB = y.data as InventoryItemSlotData;

                    int CompareResult = ((itemB.ItemId / 1000) % 10).CompareTo((itemA.ItemId / 1000) % 10);

                    if (CompareResult==0){
                        var itemAstr = itemA.ItemId.ToString();
                        var itemAComp = itemAstr.Substring(0, 1) + itemAstr.Substring(2, 3);

                        var itemBstr = itemB.ItemId.ToString();
                        var itemBComp = itemBstr.Substring(0, 1) + itemBstr.Substring(2, 3);

                        CompareResult = itemAComp.CompareTo(itemBComp);
                    }
                    return CompareResult;
                });
                break;
            case InventorySortype.ItemType:
                SortBtnTxt.text = "타입";

                InventoryScrollList.SortDataList((x, y) =>
                {
                    var itemA = x.data as InventoryItemSlotData;
                    var itemB = y.data as InventoryItemSlotData;

                    var itemAstr = itemA.ItemId.ToString();
                    var itemAComp = itemAstr.Substring(0, 1) + itemAstr.Substring(2, 3);

                    var itemBstr = itemB.ItemId.ToString();
                    var itemBComp = itemBstr.Substring(0, 1) + itemBstr.Substring(2, 3);

                    int CompareResult = itemAComp.CompareTo(itemBComp);
                    

                    if (CompareResult == 0)
                    {
                        CompareResult = ((itemB.ItemId / 1000) % 10).CompareTo((itemA.ItemId / 1000) % 10);

                    }
                    return CompareResult;
                });
                
                break;
        
        }
    
    }

    internal void OnEquipItem(int itemId)
    {
        var userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userInventoryData == null) { return; }

        var itemtype = (ItemType)((itemId / 10000) % 10);
        switch (itemtype)
        {
            case ItemType.Weapon:
                Weapon.SetItem(userInventoryData.EquipmentWeaponData);
                break;
            case ItemType.ChestArmor:
                Chest.SetItem(userInventoryData.EquipmenChestArmorData);
                break;
            case ItemType.Boots:
                Boots.SetItem(userInventoryData.EquipmentBootsData);
                break;
            case ItemType.Accessory:
                Accessory.SetItem(userInventoryData.EquipmentAccessoryData);
                break;
            case ItemType.Shield:
                Shield.SetItem(userInventoryData.EquipmentShieldData);
                break;
            case ItemType.Gloves:
                Glove.SetItem(userInventoryData.EquipmentGloveData);
                break;
        }
        SetInventoty();
        SortInventory();
        SetUserStats();
    }

    internal void OnUnequipItem(int itemId)
    {
        var userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userInventoryData == null) { return; }

        var itemtype = (ItemType)((itemId / 10000) % 10);
        switch (itemtype)
        {
            case ItemType.Weapon:
                Weapon.SetItem(null);
                break;
            case ItemType.ChestArmor:
                Chest.SetItem(null);
                break;
            case ItemType.Boots:
                Boots.SetItem(null);
                break;
            case ItemType.Accessory:
                Accessory.SetItem(null);
                break;
            case ItemType.Shield:
                Shield.SetItem(null);
                break;
            case ItemType.Gloves:
                Glove.SetItem(null);
                break;
        }
        SetInventoty();
        SortInventory();
        SetUserStats();
    }
}
