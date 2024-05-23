using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerDialogue2 : Interactable
{
    [SerializeField] private DialogueData _data;
    [SerializeField] QuestSO questSO;
    
    private Animator _animator;
    private bool _isTalk;
    private string OriginalMassage;

    CapsuleCollider _capsuleCollider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        OriginalMassage = promptMessage;
        gameObject.layer = LayerMask.NameToLayer("Interact");
    }

    private void Start()
    {
        SettingManager(false);
    }

    private void Update()
    {
        if(questSO.QuestAchievementConditions == questSO.CurrentProgress)
        {
            SettingManager(true);
        }
    }

    public override void Interact()
    {
        if (questSO.QuestAchievementConditions > questSO.CurrentProgress) return; 

        if (_isTalk == false)
        {
            _isTalk = true;
            DialogueManager.StartDialogue(_data, _animator,
                () => promptMessage = string.Empty,
                ()=>SceneControlManager.FadeOut(()=> SceneManager.LoadScene("Karaoke_2")));
        }
    }

    private void SettingManager(bool _isActive)
    {
        _capsuleCollider.enabled = _isActive;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(_isActive);
        }
    }
}
