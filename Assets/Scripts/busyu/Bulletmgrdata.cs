using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bulletmgrdata", menuName = "Bulletmgrdata")]

// 弾(部首)データを管理するクラス
public class Bulletmgrdata : ScriptableObject
{
    [SerializeField] private Sprite bullet;    // 部首スプライト
    [SerializeField] private Bulletdata[] bulletdata;    // 弾データ
    [SerializeField] private Gamemgrdata gamedata;    // ゲームデータ

    private Sprite[] bullet_character;    // 部首文字のスプライト


    // 弾を初期化する関数
    public void SetBullet()
    {
        int stage = gamedata.GetStage();

        bullet_character = bulletdata[stage].GetBullet_Character();
    }

    // 部首スプライトを返却する関数
    public Sprite GetBullet()
    {
        return this.bullet;
    }

    // 部首文字スプライトを返却する関数
    public Sprite[] GetBullet_Character()
    {
        return this.bullet_character;
    }


    // 弾データの内部クラス
    [System.Serializable]
    private class Bulletdata
    {
        [SerializeField] private Sprite[] bullet_character;    // 部首文字のスプライト

        // 部首文字スプライトを返却する関数
        public Sprite[] GetBullet_Character()
        {
            return this.bullet_character;
        }
    }
}
