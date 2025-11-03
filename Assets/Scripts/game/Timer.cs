using UnityEngine;
using UnityEngine.UI;

// タイマークラス
public class Timer : MonoBehaviour
{
    [SerializeField] private Sprite[] number;
    [SerializeField] private Image time_10;
    [SerializeField] private Image time_0;
    [SerializeField] private Image question;

    private float count_time;
    private bool stop;
    private int image_10;
    private int image_0;


    // Start is called before the first frame update
    void Start()
    {
        count_time = 61.0f;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(count_time > 0 && !stop) {
            image_10 = Mathf.FloorToInt(count_time / 10);
            image_0 = Mathf.FloorToInt(count_time % 10);
            time_10.GetComponent<Image>().sprite = number[image_10];
            time_0.GetComponent<Image>().sprite = number[image_0];

            count_time -= Time.deltaTime;
        }

        if (image_10 == 0 && image_0 == 0){
            question.GetComponent<Question>().ChangeGameOver();
        }
    }

    // タイマーを加算する関数
    public void AddTime()
    {
        count_time += 3.00f;
    }

    // タイマーを止める関数
    public void StopTimer()
    {
        stop = true;
    }
}
