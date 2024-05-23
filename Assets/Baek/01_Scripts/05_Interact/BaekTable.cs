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
            //�÷��̾ �������� ������ ���� ������ �� ������ ���� ������
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
        promptMessage = "[�ƹ��ϵ� �Ͼ�� �ʾҴ�.]";
        yield return new WaitForSeconds(1.5f);
        promptMessage = defaultMessage;
    }

    private bool QuestChake()
    {
        for (int i = 0; i < _questID.Count; i++)
        {
            if (_questID[i] == item.QuestID)//���� ������ �ִ� �����۰� ����Ʈ�� ��ġ�ϸ� true
            {
                return true;
            }
        }
        return false;
    }
}
