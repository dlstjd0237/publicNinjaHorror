using UnityEngine;

public class ManagerDialogue1 : Interactable
{
    [SerializeField] private DialogueData _data;
    private Animator _animator;
    private bool _isTalk;
    private string OriginalMassage;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        OriginalMassage = promptMessage;
        gameObject.layer = LayerMask.NameToLayer("Interact");
    }

    public override void Interact()
    {
        if (_isTalk == false)
        {
            _isTalk = true;
            DialogueManager.StartDialogue(_data, _animator,
                () => promptMessage = string.Empty, () => { _isTalk = false; promptMessage = OriginalMassage; GameObject.Find("NPCSpawn").GetComponent<NPCSpawn>().SpawnNPC(); Destroy(gameObject); });
        }
    }
}
