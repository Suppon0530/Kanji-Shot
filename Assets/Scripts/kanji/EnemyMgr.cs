using UnityEngine;

// 敵管理クラス
public class EnemyMgr : MonoBehaviour
{
    [SerializeField] private Gamemgrdata gamedata;    // ゲームデータ
    [SerializeField] private Enemymgrdata[] enemydatas;    // レベル毎の敵データ
    [SerializeField] private GameObject enemy;    // 敵のプレハブ
    [SerializeField] private GameObject canvas;    // 敵を配置するキャンバス

    private Enemymgrdata enemydata;    // 利用する敵データを格納する変数

    private int elem;    // 縦に配置する敵数
    private float radius = 2.3125f;    // 円の半径
    private float cycle = 2.0f * Mathf.PI;    // 2π
    private int count = 0;    // 敵を四角形状に配置する点

    private float max_distance;    // 敵が進む最大距離
    private int direction;    // 敵の移動の向き
    private float side;    // 四角形1辺の長さ
    private Vector3 center;    // 敵の円移動の中心座標


    // Start is called before the first frame update
    void Start()
    {
        // レベルで敵データの場合分け
        switch(gamedata.GetLevel()) {
            case 0:
                enemydata = enemydatas[0];
                elem = 4;
                break;
            case 1:
                enemydata = enemydatas[1];
                elem = 5;
                break;
            case 2:
                enemydata = enemydatas[2];
                elem = 6;
                break;
            case 3:
                enemydata = enemydatas[3];
                elem = 5;
                break;
            // 例外処理 : case 0
            default:
                enemydata = enemydatas[0];
                elem = 4;
                break;
        }

        enemydata.SetEnemy();    // 敵データの初期化

        int[] type = enemydata.GetArray_Num();
        int pattern = enemydata.GetEnemy_Pattern();
        GameObject[] enemies = new GameObject[enemydata.GetEnemy_Length()];    // 敵の数だけ配列を確保

        // 敵のインスタンスを生成し、プロパティを設定
        for(int i = 0, j = 0; i < enemies.Length; i++) {

            GameObject new_enemy = Instantiate(enemy, SetInitialPosition(pattern, i), Quaternion.identity);    // 敵インスタンスを生成

            new_enemy.tag = "enemy_" + i;
            new_enemy.GetComponent<SpriteRenderer>().sprite = enemydata.GetEnemy();
            new_enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = enemydata.GetEnemy_Character(i);
            // 敵タイプの計算
            if(j < type.Length) {
                if(type[j] == i) {
                    j++;
                }
            }
            // 敵スクリプトへの設定
            var script = new_enemy.GetComponent<Enemy>();
            script.SetType(j);
            script.SetItem(enemydata.GetItem());
            script.SetItem_Character(enemydata.GetItem_Character(i));
            script.SetEffect(enemydata.GetEffect());
            script.SetPattern(pattern);
            script.SetDistance(max_distance);
            script.SetDirection(direction);
            script.SetSquareSide(side);
            script.SetCenter(center);

            enemies[i] = new_enemy;    // 敵の配列に格納
        }

        ShuffleEnemy(enemies);    // 敵の順序を変更
    }

