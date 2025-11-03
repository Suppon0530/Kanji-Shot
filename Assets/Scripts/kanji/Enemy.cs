using UnityEngine;

// 敵クラス
public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip wrong;
    [SerializeField] private GameObject effect;

    private float speed;    // 敵の移動スピード
    private float circle_speed;    // 円運動のスピード

    private Sprite item;    // アイテムスプライト
    private Sprite item_character;    // アイテム文字スプライト
    private int type;    // 敵番号
    private int pattern;    // 敵の移動パターン
    private float max_distance;    // 敵の進む最大距離
    private float distance;    // 敵の進んだ距離
    private int direction;    // 敵の移動の向き
    private Vector3 center;    // 円移動の中心ベクトル
    private float side;    // 四角形1辺の長さ
    private Vector3[] squares;    // 四角形の移動基準4点
    private int index;    // 四角形の4点の添え字

    private GameObject busyu;    // bulletオブジェクト
    private GameObject question;    // questionオブジェクト
    private GameObject timer;    // timerオブジェクト

    private bool changing;    // 音楽を鳴らすフラグ
    private Sprite effect_sprite;
    private AudioSource audiosource;    // 正解・不正解のサウンド


    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトを探索
        busyu = GameObject.Find("Busyu");
        question = GameObject.Find("Yomi");
        timer = GameObject.Find("Timer");

        speed = 0.75f;
        circle_speed = 15.0f;

        distance = 0;
        changing = false;

        effect.GetComponent<SpriteRenderer>().sprite = effect_sprite;
        audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // 敵の移動パターンで場合分け
        switch(pattern) {
            // 水平移動
            case 0:
            case 3:
            case 6:
            case 9:
                if(distance >= max_distance) {
                    direction = -(direction);
                    distance = 0;
                }
                distance += speed * Time.deltaTime;
                transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
                break;
            // 正方形に沿って移動
            case 1:
            case 10:
                if(transform.position == squares[index]) {
                    index++;
                    if(index > 3) {
                        index = 0;
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, squares[index], speed * Time.deltaTime);
                break;
            // 円移動に沿って移動(z軸に回転)
            case 2:
            case 4:
            case 5:
            case 7:
            case 8:
            case 11:
                transform.RotateAround(center, Vector3.forward, direction * circle_speed * Time.deltaTime);
                transform.rotation = Quaternion.identity;
                break;
            // 例外処理 : 移動なし
            default:
                break;
        }
        

    }

    // 敵がタップされた場合に呼び出される関数
    public void onClick()
    {
        // 敵番号と部首番号が一致 かつ 敵タグと問題タグが一致している場合
        if(type == busyu.GetComponent<Busyu>().GetBullet_Num() && gameObject.tag == question.tag) {
            gameObject.GetComponent<SpriteRenderer>().sprite = item;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item_character;
            question.GetComponent<Question>().ChangeQuestion();
            timer.GetComponent<Timer>().AddTime();
            changing = true;
            Instantiate(effect, transform.position, Quaternion.identity);
            audiosource.PlayOneShot(correct);
        }
        else if(!changing){
            audiosource.PlayOneShot(wrong);
        }
    }

    // アイテムスプライトを返却する関数
    public Sprite GetItem()
    {
        return this.item;
    }

    // アイテムスプライトを取得する関数
    public void SetItem(Sprite sprite)
    {
        this.item = sprite;
    }

    // アイテム文字スプライトを返却する関数
    public Sprite GetItem_Character()
    {
        return this.item_character;
    }

    // アイテム文字スプライトを取得する関数
    public void SetItem_Character(Sprite sprite)
    {
        this.item_character = sprite;
    }

    public void SetEffect(Sprite sprite)
    {
        this.effect_sprite =  sprite;
    }

    // 敵番号を返却する関数
    public int GetNum()
    {
        return this.type;
    }

    // 敵番号を取得する関数
    public void SetType(int num)
    {
        this.type = num;
    }

    // 敵の移動パターンを取得する関数
    public void SetPattern(int num)
    {
        this.pattern = num;
    }

    // 敵の進む最大距離を取得する関数
    public void SetDistance(float num)
    {
        this.max_distance = num;
    }

    // 敵の向きを取得する関数
    public void SetDirection(int num)
    {
        this.direction = num;
    }

    // 四角形1辺の長さを取得する関数
    public void SetSquareSide(float num)
    {
        this.side = num;

        // 四角形1辺の長さが0以上の場合に処理
        if(side != 0) {
            squares = new Vector3[4];
            squares[0] = new Vector3(side / 2, side / 2, 0);
            squares[2] = new Vector3(-side / 2, -side / 2, 0);

            if(direction == 1) {
                squares[1] = new Vector3(-side / 2, side / 2, 0);
                squares[3] = new Vector3(side / 2, -side / 2, 0);
            }
            else {
                squares[3] = new Vector3(-side / 2, side / 2, 0);
                squares[1] = new Vector3(side / 2, -side / 2, 0);
            }

            if(direction == 1) {
                if(transform.position.x == side / 2 && transform.position.y != side / 2) {
                    index = 0;
                }
                else if(transform.position.x != -side / 2 && transform.position.y == side / 2) {
                    index = 1;
                }
                else if(transform.position.x == -side / 2 && transform.position.y != -side / 2) {
                    index = 2;
                }
                else {
                    index = 3;
                }
            }
            else {
                if(transform.position.x == side / 2 && transform.position.y != -side / 2) {
                    index = 1;
                }
                else if(transform.position.x != -side / 2 && transform.position.y == -side / 2) {
                    index = 2;
                }
                else if(transform.position.x == -side / 2 && transform.position.y != side / 2) {
                    index = 3;
                }
                else {
                    index = 0;
                }
            }
        }
    }

    // 円移動の中心座標を取得する関数
    public void SetCenter(Vector3 num)
    {
        this.center = num;
    }
}