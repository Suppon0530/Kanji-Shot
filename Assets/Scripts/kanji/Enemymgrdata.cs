using UnityEngine;

[CreateAssetMenu(fileName = "Enemymgrdata", menuName = "Enemymgrdata")]

// 敵のデータを管理するクラス
public class Enemymgrdata : ScriptableObject
{
    [SerializeField] private Sprite enemy;    // 敵スプライト
    [SerializeField] private Sprite item;    // アイテムスプライト
    [SerializeField] private Sprite effect;    // エフェクト
    [SerializeField] private Enemydata[] enemydata;    // 敵データ
    [SerializeField] private Gamemgrdata gamedata;    // ゲームデータ

    private Sprite[] enemy_character;    // 敵文字のスプライト
    private Sprite[] item_character;    // アイテム文字のスプライト
    private int[] array_num;    // 敵種類が変わる配列番号
    private int pattern;    // 敵の移動パターン


    // 敵データの初期化
    public void SetEnemy()
    {
        int stage = gamedata.GetStage();

        enemy_character = enemydata[stage].GetEnemy_Character();
        item_character = enemydata[stage].GetItem_Character();
        array_num = enemydata[stage].GetArray_Num();
        pattern = enemydata[stage].GetEnemy_Pattern();
    }

    // 敵スプライトを返却する関数
    public Sprite GetEnemy(){
        return this.enemy;
    }

    // 敵及びアイテムスプライトの種類数を返却する関数
    public int GetEnemy_Length(){
        return this.enemy_character.Length;
    }

    // 敵文字スプライトを返却する関数
    public Sprite GetEnemy_Character(int num)
    {
        return this.enemy_character[num];
    }

    // アイテムスプライトを返却する関数
    public Sprite GetItem() {
        return this.item;
    }

    // アイテム文字スプライトを返却する関数
    public Sprite GetItem_Character(int num)
    {
        return this.item_character[num];
    }

    // エフェクトを返却する関数
    public Sprite GetEffect()
    {
        return this.effect;
    }

    // 敵種類が変わる配列番号を返却する関数
    public int[] GetArray_Num() {
        return this.array_num;
    }

    // 敵種類が変わる配列番号の配列の長さを返却する関数
    public int GetArray_Num_Length()
    {
        return this.array_num.Length;
    }

    // 敵の移動パターンを返却する関数
    public int GetEnemy_Pattern()
    {
        return this.pattern;
    }


    // 敵データの内部クラス
    [System.Serializable]
    private class Enemydata
    {
        [SerializeField] private Sprite[] enemy_character;    // 敵文字のスプライト
        [SerializeField] private Sprite[] item_character;    // アイテム文字のスプライト
        [SerializeField] private int[] array_num;    // 敵種類が変わる配列番号
        [SerializeField] private int pattern;    // 敵の移動パターン


        // 敵文字スプライトを返却する関数
        public Sprite[] GetEnemy_Character()
        {
            return this.enemy_character;
        }

        // アイテム文字スプライトを返却する関数
        public Sprite[] GetItem_Character()
        {
            return this.item_character;
        }

        // 敵種類が変わる配列番号を返却する関数
        public int[] GetArray_Num()
        {
            return this.array_num;
        }

        // 敵の移動パターンを返却する関数
        public int GetEnemy_Pattern()
        {
            return this.pattern;
        }
    }
}