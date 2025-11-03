using UnityEngine;
using UnityEngine.UI;

// 設定画面クラス
public class Option : MonoBehaviour
{
    [SerializeField] private Image left;    // 左ボタンのチェック
    [SerializeField] private Image right;    // 右ボタンのチェック

    private BusyuMgr busyuMgrData;    // 弾データ


    void Start()
    {
        // 弾データを取得
        GameObject dontDestroyObject = GameObject.Find("BusyuMgr");
        if (dontDestroyObject != null)
        {
            busyuMgrData = dontDestroyObject.GetComponent<BusyuMgr>();

            // 弾データによって、チェックの表示を設定
            if (busyuMgrData.GetComponent<BusyuMgr>().GetBusyuIsLeft())
            {
                SetLeft();
            }
            else
            {
                SetRight();
            }
        }
        else
        {
            SetLeft();
        }
    }

    // 左ボタンの処理
    public void OnLeftClick()
    {
        if (busyuMgrData != null)
        {
            busyuMgrData.GetComponent<BusyuMgr>().SetBulletPosition(true);
            SetLeft();
        }
    }

    // 右ボタンの処理
    public void OnRightClick()
    {
        if (busyuMgrData != null)
        {
            busyuMgrData.GetComponent<BusyuMgr>().SetBulletPosition(false);
            SetRight();
        }
    }

    // 左の設定
    private void SetLeft()
    {
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(false);
    }

    // 右の設定
    private void SetRight()
    {
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(true);
    }
}
