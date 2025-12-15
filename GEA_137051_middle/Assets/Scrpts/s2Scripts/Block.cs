using UnityEngine;

public enum ItemType { Dirt, Grass, Water, Fshovel, Tshovel, Sshovel}
public class Block : MonoBehaviour
{
    [Header("Block stat")]
    public ItemType type = ItemType.Dirt;
    public int maxHP = 3;
    [HideInInspector] public int hp;

    public int dropCount = 1;
    public bool mineable = true;

    void Awake()
    {
        hp = maxHP;
        if (GetComponent<Collider>() == null) gameObject.AddComponent<BoxCollider>();
        if (string.IsNullOrEmpty(gameObject.tag) || gameObject.tag == "UnTagged") 
            gameObject.tag = "Block";
    }

    public void Hit(int damage, Inventory inven)
    {
        if (!mineable) return;

        hp -= damage;

        if (hp <= 0)
        {
            if (inven != null && dropCount > 0)
                inven.add(type, dropCount);

            Destroy(gameObject);
        }
    }
}
