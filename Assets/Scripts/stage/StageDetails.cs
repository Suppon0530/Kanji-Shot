using UnityEngine;
using UnityEngine.UI;

// ステージ詳細クラス
public class StageDetails : MonoBehaviour
{
    [SerializeField] private Image detail;

    private Sprite[] details;    // ステージ詳細のスプライト


    // ステージ詳細のスプライトを設定する関数
    public void SetStageDetails(int stage)
    {
        detail.GetComponent<Image>().sprite = details[stage];
    }

    // ステージ詳細のスプライトを取得する関数
    public void SetDetails(Sprite[] num)
    {
        this.details = num;
    }
}
