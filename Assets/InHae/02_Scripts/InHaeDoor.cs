using UnityEngine;

public class InHaeDoor : Interactable
{
    [SerializeField] private Vector3 vec;
    public override void Interact()
    {
        PoolManager.SpawnFromPool("ming", transform.position);
    }
}
