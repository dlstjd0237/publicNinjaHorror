using Unity.Mathematics;
using UnityEngine;

public class NpcInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    [SerializeField] private Transform spawnPos;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Instantiate(npc, spawnPos.position, quaternion.identity);
    }
}
