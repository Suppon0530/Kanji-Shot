using UnityEngine;
using UnityEngine.SceneManagement;

// ゲームシーンクラス
public class Game : MonoBehaviour
{
    // タイトルボタン選択時の処理
    public void OnReturnClick()
    {
        SceneManager.LoadScene("title");    // タイトルシーンのロード
    }

    // 再挑戦ボタン選択時の処理
    public void OnRetryClick()
    {
        SceneManager.LoadScene("stage");    // ステージ選択シーンのロード
    }
}
