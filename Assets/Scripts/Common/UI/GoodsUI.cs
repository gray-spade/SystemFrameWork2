using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoodsUI : MonoBehaviour
{
    public TextMeshProUGUI GoldAmountTxt;
    public TextMeshProUGUI GemAmountTxt;


    public void SetValues() {

        var userGoodsData = UserDataManager.Instance.GetUserData<UserGoodsData>();

        if (userGoodsData == null) {
            Logger.Log("No User Goods Data");
            return;
        }


        GoldAmountTxt.text = userGoodsData.Gold.ToString("N0");
        GemAmountTxt.text= userGoodsData.Gem.ToString("N0");

    }


}
