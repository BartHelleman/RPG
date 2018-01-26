using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Item> Items = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    [SerializeField] private int _inventorySpace = 20;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of Inventory found");
            return;
        }

        Instance = this;
    }

    public bool Add(Item item)
    {
        if (Items.Count >= _inventorySpace)
        {
            Debug.Log("Not enough inventory space");
            return false;
        }

        Items.Add(item);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(Item item)
    {
        Items.Remove(item);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }
}
