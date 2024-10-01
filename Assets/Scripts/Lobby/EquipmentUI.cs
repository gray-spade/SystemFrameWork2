using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class EquipmentUIData : BaseUIData {
    public long SerialNumber;
    public int ItemId;
    public bool IsEquipped;
}

public class EquipmentUI : BaseUI
{
    public Image ItemGradeBg;
    public Image ItemIcon;
    public TextMeshProUGUI ItemGradeBgTxt;
    public TextMeshProUGUI ItemIconTxt;

    public TextMeshProUGUI AttackAmountTxt;
    public TextMeshProUGUI DefenseAmountTxt;
    public TextMeshProUGUI EquipmentBtnTxt;

    EquipmentUIData m_EquipmentUIData;

    public override void SetInfo(BaseUIData uiData)
    {

        base.SetInfo(uiData);
        m_EquipmentUIData = uiData as EquipmentUIData;
        if (m_EquipmentUIData == null) {
            Logger.LogError("m_EquipmentUIData is invalid");
            return;
        }

        var itemData = DataTableManager.Instance.GetItemData(m_EquipmentUIData.ItemId);
        if (itemData == null) {
            
            return;
        }
        var itemGrade = (ItemGrade)((itemData.ItemId / 1000) % 10);

        var gradeBgTexture = Resources.Load<Texture2D>($"Textures/{itemGrade}");

        if (gradeBgTexture != null)
        {
            ItemGradeBg.sprite =
                Sprite.Create(gradeBgTexture, new Rect(0, 0, gradeBgTexture.width, gradeBgTexture.height), new Vector2(1f, 1f));
        }
        var hexColor = string.Empty;
        switch (itemGrade)
        {
            case ItemGrade.Common:
                hexColor = "#1AB3ff";
                break;
            case ItemGrade.UnCommon:
                hexColor = "#51C52C";
                break;
            case ItemGrade.Rare:
                hexColor = "#EA5AFF";
                break;
            case ItemGrade.Epic:
                hexColor = "#FF9900";
                break;
            case ItemGrade.Legendary:
                hexColor = "#F24949";
                break;
        }
        Color color;
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
            ItemGradeBgTxt.color = color;
        ItemGradeBgTxt.text = itemGrade.ToString();

        Logger.Log(m_EquipmentUIData.ItemId.ToString());
        StringBuilder sb = new StringBuilder(m_EquipmentUIData.ItemId.ToString());
        sb[1] = '1';
        var itemIconName = sb.ToString();

        var itemIconTexture = Resources.Load<Texture2D>($"Textures/{itemIconName}");
        if (itemIconTexture != null)
        {
            ItemIcon.sprite =
                Sprite.Create(itemIconTexture, new Rect(0, 0, itemIconTexture.width, itemIconTexture.height), new Vector2(1f, 1f));
        }
        
       

        AttackAmountTxt.text = $"+{itemData.AttackPower}";
        DefenseAmountTxt.text = $"+{itemData.Defense}";


        ItemIconTxt.text = itemData.ItemName;
        EquipmentBtnTxt.text = m_EquipmentUIData.IsEquipped ? "Unequip" : "equip";
    }

    public void OnClickEquipBtn() {
        var userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userInventoryData == null) { return; }
        if (m_EquipmentUIData.IsEquipped)
        {
            userInventoryData.UnequipItem(m_EquipmentUIData.SerialNumber,m_EquipmentUIData.ItemId);
        }
        else {
            userInventoryData.EquipItem(m_EquipmentUIData.SerialNumber, m_EquipmentUIData.ItemId);
        }
        userInventoryData.SaveData();

        var inventoryUI = UIManager.Instance.GetActiveUI<InventoryUI>() as InventoryUI;

        if (inventoryUI != null) {
            if (m_EquipmentUIData.IsEquipped)
            {
                inventoryUI.OnUnequipItem( m_EquipmentUIData.ItemId);
            }
            else
            {
                inventoryUI.OnEquipItem( m_EquipmentUIData.ItemId);
            }

        }

        CloseUI();
    }
}
