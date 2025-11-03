using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ステージクラス
public class Stage : MonoBehaviour
{
    [SerializeField] private Gamemgrdata stagedata;    // ゲームデータを管理する型の変数
    [SerializeField] private Image details;    // ステージ選択後のウインドウ


    // ステージ選択時の処理
    public void OnButtonClick(int num)
    {
        // 24種類のステージ
        switch(num) {
            case 0:
                stagedata.SetStage(0);
                break;
            case 1:
                stagedata.SetStage(1);
                break;
            case 2:
                stagedata.SetStage(2);
                break;
            case 3:
                stagedata.SetStage(3);
                break;
            case 4:
                stagedata.SetStage(4);
                break;
            case 5:
                stagedata.SetStage(5);
                break;
            case 6:
                stagedata.SetStage(6);
                break;
            case 7:
                stagedata.SetStage(7);
                break;
            case 8:
                stagedata.SetStage(8);
                break;
            case 9:
                stagedata.SetStage(9);
                break;
            case 10:
                stagedata.SetStage(10);
                break;
            case 11:
                stagedata.SetStage(11);
                break;
            case 12:
                stagedata.SetStage(12);
                break;
            case 13:
                stagedata.SetStage(13);
                break;
            case 14:
                stagedata.SetStage(14);
                break;
            case 15:
                stagedata.SetStage(15);
                break;
            case 16:
                stagedata.SetStage(16);
                break;
            case 17:
                stagedata.SetStage(17);
                break;
            case 18:
                stagedata.SetStage(18);
                break;
            case 19:
                stagedata.SetStage(19);
                break;
            case 20:
                stagedata.SetStage(20);
                break;
            case 21:
                stagedata.SetStage(21);
                break;
            case 22:
                stagedata.SetStage(22);
                break;
            case 23:
                stagedata.SetStage(23);
                break;
            default:
                break;
        }

        details.GetComponent<StageDetails>().SetStageDetails(num);
        details.gameObject.SetActive(true);
    }

    // 戻るボタン選択時の処理
    public void OnReturnClick()
    {
        SceneManager.LoadScene("level");
    }

    // スタート開始ボタン選択時の処理
    public void OnStartClick()
    {
        SceneManager.LoadScene("game");
    }

    // 戻るボタン選択時の処理
    public void OnBackClick()
    {
        details.gameObject.SetActive(false);
    }
}
