using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Phone : Interactable
{
    public DialogueData DialogueData;
    private string defaultMessage;
    private AudioSource _audio;
    private bool _isInteracted;
    [SerializeField] private NPCSpawn _npcSpawn;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        defaultMessage = promptMessage;
    }
    public override void Interact()
    {
        if (_isInteracted == false)
        {
            StopCoroutine("ItemNone");
            StartCoroutine("ItemNone");
            return;
        }
        _audio.Stop();
        DialogueManager.StartDialogue(DialogueData, null, null, () => _npcSpawn.SpawnMainChrNPC());
    }

    public void StartEvent()
    {
        _audio.Play();
        _audio.loop = true;
        _isInteracted = true;
    }

    private IEnumerator ItemNone()
    {
        promptMessage = "[ÀüÈ­°¡ ¿ÀÁö ¾Ê¾Ò´Ù.]";
        yield return new WaitForSeconds(1.5f);
        promptMessage = defaultMessage;
    }

}
