using UnityEngine;
using UnityEngine.UI;

// ステージを管理するクラス
public class StageMgr : MonoBehaviour
{
    [SerializeField] private Gamemgrdata gamedata;    // ゲーム管理データ
    [SerializeField] private Stagemgrdata[] stagedatas;    // ステージ管理データ
    [SerializeField] private Button[] buttons;    // ステージ選択のボタン
    [SerializeField] private Image details;    // ステージ選択後のウインドウ

    private Stagemgrdata stagedata;    // ステージデータ
    private StageClearMgr stagecleardata;    // ステージクリアデータ
    private Sprite stage;    // ステージスプライト

    // Start is called before the first frame update
    void Start()
    {
        // レベルで敵データの場合分け
        switch(gamedata.GetLevel()) {
            case 0:
                stagedata = stagedatas[0];
                break;
            case 1:
                stagedata = stagedatas[1];
                break;
            case 2:
                stagedata = stagedatas[2];
                break;
            case 3:
                stagedata = stagedatas[3];
                break;
            // 例外処理 : case 0
            default:
                stagedata = stagedatas[0];
                break;
        }

        stagedata.SetStage();

        // ステージクリアデータの取得
        GameObject dontDestroyObject = GameObject.Find("StageClearMgr");
        if (dontDestroyObject != null)
        {
            stagecleardata = dontDestroyObject.GetComponent<StageClearMgr>();
        }

        // ステージスプライトの設定
        stage = stagedata.GetStage();
        for(int i = 0; i < 24; i++) {
            buttons[i].GetComponent<Image>().sprite = stage;
            if(stagecleardata != null) {
                bool iscleared = stagecleardata.GetStageCleared(gamedata.GetLevel(), i);
                if(iscleared) {
                    buttons[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
                }
                else {
                    buttons[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
        // ステージ詳細スプライトの設定
        details.GetComponent<StageDetails>().SetDetails(stagedata.GetDetails());
        details.gameObject.SetActive(false);
    }
}
