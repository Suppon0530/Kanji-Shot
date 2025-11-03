using UnityEngine;

[CreateAssetMenu(fileName = "Stagemgrdata", menuName = "Stagemgrdata")]

// ステージデータを管理するクラス
public class Stagemgrdata : ScriptableObject
{
    [SerializeField] private Stagedata stagedata;    // ステージデータ(内部クラス)
    [SerializeField] private Gamemgrdata gamedata;    // ゲームデータ

    private Sprite stage;    // ステージスプライト
    private Sprite frame;    // ゲームフレーム
    private Sprite[] details;    // ステージの詳細スプライト


    // ステージデータの初期化
    public void SetStage()
    {
        stage = stagedata.GetStage();
        frame = stagedata.GetFrame();
        details = stagedata.GetDetails();
    }

    // 敵スプライトを返却する関数
    public Sprite GetStage()
    {
        return this.stage;
    }

    // ゲームフレームを返却する関数
    public Sprite GetFrame()
    {
        return this.frame;;
    }

    // ステージの詳細スプライトを返却する関数
    public Sprite[] GetDetails()
    {
        return this.details;
    }


    // ステージデータの内部クラス
    [System.Serializable]
    private class Stagedata
    {
        [SerializeField] private Sprite stage;    // ステージスプライト
        [SerializeField] private Sprite frame;    // ゲームフレーム
        [SerializeField] private Sprite[] details;    // ステージの詳細スプライト

        // 敵スプライトを返却する関数
        public Sprite GetStage()
        {
            return this.stage;
        }

        // ゲームフレームを返却する関数
        public Sprite GetFrame()
        {
            return this.frame;
        }

        // ステージの詳細スプライトを返却する関数
        public Sprite[] GetDetails()
        {
            return this.details;
        }
    }

}