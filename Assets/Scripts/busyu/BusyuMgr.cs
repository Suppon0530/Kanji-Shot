using UnityEngine;
using System.IO;

// 部首弾を管理するクラス
public class BusyuMgr : MonoBehaviour
{
    // 部首弾の位置データクラス
    [System.Serializable]
    private class BusyuPositionData
    {
        public bool isLeft;
        public Vector3 position;
    }

    private BusyuPositionData busyuPositionData;
    private Vector3 defaultBusyuLeftPosition = new Vector3(-2f, -3.375f, 0f);
    private Vector3 defaultBusyuRightPosition = new Vector3(2f, -3.375f, 0f);
    private string busyuFilePath;
    private static bool dontDestroy = false;

    void Awake()
    {
        if (!dontDestroy)
        {
            DontDestroyOnLoad(this);
            dontDestroy = true;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        // ■ ファイル名を要変更
        busyuFilePath = Path.Combine(Application.persistentDataPath, "bulletposition.json");
        LoadBusyuPositionData();
    }

    // 部首弾の位置を取得
    public Vector3 GetBusyuPosition()
    {
        if (busyuPositionData != null)
        {
            return busyuPositionData.position;
        }

        // データが存在しない場合、デフォルトの位置を返却
        return defaultBusyuLeftPosition;
    }

    // 部首弾の位置（左右）を取得
    public bool GetBusyuIsLeft()
    {
        if (busyuPositionData != null)
        {
            return busyuPositionData.isLeft;
        }

        // データが存在しない場合、デフォルトの位置を返却
        return true;
    }

    // 部首弾の設定
    public void SetBulletPosition(bool isLeft)
    {
        if (isLeft)
        {
            busyuPositionData = new BusyuPositionData()
            {
                isLeft = isLeft,
                position = defaultBusyuLeftPosition
            };
        }
        else if (!isLeft)
        {
            busyuPositionData = new BusyuPositionData()
            {
                isLeft = isLeft,
                position = defaultBusyuRightPosition
            };
        }

        SaveBusyuPositionData();
    }

    // 部首弾の位置データをロード
    private void LoadBusyuPositionData()
    {
        if (File.Exists(busyuFilePath))
        {
            string json = File.ReadAllText(busyuFilePath);
            busyuPositionData = JsonUtility.FromJson<BusyuPositionData>(json);
        }
        else
        {
            // データが存在しない場合、デフォルトのデータを作成
            busyuPositionData = new BusyuPositionData()
            {
                isLeft = true,
                position = defaultBusyuLeftPosition
            };
            SaveBusyuPositionData();
        }
    }

    // 部首弾の位置データを保存
    private void SaveBusyuPositionData()
    {
        string json = JsonUtility.ToJson(busyuPositionData);
        File.WriteAllText(busyuFilePath, json);
    }
}
