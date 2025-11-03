using UnityEngine;
using UnityEngine.SceneManagement;

// レベルクラス
public class Level : MonoBehaviour
{
    [SerializeField] private Gamemgrdata leveldata;    // ゲームデータを管理する型の変数

    // レベル選択時の処理
    public void OnButtonClick(int num)
    {
        // 0: 易, 1: 並, 2: 難, 3: 極
        switch(num) {
            case 0:
                leveldata.SetLevel(0);
                break;
            case 1:
                leveldata.SetLevel(1);
                break;
            case 2:
                leveldata.SetLevel(2);
                break;
            case 3:
                leveldata.SetLevel(3);
                break;
            default:
                break;
        }

        SceneManager.LoadScene("stage");    // ステージ選択シーンのロード
    }

    // 戻るボタン選択時の処理
    public void OnReturnClick()
    {
        SceneManager.LoadScene("title");    // タイトルシーンのロード
    }
}
