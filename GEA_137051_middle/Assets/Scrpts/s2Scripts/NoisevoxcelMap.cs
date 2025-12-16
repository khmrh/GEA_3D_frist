using UnityEngine;

public class NoisevoxcelMap : MonoBehaviour
{

    public GameObject blockPrefabDirt;
    public GameObject blockPrefabGrass;
    public GameObject blockPrefabwater;
    public GameObject blockPrefabWood;
    public GameObject blockPrefabIron;
    public GameObject blockPrefabDiamond;
    public int width = 20;
    public int depth = 20;
    public int maxHeight = 16;
    public int waterLevel = 4;
    public int woodLevel = 3;
    public int IronLevel = 8;
    public int diamondLevel = 4;

    [SerializeField] float noiseScale = 20f;

    void Start()
    {
        float offsetX = Random.Range(-9999f, 9999f);
        float offsetZ = Random.Range(-9999f, 9999f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetZ) / noiseScale;
                float noise = Mathf.PerlinNoise(nx, nz);
                int h = Mathf.FloorToInt(noise * maxHeight);
                if (h <= 0) h = 1;
                for (int y = 0; y <= h; y++)
                {
                    if (y == h)
                        PlaseGrass(x, y, z);
                    else
                        PlaseDirt(x, y, z);
                }
                for (int y = h + 1; y <= waterLevel; y++)
                {
                    PlaseWater(x, y, z);
                }
                for (int y = h + 1; y <= woodLevel; y++)
                {
                    PlaseWood(x, y, z);
                }
                for (int y = h - 5; y <= IronLevel; y++)
                {
                    PlaseIron(x, y, z);
                }
                for (int y = h - 10; y <= diamondLevel; y++)
                {
                    PlaseDiamond(x, y, z);
                }
            }
        }
    }

    private void PlaseWater(int x, int y, int z)
    {
        var go = Instantiate(blockPrefabwater, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Water_{x}_{y}_{z}";
    }

    private void PlaseDirt(int x, int y, int z)
    {
        var go = Instantiate(blockPrefabDirt, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Dirt_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Dirt;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }

    private void PlaseGrass(int x, int y, int z)
    {
        var go = Instantiate(blockPrefabGrass, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Grass_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Grass;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }

    private void PlaseWood(int x, int y, int z)
    {
        var go = Instantiate(blockPrefabWood, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Wood_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Wood;
        b.maxHP = 5;
        b.dropCount = 1;
        b.mineable = true;
    }

    private void PlaseIron(int x, int y, int z)
    {
        var go = Instantiate(blockPrefabIron, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Iron_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Iron;
        b.maxHP = 10;
        b.dropCount = 1;
        b.mineable = true;
    }

    private void PlaseDiamond(int x, int y, int z)
    {
        var go = Instantiate(blockPrefabDiamond, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Diamond_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Diamond;
        b.maxHP = 15;
        b.dropCount = 1;
        b.mineable = true;
    }

    public void PleaceTile(Vector3Int pos, ItemType type)
    {
        switch (type)
        {
            case ItemType.Dirt:
                PlaseDirt(pos.x, pos.y, pos.z);
                break;
            case ItemType.Grass:
                PlaseGrass(pos.x, pos.y, pos.z);
                break;
            case ItemType.Water:
                PlaseWater(pos.x, pos.y, pos.z);
                break;
        }
    }
}