    // 敵の初期位置を求める関数
    private Vector3 SetInitialPosition(int pattern, int num)
    {
        int row;    // x座標の倍数
        int col;    // y座標の倍数
        int right;    // 左右の判定
        float point;    // 円の分割点
        Vector3 initialposition;    // 敵の初期位置


        // 敵の移動パターンで場合分け
        switch(pattern) {
        // 3 * 4で整列配置
            case 0:
                row = num / elem;
                col = num % elem;
                initialposition = new Vector3(-0.6875f + row * 1.5f, 2.25f - col * 1.5f, 0.0f);
                max_distance = 2.3125f - 0.6875f;
                direction = -1;
                side = 0;
                center = new Vector3(0, 0, 0);
                break;
            // 正方形に配置
            case 1:
                if(count == 5) {
                    count = 7;
                }
                if(count == 9) {
                    count = 11;
                }
                row = count / elem;
                col = count % elem;
                count++;
                initialposition = new Vector3(-2.25f + 4.5f / 3.0f * row, 2.25f - 4.5f / 3.0f * col, 0);
                max_distance = 0;
                direction = -1;
                side = 4.5f;
                center = new Vector3(0, 0, 0);
                break;
            // 円状に配置
            case 2:
                point = ((float)num / 12) * cycle + Mathf.PI / 2;
                initialposition = new Vector3(Mathf.Cos(point) * radius, Mathf.Sin(point) * radius, 0);
                max_distance = 0;
                direction = -1;
                side = 0;
                center = new Vector3(0, 0, 0);
                break;
            // 3 * 5で左右に配置
            case 3:
                row = num / elem;
                col = num % elem;
                right = 1;
                direction = -1;
                if(col % 2 == 0) {
                    right = -1;
                    direction = 1;
                }
                initialposition = new Vector3((-0.1875f + row * 1.25f) * right, 2.375f - col * 1.125f, 0);
                max_distance = 2.3125f - 0.1875f;
                side = 0;
                center = new Vector3(0, 0, 0);
                break;
            // 3箇所に五角形状で配置
            case 4:
                radius = 0.9f;
                if(num < 5) {
                    point = ((float)num / 5) * cycle + Mathf.PI / 2;
                    initialposition = new Vector3(Mathf.Cos(point) * radius - 1.375f, Mathf.Sin(point) * radius + 1.5f, 0);
                    center = new Vector3(-1.375f, 1.5f, 0);
                    direction = -1;
                }
                else if(5 <= num && num < 10) {
                    point = ((float)num / 5) * cycle;
                    initialposition = new Vector3(Mathf.Cos(point) * radius + 1.375f, Mathf.Sin(point) * radius + 0.5f, 0);
                    center = new Vector3(1.375f, 0.5f, 0);
                    direction = 1;
                }
                else {
                    point = ((float)num / 5) * cycle + Mathf.PI / 4;
                    initialposition = new Vector3(Mathf.Cos(point) * radius - 0.25f, Mathf.Sin(point) * radius - 2.0f, 0);
                    center = new Vector3(-0.25f, -2.0f, 0);
                    direction = -1;
                }
                max_distance = 0;
                side = 0;
                break;
            // 中心に五角形、外側に円状に配置
            case 5:
                if(num < 5) {
                    radius = 1.0f;
                    row = 5;
                    direction = 1;
                }
                else {
                    radius = 2.3125f;
                    row = 10;
                    direction = -1;
                }
                point = ((float)num / row) * cycle + Mathf.PI / 2;
                initialposition = new Vector3(Mathf.Cos(point) * radius, Mathf.Sin(point) * radius, 0);
                max_distance = 0;
                side = 0;
                center = new Vector3(0, 0, 0);
                break;
            // 3 * 6で左右バラバラに配置
            case 6:
                row = num / elem;
                col = num % elem;
                right = 1;
                direction = -1;
                if(col % 2 == 0) {
                    right = -1;
                    direction = 1;
                }
                initialposition = new Vector3((-0.1875f + row * 1.25f) * right, 2.5625f - col * 1.0f, 0);
                max_distance = 2.3125f - 0.1875f;
                side = 0;
                center = new Vector3(0, 0, 0);
                break;
            // 3箇所に六角形状に配置
            case 7:
                radius = 1.0f;
                if(num < 6) {
                    point = ((float)num / 6) * cycle + Mathf.PI / 2;
                    initialposition = new Vector3(Mathf.Cos(point) * radius, Mathf.Sin(point) * radius + 1.375f, 0);
                    center = new Vector3(0, 1.375f, 0);
                    direction = 1;
                }
                else if(6 <= num && num < 12) {
                    point = ((float)num / 6) * cycle;
                    initialposition = new Vector3(Mathf.Cos(point) * radius - 1.40625f, Mathf.Sin(point) * radius - 1.375f, 0);
                    center = new Vector3(-1.40625f, -1.375f, 0);
                    direction = -1;
                }
                else {
                    point = ((float)num / 6) * cycle + Mathf.PI / 6;
                    initialposition = new Vector3(Mathf.Cos(point) * radius + 1.40625f, Mathf.Sin(point) * radius - 1.375f, 0);
                    center = new Vector3(1.40625f, -1.375f, 0);
                    direction = 1;
                }
                max_distance = 0;
                side = 0;
                break;
            // 中心に六角形、外側に円状に配置
            case 8:
                if(num < 6) {
                    radius = 1.125f;
                    row = 6;
                    direction = -1;
                }
                else {
                    radius = 2.3125f;
                    row = 12;
                    direction = 1;
                }
                point = ((float)num / row) * cycle + Mathf.PI / 2;
                initialposition = new Vector3(Mathf.Cos(point) * radius, Mathf.Sin(point) * radius, 0);
                max_distance = 0;
                side = 0;
                center = new Vector3(0, 0, 0);
                break;
            // 4 * 5で整列配置
            case 9:
                row = num / elem;
                col = num % elem;
                initialposition = new Vector3(-1.4375f + row * 1.25f, 2.375f - col * 1.125f, 0.0f);
                max_distance = 2.3125f - 1.4375f;
                direction = -1;
                side = 0;
                center = new Vector3(0, 0, 0);
                break;
            // 回状に配置
            case 10:
                if(num < 8) {
                    if(count == 4) {
                        count = 5;
                    }
                    row = count / 3;
                    col = count % 3;
                    count++;
                    initialposition = new Vector3(-1.25f + 2.5f / 2.0f * row, 1.25f - 2.5f / 2.0f * col, 0);
                    direction = 1;
                    side = 2.5f;
                }
                else {
                    if(num == 8) {
                        count = 0;
                    }
                    if(count == 5) {
                        count = 7;
                    }
                    if(count == 9) {
                        count = 11;
                    }
                    row = count / 4;
                    col = count % 4;
                    count++;
                    initialposition = new Vector3(-2.3125f + 4.625f / 3.0f * row, 2.3125f - 4.625f / 3.0f * col, 0);
                    direction = -1;
                    side = 4.625f;
                }
                max_distance = 0;
                center = new Vector3(0, 0, 0);
                break;
            // 二重円状に配置
            case 11:
                if(num < 8) {
                    radius = 1.375f;
                    row = 8;
                    direction = 1;
                }
                else {
                    radius = 2.4375f;
                    row = 12;
                    direction = -1;
                }
                point = ((float)num / row) * cycle + Mathf.PI / 2;
                initialposition = new Vector3(Mathf.Cos(point) * radius, Mathf.Sin(point) * radius, 0);
                max_distance = 0;
                side = 0;
                center = new Vector3(0, 0, 0);
                break;
            // 例外処理 : case 0
            default:
                row = num / elem;
                col = num % elem;
                initialposition = new Vector3(3.5f + row * 1.2f, 2.0f - col * 1.5f, 0);
                max_distance = 2.3125f - 0.6875f;
                direction = -1;
                side = 0;
                center = new Vector3(0, 0, 0);
                break;
        }

        return initialposition;
    } 

