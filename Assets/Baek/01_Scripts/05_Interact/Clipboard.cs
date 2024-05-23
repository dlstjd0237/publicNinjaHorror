using UnityEngine;



[RequireComponent(typeof(Outline))]
public class Clipboard : Interactable
{
    [SerializeField] private QuestSO[] _questSO;
    [SerializeField] private bool NpcSpawn = false;
    public override void Interact()
    {
        Debug.Log(QuestManager.Instance);
        QuestManager.Instance.GetClipboard = true;
        for (int i = 0; i < _questSO.Length; i++)
        {
            QuestManager.Instance.TakeQuest(_questSO[i]);
        }
        if (NpcSpawn == false)
        {
            GameObject.Find("NPCSpawn").GetComponent<NPCSpawn>().SpawnNPC();
        }
        gameObject.SetActive(false);
    }
}
