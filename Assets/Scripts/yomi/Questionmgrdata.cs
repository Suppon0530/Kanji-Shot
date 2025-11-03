using UnityEngine;

[CreateAssetMenu(fileName = "Questionmgrdata", menuName = "Questionmgrdata")]

// 問題データを管理するクラス
public class Questionmgrdata : ScriptableObject
{
    [SerializeField] private Questiondata[] questiondata;    // 問題データ
    [SerializeField] private Gamemgrdata gamedata;    // ゲームデータ

    private Sprite[] question;    // 問題スプライト


    // 問題データの初期化
    public void SetQuestion()
    {
        int stage = gamedata.GetStage();

        question = questiondata[stage].GetQuestion();
    }

    // 問題スプライトを返却する関数
    public Sprite[] GetQuestion()
    {
        return this.question;
    }

    // 問題スプライトの長さを返却する関数
    public int GetQuestion_Length()
    {
        return this.question.Length;
    }

    // 問題データの内部クラス
    [System.Serializable]
    private class Questiondata {

        [SerializeField] private Sprite[] question;    // 問題スプライト

        // 問題スプライトを返却する関数
        public Sprite[] GetQuestion()
        {
            return this.question;
        }
    }
}