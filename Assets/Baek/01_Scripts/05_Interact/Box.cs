using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    [SerializeField] private DialogueData qwer;
    public override void Interact()
    {
        Debug.Log("���ͷ�Ʈ��");
        DialogueManager.StartDialogue(qwer);
        gameObject.SetActive(false);
    }


}
