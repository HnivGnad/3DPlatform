using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject topBlockPrefab;    // Lớp cỏ trên cùng
    public GameObject bottomBlockPrefab; // Lớp đất phía dưới

    [Header("Grid Settings")]
    [Tooltip("Số lượng khối theo chiều ngang (Trục X)")]
    public int width = 5;
    
    [Tooltip("Số lượng lớp khối theo chiều thẳng đứng (Trục Y)")]
    public int depth = 3; 
    
    [Tooltip("Số lượng khối theo chiều dọc (Trục Z)")]
    public int length = 10;

    public float blockSize = 1f;

    [ContextMenu("Generate Ground")]
    public void GenerateGround()
    {
        ClearGround();

        if (topBlockPrefab == null || bottomBlockPrefab == null)
        {
            Debug.LogError("Vui lòng gán đầy đủ Prefabs cho Top và Bottom Block!");
            return;
        }

        GameObject container = new GameObject("Ground_Container");
        container.transform.parent = this.transform;
        container.transform.localPosition = Vector3.zero;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                for (int y = 0; y < depth; y++)
                {
                    Vector3 spawnPos = new Vector3(
                        (x - width / 2.0f + 0.5f) * blockSize,
                        -y * blockSize,
                        (z - length / 2.0f + 0.5f) * blockSize
                    );

                    GameObject prefabToUse = (y == 0) ? topBlockPrefab : bottomBlockPrefab;
                    GameObject newBlock = Instantiate(prefabToUse, spawnPos, Quaternion.identity);
                    
                    newBlock.transform.parent = container.transform;
                    newBlock.name = $"Block_{x}_{y}_{z}";
                }
            }
        }
        Debug.Log($"Đã tạo xong mặt đất: {width}x{depth}x{length}");
    }

    [ContextMenu("Clear Ground")]
    public void ClearGround()
    {
        Transform container = transform.Find("Ground_Container");
        if (container != null)
        {
            DestroyImmediate(container.gameObject);
        }
    }
}
