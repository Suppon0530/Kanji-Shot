using UnityEngine;

// エフェクトクラス
public class Effect : MonoBehaviour
{
    private float initialSize = 0.1f;
    private float maxSize = 2.0f;
    private float animationDuration = 1.0f;
    private float startTime;
    private SpriteRenderer effectSpriteRenderer;


    void Start()
    {
        effectSpriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = Vector3.one * initialSize;
        startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;
        float t = Mathf.Clamp01(elapsedTime / animationDuration);

        // サイズを徐々に大きく
        float newSize = Mathf.Lerp(initialSize, maxSize, t);
        transform.localScale = Vector3.one * newSize;

        // 透明度を徐々に小さく
        Color color = effectSpriteRenderer.color;
        color.a = Mathf.Lerp(1.0f, 0.0f, t);
        effectSpriteRenderer.color = color;

        // アニメーション終了後にGameObjectを破棄
        if (t >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
