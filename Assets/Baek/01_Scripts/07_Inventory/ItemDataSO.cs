using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemDataSO")]

public class ItemDataSO : ScriptableObject
{
    [SerializeField] private Sprite _itemSprit; public Sprite ItemSprit { get { return _itemSprit; } }
    [SerializeField] private string _itemName; public string ItemName { get { return _itemName; } }
    [SerializeField] private GameObject _itemPrefab; public GameObject ItemPrefab { get { return _itemPrefab; } }
    [SerializeField] private int _questID; public int QuestID { get { return _questID; } }
}
