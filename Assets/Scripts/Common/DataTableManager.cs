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
        //특정 챕터 넘버로 챕터 데이터 테이블을 검색해서
        //그 챕터 넘버에 해당하는 데이터를 반환하는 함수
        //링크 사용->링크 : 검색,변경을 좀 더 용이하게 해주는 기능
        //만약 링크를 사용하지 않는다면
        /*
        foreach (var item in ChapterDataTalbe) {
            if (item.ChapterNo == chapterNo) {
                return item;
            }
        }
        return null;*/

        //LinQ
        //.Where 조건식이 참인 요소만 필터링
        //FIrstOrDefault() 함수 :매개변수가 생략된 경우 컬렉션의 첫번째 요소를 반환합니다
        //주어진 조건을 만족하는 시퀸스에서 첫 번째 요소를 검색하는 LINQ 메서
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