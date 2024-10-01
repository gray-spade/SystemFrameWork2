using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataTableManager : SingletonBehaviour<DataTableManager>
{

    private const string DATA_PATH = "DataTable";
    protected override void Init()
    {
        base.Init();

        LoadChapterDataTable();
        LoadItemDataTable();
    }

    #region CHAPTER_DATA
    private const string CHAPTER_DATA_TABLE = "ChapterDataTable";
     
    private List<ChapterData> ChapterDataTalbe = new List<ChapterData>();
    // Start is called before the first frame update

    

    private void LoadChapterDataTable()
    {
        var parsedDataTable = CSVReader.Read($"{DATA_PATH}/{CHAPTER_DATA_TABLE}");

        foreach (var data in parsedDataTable) {
            ChapterDataTalbe.Add(new ChapterData {
                ChapterNo= Convert.ToInt32( data["chapter_no"]), 
                TotalStage = Convert.ToInt32(data["total_stages"]),
                ChapterRewardGem = Convert.ToInt32(data["chapter_reward_gem"]),
                ChapterRewardGold = Convert.ToInt32(data["chapter_reward_gold"])
            });
        }
    }
    private void LoadDataTable<T>(List<T> DataTalbe,string path)
    {
        var parsedDataTable = CSVReader.Read($"{DATA_PATH}/{path}");

        foreach (var data in parsedDataTable)
        {
            ChapterDataTalbe.Add(new ChapterData
            {
                ChapterNo = Convert.ToInt32(data["chapter_no"]),
                TotalStage = Convert.ToInt32(data["total_stage"]),
                ChapterRewardGem = Convert.ToInt32(data["chapter_reward_gem"]),
                ChapterRewardGold = Convert.ToInt32(data["chapter_reward_gold"])
            });
        }
    }

    public ChapterData GetChapterData(int chapterNo) {
        //Ư�� é�� �ѹ��� é�� ������ ���̺��� �˻��ؼ�
        //�� é�� �ѹ��� �ش��ϴ� �����͸� ��ȯ�ϴ� �Լ�
        //��ũ ���->��ũ : �˻�,������ �� �� �����ϰ� ���ִ� ���
        //���� ��ũ�� ������� �ʴ´ٸ�
        /*
        foreach (var item in ChapterDataTalbe) {
            if (item.ChapterNo == chapterNo) {
                return item;
            }
        }
        return null;*/

        //LinQ
        //.Where ���ǽ��� ���� ��Ҹ� ���͸�
        //FIrstOrDefault() �Լ� :�Ű������� ������ ��� �÷����� ù��° ��Ҹ� ��ȯ�մϴ�
        //�־��� ������ �����ϴ� ���������� ù ��° ��Ҹ� �˻��ϴ� LINQ �޼�
        //new[] {"A","B","C"}.FIrstOrDefault();
        //A

        return ChapterDataTalbe.Where(item => item.ChapterNo == chapterNo).FirstOrDefault();
    }

    #endregion

    #region ITEM_DATA
        private const string ITEM_DATA_TABLE = "ItemDataTable";

    private List<ItemData> ItemDataTable = new List<ItemData>();
    void LoadItemDataTable() {
        var parsedDataTable = CSVReader.Read($"{DATA_PATH}/{ITEM_DATA_TABLE}");
        foreach (var data in parsedDataTable) {
            var itemData = new ItemData
            {
                ItemId = Convert.ToInt32(data["item_id"]),
                ItemName = data["item_name"].ToString(),
                AttackPower = Convert.ToInt32(data["attack_power"]),
                Defense = Convert.ToInt32(data["defense"]),

            };

            ItemDataTable.Add(itemData);
        }
    }

    public ItemData GetItemData(int itemId) {
        return ItemDataTable.Where(item => item.ItemId == itemId).FirstOrDefault();
    }
    #endregion
    public class ChapterData
    {
        public int ChapterNo;
        public int TotalStage;
        public int ChapterRewardGem;
        public int ChapterRewardGold;
    }

    public class ItemData {
        public int ItemId;
        public string ItemName;
        public int AttackPower;
        public int Defense;
    }

    
}
public enum ItemType
{
    Weapon = 1,
    Shield,
    ChestArmor,
    Gloves,
    Boots,
    Accessory
}

public enum ItemGrade
{
    Common = 1,
    UnCommon,
    Rare,
    Epic,
    Legendary
}