using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;


    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を右端に設定
        SetScrollPosition(1f);
    }

    private void SetScrollPosition(float position)
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.horizontalNormalizedPosition = position;
    }
}
