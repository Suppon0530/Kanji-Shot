using UnityEngine;

// サウンドクラス
public class SoundMgr : MonoBehaviour
{
    private static bool Dontdestroy = false;

    void Start()
    {
        if(!Dontdestroy) {
            DontDestroyOnLoad(this);
            Dontdestroy = true;
        }
        else {
            Destroy(this.gameObject);
            return;
        }
    }
}
