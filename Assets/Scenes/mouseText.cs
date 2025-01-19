using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro用の名前空間

public class MouseToTextObject : MonoBehaviour
{
    private List<Vector2> mousePositions = new List<Vector2>();
    public TextMeshPro textMeshPro; // TextMeshProコンポーネントへの参照

    void Update()
    {
        // マウスの左ボタンが押されている間だけ動きを記録
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePositions.Add(mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // ボタンを離したらテキストに変換
            string recognizedText = RecognizeMousePath(mousePositions);
            textMeshPro.text = recognizedText; // TextMeshProオブジェクトに結果を表示

            // 軌跡データをリセット
            mousePositions.Clear();
        }
    }

    private string RecognizeMousePath(List<Vector2> positions)
    {
        // 簡易的な例: 動きの長さで文字を判定
        float totalDistance = 0;
        for (int i = 1; i < positions.Count; i++)
        {
            totalDistance += Vector2.Distance(positions[i - 1], positions[i]);
        }

        // 距離に基づいて簡易的な文字判定
        if (totalDistance < 100)
            return "Short movement";
        else if (totalDistance < 300)
            return "Medium movement";
        else
            return "Long movement";
    }
}
