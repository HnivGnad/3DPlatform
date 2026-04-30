using UnityEngine;

public class BlockGridGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject topBlockPrefab;
    public GameObject baseBlockPrefab;

    [Header("Grid Settings")]
    public Vector3Int gridSize = new Vector3Int(3, 3, 3);
    public float blockSize = 1f;

    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {
        ClearGrid();

        if (baseBlockPrefab == null || topBlockPrefab == null)
        {
            Debug.LogError("Vui lòng gán cả Base Block Prefab và Top Block Prefab!");
            return;
        }

        GameObject container = new GameObject("GeneratedGrid");
        container.transform.parent = this.transform;
        container.transform.localPosition = Vector3.zero;

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    // Chọn prefab: Tầng trên cùng dùng topBlockPrefab, còn lại dùng baseBlockPrefab
                    GameObject prefabToUse = (y == gridSize.y - 1) ? topBlockPrefab : baseBlockPrefab;

                    Vector3 spawnPos = new Vector3(
                        x * blockSize,
                        y * blockSize,
                        z * blockSize
                    );

                    GameObject newBlock = Instantiate(prefabToUse, container.transform);
                    newBlock.transform.localPosition = spawnPos;
                    newBlock.name = $"Block_{x}_{y}_{z}";
                }
            }
        }

        Debug.Log($"Đã tạo xong lưới khối {gridSize.x}x{gridSize.y}x{gridSize.z}!");
    }

    [ContextMenu("Clear Grid")]
    public void ClearGrid()
    {
        Transform oldContainer = transform.Find("GeneratedGrid");
        if (oldContainer != null)
        {
            if (Application.isPlaying)
                Destroy(oldContainer.gameObject);
            else
                DestroyImmediate(oldContainer.gameObject);
        }
    }
}
