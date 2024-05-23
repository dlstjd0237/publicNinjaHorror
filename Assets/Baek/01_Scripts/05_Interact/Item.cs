using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Item : Interactable, IItem
{
    [SerializeField] private ItemDataSO _itemSO;
    private Inventory inventory;
    private bool _deployment;
    private void Awake()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();

    }
    public override void Interact()
    {
        if ((inventory.CurrentItemSO != null || _deployment == true)|| !QuestManager.Instance.GetClipboard)
            //클립보드를 보유중이 아니거나,인벤토리에 아이템이 있거나, 배치되있으면 리턴 
            return;
        inventory.TakeItem(_itemSO);// 아이템 정보를 인벤토리로 전송
        gameObject.SetActive(false);


    }

    void IItem.Deployment()
    {
        GetComponent<Outline>().enabled = false;
        _deployment = true;
        promptMessage = string.Empty;
    }
}
