using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeaninglessInteract : Interactable
{
    private bool _isInteract;
    private string _originalMessage;
    [SerializeField] private string _interactMessage;
    private void Awake()
    {
        _originalMessage = promptMessage;
    }
    public override void Interact()
    {
        if (_isInteract == false)
        {
            _isInteract = true;
            promptMessage = _interactMessage;
            StartCoroutine(nameof(InteractCoroutine));
        }

    }

    private IEnumerator InteractCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        _isInteract = false;
        promptMessage = _originalMessage;
    }


}
