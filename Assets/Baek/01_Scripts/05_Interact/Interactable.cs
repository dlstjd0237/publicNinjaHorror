using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;
    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interact");
    }
    public void BassInteract()
    {
        Interact();
    }

    public abstract void Interact();


}