using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Phone : Interactable
{
    public DialogueData DialogueData;
    private string defaultMessage;
    private AudioSource _audio;
    private bool qwer;
    [SerializeField] private NPCSpawn _npcSpawn;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        defaultMessage = promptMessage;
    }
    public override void Interact()
    {
        if (qwer == false)
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
        qwer = true;
    }

    private IEnumerator ItemNone()
    {
        promptMessage = "[전화가 오지 않았다.]";
        yield return new WaitForSeconds(1.5f);
        promptMessage = defaultMessage;
    }

}
