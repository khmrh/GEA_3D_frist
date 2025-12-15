using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHarvester : MonoBehaviour
{
    public float rayDistance = 5f;
    public LayerMask hitMask = ~0;
    public int toolDamge = 1;
    public float hitCooldown = 0.15f;
    private float _nextHitTime;
    private Camera _cam;
    public Inventory inventory;
    InventoryUI invenUI;
    public GameObject selectedBlock;

    void Awake()
    {
        _cam = Camera.main;
        if (inventory == null) inventory = gameObject.AddComponent<Inventory>();
        invenUI = FindObjectOfType<InventoryUI>();
    }

    void Update()
    {
        if (invenUI.selectedIndex < 0)
        {
            selectedBlock.transform.localScale = Vector3.zero;
            if (Input.GetMouseButtonDown(0) && Time.time >= _nextHitTime)
            {
                _nextHitTime = Time.time + hitCooldown;

                Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
                if (Physics.Raycast(ray, out var hit, rayDistance, hitMask))
                {
                    var block = hit.collider.GetComponent<Block>();
                    if (block != null)
                    {
                        block.Hit(toolDamge, inventory);
                    }
                }
            }
        }
        else
        {
            Ray rayDebug = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(rayDebug, out var hitDebug, rayDistance, hitMask, QueryTriggerInteraction.Ignore))
            {
                //Debug.DrawRay(hitDbug.point, hitDebug.nomal, color.red, 2f);
                Vector3Int placePos = AdjacentCellOnHitFace(hitDebug);
                selectedBlock.transform.localScale = Vector3.one;
                selectedBlock.transform.position = placePos;
                selectedBlock.transform.rotation = Quaternion.identity;
            }
            else
            {
                selectedBlock.transform.localScale = Vector3.zero;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out var hit, rayDistance, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Vector3Int PlacePos = AdjacentCellOnHitFace(hit);

                    ItemType selected = invenUI.GetInventorySlot();
                    if (inventory.Consume(selected, 1))
                    {
                        FindObjectOfType<NoisevoxcelMap>().PleaceTile(PlacePos, selected);
                    }
                }
            }
        }
    }
        
    static Vector3Int AdjacentCellOnHitFace(in RaycastHit hit)
    {
        Vector3 baseCenter = hit.collider.transform.position;
        Vector3 adjCentrt = baseCenter + hit.normal;
        return Vector3Int.RoundToInt(adjCentrt);
    }
}
