using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteract : Interactable
{
    public override void Interact()
    {
        Debug.Log("��ȣ�ۿ�");
        gameObject.SetActive(false);    
    }

   
}
