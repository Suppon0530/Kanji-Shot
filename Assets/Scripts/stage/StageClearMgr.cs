using System.Collections.Generic;
using UnityEngine;
using System.IO;

// ステージクリアを管理するクラス
public class StageClearMgr : MonoBehaviour
{
    // ステージクリアクラス
    [System.Serializable]
    public class StageClearData
    {
        public List<StageRecord> stageRecords = new List<StageRecord>();
    }

    // ステージクラス
    [System.Serializable]
    public class StageRecord
    {
        public string key;
        public bool cleared;
    }

    private StageClearData stageData;
    private string stageClearFilePath;
    private static bool DontDestroy = false;

    void Awake()
    {
        if (!DontDestroy)
        {
            DontDestroyOnLoad(this);
            DontDestroy = true;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        stageClearFilePath = Path.Combine(Application.persistentDataPath, "stagedata.json");
        LoadStageClearData();
    }

    // ステージクリアを取得
    public bool GetStageCleared(int level, int stage)
    {
        string key = GetStageKey(level, stage);
        StageRecord record = FindStageRecord(key);
        return record != null ? record.cleared : false;
    }

    // ステージクリアを設定
    public void SetStageCleared(int level, int stage, bool cleared)
    {
        string key = GetStageKey(level, stage);
        StageRecord record = FindStageRecord(key);

        if (record == null)
        {
            record = new StageRecord()
            {
                key = key,
                cleared = cleared
            };
            stageData.stageRecords.Add(record);
            SaveStageClearData();
        }
    }

    // ステージクリアデータの読み込み
    private void LoadStageClearData()
    {
        if (File.Exists(stageClearFilePath))
        {
            string json = File.ReadAllText(stageClearFilePath);
            stageData = JsonUtility.FromJson<StageClearData>(json);
        }
        else
        {
            stageData = new StageClearData();
            SaveStageClearData();
        }
    }

    // ステージクリアデータの保存
    private void SaveStageClearData()
    {
        string json = JsonUtility.ToJson(stageData);
        File.WriteAllText(stageClearFilePath, json);
    }

    // ステージレコードの検索
    private StageRecord FindStageRecord(string key)
    {
        return stageData.stageRecords.Find(record => record.key == key);
    }

    // ステージキーの取得
    private string GetStageKey(int level, int stage)
    {
        return $"level_{level}_stage_{stage}";
    }
}