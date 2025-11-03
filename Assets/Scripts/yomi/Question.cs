using UnityEngine;
using UnityEngine.UI;

// 問題クラス
public class Question : MonoBehaviour
{
    [SerializeField] private Gamemgrdata game;
    [SerializeField] private Stagemgrdata[] stagedatas;
    [SerializeField] private Image question_character;
    [SerializeField] private Image frame_0;
    [SerializeField] private Image frame_1;
    [SerializeField] private Sprite gameclear;
    [SerializeField] private Sprite gameover;
    [SerializeField] private GameObject timer;
    [SerializeField] private Questionmgrdata[] questiondatas;
    [SerializeField] private GameObject retry;
    [SerializeField] private GameObject retitle;

    private Sprite[] questions;    // 問題スプライト
    private int[] tag_num;    // 問題のタグ番号
    private Questionmgrdata questiondata;    // 問題データ
    private Stagemgrdata stagedata;    // ステージデータ
    private string filepath;    // 問題管理データのファイルパス
    private int num;    // 問題番号

    private StageClearMgr stagecleardata;


    // Start is called before the first frame update
    void Start()
    {
        // レベルで問題データを場合分け
        switch(game.GetLevel()) {
            case 0:
                questiondata = questiondatas[0];
                stagedata = stagedatas[0];
                break;
            case 1:
                questiondata = questiondatas[1];
                stagedata = stagedatas[1];
                break;
            case 2:
                questiondata = questiondatas[2];
                stagedata = stagedatas[2];
                break;
            case 3:
                questiondata = questiondatas[3];
                stagedata = stagedatas[3];
                break;
            default:
                questiondata = questiondatas[0];
                stagedata = stagedatas[0];
                break;
        }

        frame_0.GetComponent<Image>().sprite = stagedata.GetFrame();
        frame_1.GetComponent<Image>().sprite = stagedata.GetFrame();

        // retry.SetActive(false);
        retitle.SetActive(false);

        questiondata.SetQuestion();    // 問題データの初期化
        questions = questiondata.GetQuestion();

        // 問題数だけ、タグ番号を取得
        tag_num = new int[questiondata.GetQuestion_Length()];
        for(int i = 0; i < questions.Length; i++) {
            tag_num[i] = i;
        }

        ShuffleQuestion(questions, tag_num);    // 問題の順序を変更

        // 一問目の問題を設定
        num = 0;
        question_character.tag = "enemy_" + tag_num[num];
        question_character.transform.GetChild(0).GetComponent<Image>().sprite = questions[num];
        question_character.transform.GetChild(0).GetComponent<Image>().SetNativeSize();

        GameObject dontDestroyObject = GameObject.Find("StageClearMgr");
        if (dontDestroyObject != null)
        {
            stagecleardata = dontDestroyObject.GetComponent<StageClearMgr>();
        }

    }

    // 問題の順序をランダムにする関数
    private void ShuffleQuestion(Sprite[] questions, int[] tag_num)
    {
        // フィッシャー-イェーツのシャッフル(n-1から減少させ、ランダムに問題を入れ替え)
        for(int i = questions.Length - 1; i > 0; i--) {
            int random = UnityEngine.Random.Range(0, i + 1);

            // 問題スプライトを入れ替え
            var temp_sprite = questions[i];
            questions[i] = questions[random];
            questions[random] = temp_sprite;

            // 問題のタグ番号を入れ替え
            var temp_tag = tag_num[i];
            tag_num[i] = tag_num[random];
            tag_num[random] = temp_tag;
        }
    }

    // 問題を変更する関数
    public void ChangeQuestion()
    {
        num++;

        // 問題が残っている場合は、問題を更新。そうでなければ"ゲームクリア"を表示
        if(num < questions.Length) {
            question_character.tag = "enemy_" + tag_num[num];
            question_character.transform.GetChild(0).GetComponent<Image>().sprite = questions[num];
            question_character.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
        }
        else {
            question_character.tag = "Finish";
            question_character.transform.GetChild(0).GetComponent<Image>().sprite = gameclear;
            question_character.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
            timer.GetComponent<Timer>().StopTimer();
            if (stagecleardata != null) {
                stagecleardata.SetStageCleared(game.GetLevel(), game.GetStage(), true);
            }
            EndGame();
        }
    }

    // ゲームオーバーを表示する関数
    public void ChangeGameOver()
    {
        question_character.tag = "Finish";
        question_character.transform.GetChild(0).GetComponent<Image>().sprite = gameover;
        question_character.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
        timer.GetComponent<Timer>().StopTimer();
        EndGame();
    }

    private void EndGame()
    {
        retry.SetActive(true);
        retitle.SetActive(true);
    }
}
