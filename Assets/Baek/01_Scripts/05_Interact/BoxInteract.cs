using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteract : Interactable
{
    public override void Interact()
    {
        Debug.Log("상호작용");
        gameObject.SetActive(false);    
    }

   
}
