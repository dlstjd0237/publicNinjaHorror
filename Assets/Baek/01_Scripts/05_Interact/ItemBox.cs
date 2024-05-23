using UnityEngine;

public class ItemBox : Interactable
{
    private Inventory _inventory;
    [SerializeField] private ItemDataSO _so;
    private void Awake()
    {
        _inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }
    public override void Interact()
    {
        if (_inventory.CurrentItemSO != null)
            return;
        _inventory.TakeItem(_so);
    }

}
