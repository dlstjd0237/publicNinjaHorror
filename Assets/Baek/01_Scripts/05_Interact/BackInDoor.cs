using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BackInDoor : Interactable
{

    [SerializeField] private Vector3 _moveRot;
    private Vector3 DefaultRot;
    private bool _open;
    public override void Interact()
    {
        if (_open)
        {
            transform.DOLocalRotate(_moveRot, 1);
        }
        else
        {
            transform.DOLocalRotate(DefaultRot, 1);
        }
        _open = _open == true ? false : true;
    }
}
