using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteBin : Interactable
{
    [SerializeField] private int _garbageID;
    ItemDataSO item;
    Inventory inventory;
    public override void Interact()
    {
        inventory = SceneControlManager.Instance.Player.GetComponent<Inventory>();
        item = inventory.DeploymentItem();
        if (item == null)
            return;
        if (item.QuestID == _garbageID && !QuestManager.Instance.QuestClearChake(item.QuestID))
        {
            StopCoroutine("_noenItem");
            StartCoroutine("_noenItem");
            QuestManager.Instance.QuestProgressSet(item.QuestID);
        }
        else
        {
            StopCoroutine("_noenItem");
            StartCoroutine("_noenItem");
        }
    }

    private IEnumerator _noenItem()
    {
        var defaultMessage = promptMessage;
        promptMessage = $"[{item.ItemName}À» ¹ö·È´Ù.]";
        inventory.CurrentItemSO = null;
        inventory.ItemImageReSet();
        yield return new WaitForSeconds(1.5f);
        promptMessage = defaultMessage;
    }
}
