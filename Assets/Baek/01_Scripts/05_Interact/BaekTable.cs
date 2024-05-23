using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaekTable : Interactable
{
    [SerializeField] private Transform _spawn;
    private string defaultMessage;
    [SerializeField] private List<int> _questID;
    private ItemDataSO item;
    private void Start()
    {
        defaultMessage = promptMessage;
    }
    public override void Interact()
    {
        Inventory inventory = SceneControlManager.Instance.Player.GetComponent<Inventory>();
        item = inventory.DeploymentItem();
        if ((item != null && !QuestManager.Instance.QuestClearChake(item.QuestID)) && QuestChake())
        {
            //플레이어가 아이템을 가지고 있지 않으면 널 있으면 뭐라도 들어가있음
            inventory.CurrentItemSO = null;
            QuestManager.Instance.QuestProgressSet(item.QuestID);
            GameObject tableItem = Instantiate(item.ItemPrefab, _spawn);
            tableItem.GetComponent<IItem>()?.Deployment();
            inventory.ItemImageReSet();
            Destroy(this);
        }
        else
        {
            StopCoroutine("ItemNone");
            StartCoroutine("ItemNone");
        }



    }

    private IEnumerator ItemNone()
    {
        promptMessage = "[아무일도 일어나지 않았다.]";
        yield return new WaitForSeconds(1.5f);
        promptMessage = defaultMessage;
    }

    private bool QuestChake()
    {
        for (int i = 0; i < _questID.Count; i++)
        {
            if (_questID[i] == item.QuestID)//현재 가지고 있는 아이템과 퀘스트가 일치하면 true
            {
                return true;
            }
        }
        return false;
    }
}
