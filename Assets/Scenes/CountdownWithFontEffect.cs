using UnityEngine;
using TMPro;

public class Countdown3D : MonoBehaviour
{
    public TextMeshPro countdownText; // 3D用のTextMeshProオブジェクト
    public float countdownTime = 10f; // カウントダウンの開始時間

    private float currentTime;        // 現在のカウントダウン時間

    void Start()
    {
        if (countdownText == null)
        {
            Debug.LogError("TextMeshProがアタッチされていません！");
            return;
        }

        currentTime = countdownTime;  // カウントダウン時間を初期化
        UpdateCountdownText();        // 初期のテキストを更新
    }

    void Update()
    {
        if (currentTime > 0)
        {
            // 時間を減少させる
            currentTime -= Time.deltaTime;

            // 残り時間が0以下になったら停止
            if (currentTime <= 0)
            {
                currentTime = 0;
            }

            UpdateCountdownText();
        }
    }

    void UpdateCountdownText()
    {
        // カウントダウンの数値を更新
        countdownText.text = Mathf.Ceil(currentTime).ToString();
    }
}
