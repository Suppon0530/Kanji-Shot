using UnityEngine;

[CreateAssetMenu(fileName = "Gamemgrdata", menuName = "Gamemgrdata")]

// ゲームのレベル・ステージを管理するクラス
public class Gamemgrdata : ScriptableObject
{
    private int level;    // 0: 春, 1: 夏, 2: 秋, 3: 冬
    private int stage;    // 0 ~ 11


    // レベルの設定関数
    public void SetLevel(int level)
    {
        this.level = level;
    }

    // レベルの返却関数
    public int GetLevel()
    {
        return this.level;
    }

    // ステージの設定関数
    public void SetStage(int stage)
    {
        this.stage = stage;
    }

    // ステージの返却関数
    public int GetStage()
    {
        return this.stage;
    }
}