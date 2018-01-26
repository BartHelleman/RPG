using System.Collections;
using UnityEngine;

public enum HarvestableType { Tree, Rock }

public class Harvestable : Interactable
{
    [SerializeField] private Item _dropItem;
    [SerializeField] private int _dropAmount;
    [SerializeField] private GameObject _baseLoot;
    [SerializeField] private float _harvestTime = 3f;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Harvesting...");

        StartCoroutine(Harvest());
    }

    private IEnumerator Harvest()
    {
        yield return new WaitForSeconds(_harvestTime);

        GameObject loot = Instantiate(_baseLoot, transform.position + Vector3.up, transform.rotation);
        Item[] items = new Item[_dropAmount];

        for (int i = 0; i < _dropAmount; i++)
        {
            items[i] = _dropItem;
        }

        loot.GetComponent<Loot>().ItemsToLoot = items;
        
        Destroy(gameObject);
    }
}