    // 敵の順序をランダムにする関数
    private void ShuffleEnemy(GameObject[] enemies)
    {
        // フィッシャー-イェーツのシャッフル(n-1から減少させ、ランダムに敵オブジェクトを入れ替え)
        for(int i = enemies.Length - 1; i > 0; i--) {
            int random = UnityEngine.Random.Range(0, i + 1);

            // 敵文字スプライトを変更
            var temp_sprite = enemies[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            enemies[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = enemies[random].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            enemies[random].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = temp_sprite;

            // アイテムスプライトを変更
            var temp_item = enemies[i].GetComponent<Enemy>().GetItem();
            enemies[i].GetComponent<Enemy>().SetItem(enemies[random].GetComponent<Enemy>().GetItem());
            enemies[random].GetComponent<Enemy>().SetItem(temp_item);

            // アイテム文字スプライトを変更
            var temp_item_c = enemies[i].GetComponent<Enemy>().GetItem_Character();
            enemies[i].GetComponent<Enemy>().SetItem_Character(enemies[random].GetComponent<Enemy>().GetItem_Character());
            enemies[random].GetComponent<Enemy>().SetItem_Character(temp_item_c);

            // 敵タグを変更
            var temp_tag = enemies[i].tag;
            enemies[i].tag = enemies[random].tag;
            enemies[random].tag = temp_tag;

            // 敵番号を変更
            int temp_type = enemies[i].GetComponent<Enemy>().GetNum();
            enemies[i].GetComponent<Enemy>().SetType(enemies[random].GetComponent<Enemy>().GetNum());
            enemies[random].GetComponent<Enemy>().SetType(temp_type);
        }
    }
}
