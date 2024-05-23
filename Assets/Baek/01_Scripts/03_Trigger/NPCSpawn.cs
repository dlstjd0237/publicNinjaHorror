using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    [SerializeField] private Transform SpawnTrm;
    [SerializeField] private List<GameObject> _npcList;
    [SerializeField] private GameObject _mainChr;
    public void SpawnNPC()
    {

        int count = Random.Range(0, _npcList.Count);
        GameObject qwer = Instantiate(_npcList[count], SpawnTrm.position, Quaternion.identity);

    }

    public void SpawnMainChrNPC()
    {
        Instantiate(_mainChr, SpawnTrm.position, Quaternion.identity);
    }

}
