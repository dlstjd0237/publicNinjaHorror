using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemDataSO _currentItemSO;
    public ItemDataSO CurrentItemSO { get => _currentItemSO; set => _currentItemSO = value; }

    private Image _itemImage;
    private Color _imageSetColor, _imageNoneColor;

    private void Awake()
    {
        _itemImage = transform.Find("Canvas/ItemOutline/ItemSprite/item").GetComponent<Image>();
        _imageSetColor = _itemImage.color;
        _imageSetColor.a = 1;
        _imageNoneColor = _itemImage.color;
        _imageNoneColor.a = 0;
    }


    public void TakeItem(ItemDataSO so)
    {
        _currentItemSO = so;
        ItemImageSet();
    }

    public ItemDataSO DeploymentItem()
    {
        if (_currentItemSO == null) // 여기 아래부터는 아이템을 가지고 있는 상태인거임
            return null;

        return _currentItemSO;
    }

    public void ItemImageSet()
    {
        _itemImage.color = _imageSetColor;
        _itemImage.sprite = _currentItemSO.ItemSprit;
    }
    public void DropItem(Transform dropTrm)
    {
        if (_currentItemSO == null)
            return;
        Instantiate(_currentItemSO.ItemPrefab, dropTrm.position, Quaternion.identity);
        _currentItemSO = null;
        ItemImageReSet();
    }
    public void ItemImageReSet()
    {
        _itemImage.color = _imageNoneColor;
        _itemImage.sprite = null;
    }




}
