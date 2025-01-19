using UnityEngine;

public class RockyIceMaterial : MonoBehaviour
{
    public Texture2D normalMap;

    void Start()
    {
        // マテリアル作成
        Material iceMaterial = new Material(Shader.Find("Standard"));
        iceMaterial.color = new Color(0.5f, 0.8f, 1f, 0.6f); // 青みがかった透明色
        iceMaterial.SetFloat("_Smoothness", 0.7f); // 滑らかさ
        iceMaterial.SetFloat("_Metallic", 0.3f); // メタリック感

        // ノーマルマップの適用
        if (normalMap != null)
        {
            iceMaterial.SetTexture("_BumpMap", normalMap);
            iceMaterial.EnableKeyword("_NORMALMAP");
        }

        // オブジェクトにマテリアルを適用
        GetComponent<Renderer>().material = iceMaterial;
    }
}
