using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataTableManager : SingletonBehaviour<DataTableManager>
{

    private const string DATA_PATH = "DataTable";

    private const string CHAPTER_DATA_TABLE = "ChapterDataTable";
     
    private List<ChapterData> ChapterDataTalbe = new List<ChapterData>();
    // Start is called before the first frame update

    protected override void Init()
    {
        base.Init();

        LoadChapterDataTable();
    }

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
}
public class ChapterData
{
    public int ChapterNo;
    public int TotalStage;
    public int ChapterRewardGem;
    public int ChapterRewardGold;
}