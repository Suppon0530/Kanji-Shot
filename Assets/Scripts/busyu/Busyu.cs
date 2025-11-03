using UnityEngine;

// 弾クラス
public class Busyu : MonoBehaviour
{
    [SerializeField] private Gamemgrdata game;
    [SerializeField] private Bulletmgrdata[] bulletdatas;

    private Bulletmgrdata bulletdata;    // 部首管理データ
    private Sprite busyu;    // 部首スプライト
    private Sprite[] busyu_character;    // 部首文字スプライト
    private int busyu_num = 0;    // 現在の部首番号
    private float rotationAngle = 0.0f; // 回転角度

    private BusyuMgr bulletmgrdata;


    // Start is called before the first frame update
    void Start()
    {
        // レベルで弾データを場合分け
        switch(game.GetLevel()) {
            case 0:
                bulletdata = bulletdatas[0];
                break;
            case 1:
                bulletdata = bulletdatas[1];
                break;
            case 2:
                bulletdata = bulletdatas[2];
                break;
            case 3:
                bulletdata = bulletdatas[3];
                break;
            default:
                bulletdata = bulletdatas[0];
                break;
        }

        bulletdata.SetBullet();    // 弾データの初期化

        busyu = bulletdata.GetBullet();
        busyu_character = bulletdata.GetBullet_Character();

        // 部首スプライト・部首文字スプライトの設定
        gameObject.GetComponent<SpriteRenderer>().sprite = busyu;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = busyu_character[busyu_num];

        GameObject dontDestroyObject = GameObject.Find("BusyuMgr");
        if (dontDestroyObject != null)
        {
            bulletmgrdata = dontDestroyObject.GetComponent<BusyuMgr>();
        }

        this.gameObject.transform.position = bulletmgrdata.GetBusyuPosition();
    }

    // ユーザが、部首に触れた場合の処理
    public void onClickBullet()
    {
        busyu_num++;
        if(busyu_num == busyu_character.Length) {
            busyu_num = 0;
        }

        // 部首文字スプライトを回転させて表示
        float angleIncrement = 90.0f / busyu_character.Length;
        rotationAngle += angleIncrement;
        if (rotationAngle >= 90.0f)
        {
            rotationAngle -= 90.0f;
        }

        // 部首文字スプライトの回転を適用
        gameObject.transform.GetChild(1).rotation = Quaternion.Euler(0.0f, 0.0f, rotationAngle);

        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = busyu_character[busyu_num];    // 部首文字スプライトを変更
    }

    // 部首番号を返却する関数
    public int GetBullet_Num()
    {
        return busyu_num;
    }
}
