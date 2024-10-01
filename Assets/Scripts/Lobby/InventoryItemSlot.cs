using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gpm.Ui;
using UnityEngine.UI;
using System.Text;

public class InventoryItemSlotData : InfiniteScrollData
{
    public long SerialNumber;
    public int ItemId;

}
    public class InventoryItemSlot : InfiniteScrollItem
{
    public Image ItemGradeBg;//등급에 따라이미지를 처리해줄 백그라운드 이미지 컴포넌트
    public Image ItemIcon;//아이템이 무엇인지에 따라 아이템 이미지를 처리해줄 아이콘 이미지 컴포넌트

    private InventoryItemSlotData m_InvenoryItemSlotData;
    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        
        m_InvenoryItemSlotData = scrollData as InventoryItemSlotData;

        if (m_InvenoryItemSlotData == null) {
            Logger.LogError("m_InvenoryItemSlotData is invalid");
            return;
        }
        
        var itemGrade = (ItemGrade)((m_InvenoryItemSlotData.ItemId / 1000) % 10);
        
        var gradeBgTexture = Resources.Load<Texture2D>($"Textures/{itemGrade}");
        
        if (gradeBgTexture != null)
        {
            ItemGradeBg.sprite =
                Sprite.Create(gradeBgTexture, new Rect(0, 0, gradeBgTexture.width, gradeBgTexture.height), new Vector2(1f, 1f));
        }
        else {
            Debug.Log(itemGrade.ToString());
        }
        Logger.Log(m_InvenoryItemSlotData.ItemId.ToString());
        StringBuilder sb = new StringBuilder(m_InvenoryItemSlotData.ItemId.ToString());
        sb[1] = '1';
        var itemIconName = sb.ToString();

        var itemIconTexture = Resources.Load<Texture2D>($"Textures/{itemIconName}");
        if (itemIconTexture != null)
        {
            ItemIcon.sprite =
                Sprite.Create(itemIconTexture, new Rect(0, 0, itemIconTexture.width, itemIconTexture.height), new Vector2(1f, 1f));
        }
    }

    public void OnClick() {
        EquipmentUIData equipmentUIData = new EquipmentUIData();
        equipmentUIData.ItemId = m_InvenoryItemSlotData.ItemId;
        equipmentUIData.SerialNumber = m_InvenoryItemSlotData.SerialNumber;
        equipmentUIData.IsEquipped=false;
        UIManager.Instance.OpenUI<EquipmentUI>(equipmentUIData);
    }
}
