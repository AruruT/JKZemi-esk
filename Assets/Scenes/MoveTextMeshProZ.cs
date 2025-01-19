using UnityEngine;

public class MoveTextMeshProZ : MonoBehaviour
{
    public float speed = 1.0f; // 移動速度
    public float targetZ = 0f; // 最終的なZ座標

    private Transform textTransform;

    void Start()
    {
        // TextMeshProオブジェクトのTransformを取得
        textTransform = transform;
    }

    void Update()
    {
        // 現在のZ座標を取得
        float currentZ = textTransform.position.z;

        // 徐々にZ座標を目標値に近づける
        if (Mathf.Abs(currentZ - targetZ) > 0.01f) // 誤差を許容
        {
            float newZ = Mathf.MoveTowards(currentZ, targetZ, speed * Time.deltaTime);
            textTransform.position = new Vector3(
                textTransform.position.x,
                textTransform.position.y,
                newZ
            );
        }
    }
}
