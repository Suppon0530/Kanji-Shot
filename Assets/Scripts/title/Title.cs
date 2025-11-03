using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// タイトルクラス
public class Title : MonoBehaviour
{
    [SerializeField] private Image howto;    // 遊び方画面
    [SerializeField] private Image option;    // 設定画面


    // プレイボタン選択時の処理
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Level");
    }

    // 遊び方ボタン選択時の処理
    public void OnHowtoButtonClick()
    {
        howto.gameObject.SetActive(true);
    }

    // 設定ボタン選択時の処理
    public void OnOptionButtonClick()
    {
        option.gameObject.SetActive(true);
    }

    // 戻るボタン選択時の処理(遊び方画面)
    public void OnHowtoReturnClick()
    {
        howto.gameObject.SetActive(false);
    }

    // 戻るボタン選択時の処理(設定画面)
    public void OnOptionReturnClick()
    {
        option.gameObject.SetActive(false);
    }
}