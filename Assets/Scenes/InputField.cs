using UnityEngine;
using TMPro;

public class JapaneseInputField : MonoBehaviour
{
    public TMP_InputField inputField; // TextMeshPro InputFieldへの参照
    public TextMeshPro textMeshPro; // 結果を表示するTextMeshProオブジェクト

    public void OnTextChanged()
    {
        // 入力フィールドのテキストを取得して表示
        textMeshPro.text = inputField.text;
    }
}
