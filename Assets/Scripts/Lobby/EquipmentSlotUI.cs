using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    public GameObject AddIcon;
    public Image EquippedItemGradeBg;
    public Image EquippedItemIcon;
    private UserItemData m_UserItemData;
    
    public void SetItem(UserItemData userItemData)
    {
        m_UserItemData = userItemData;
        if (m_UserItemData != null)
        {
            AddIcon.SetActive(false);
            EquippedItemGradeBg.gameObject.SetActive(true);
            EquippedItemIcon.gameObject.SetActive(true);


            var itemGrade = (ItemGrade)((m_UserItemData.ItemId / 1000) % 10);

            var gradeBgTexture = Resources.Load<Texture2D>($"Textures/{itemGrade}");

            if (gradeBgTexture != null)
            {
                EquippedItemGradeBg.sprite =
                    Sprite.Create(gradeBgTexture, new Rect(0, 0, gradeBgTexture.width, gradeBgTexture.height), new Vector2(1f, 1f));
            }
            

            StringBuilder sb = new StringBuilder(m_UserItemData.ItemId.ToString());
            sb[1] = '1';
            var itemIconName = sb.ToString();
            var itemIconTexture = Resources.Load<Texture2D>($"Textures/{itemIconName}");
            if (itemIconTexture != null)
            {
                EquippedItemIcon.sprite =
                    Sprite.Create(itemIconTexture, new Rect(0, 0, itemIconTexture.width, itemIconTexture.height), new Vector2(1f, 1f));
            }
        }
        else {
            m_UserItemData = null;
            AddIcon.SetActive(true);
            EquippedItemGradeBg.gameObject.SetActive(false);
            EquippedItemIcon.gameObject.SetActive(false);
        }


    }





    public void OnClick()
    {
        EquipmentUIData equipmentUIData = new EquipmentUIData();
        equipmentUIData.ItemId = m_UserItemData.ItemId;
        equipmentUIData.SerialNumber = m_UserItemData.SerialNumber;
        equipmentUIData.IsEquipped = true;
        UIManager.Instance.OpenUI<EquipmentUI>(equipmentUIData);
    }
}
