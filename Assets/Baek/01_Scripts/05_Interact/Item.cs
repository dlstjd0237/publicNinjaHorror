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
            //Ŭ�����带 �������� �ƴϰų�,�κ��丮�� �������� �ְų�, ��ġ�������� ���� 
            return;
        inventory.TakeItem(_itemSO);// ������ ������ �κ��丮�� ����
        gameObject.SetActive(false);


    }

    void IItem.Deployment()
    {
        GetComponent<Outline>().enabled = false;
        _deployment = true;
        promptMessage = string.Empty;
    }
}
