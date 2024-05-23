using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Npc : Interactable
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private DialogueData dialogueData;
    private Transform targetTrm;
    private Transform npcSpot;
    private Transform playerTrm;
    private Transform boothTrm;
    private bool isTalking; public bool IsTalking => isTalking;


    private Transform outDoorPos;
    private Transform inDoorPos;
    private Transform roomPos;
    private Door currentDoor;

    private bool onStayDoor = true;
    private bool onInRoom; public bool OnInRoom => onInRoom;
    private bool onRest;

    private bool restReady;
    private bool onResting;
    private bool doorCheck;
    private bool firstArrivalCheck;

    private float timer;
    private float maxTimer;
    private float exitTimer;
    private float exitMaxTimer;
    private int restNum;
    private int randomNum;

    private static bool onAddList;
    private static bool someoneRest;
    private static List<int> roomNumList = new();
    private enum State
    {
        Enter,
        Stay,
        Exit
    }
    private State currentState;
    private NPCSpawn _npcSpawn = null;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        npcSpot = GameObject.FindWithTag("NpcSpot").transform;
        playerTrm = GameObject.FindWithTag("Player").transform;
        boothTrm = GameObject.FindWithTag("Map").transform.Find("Booths");
        currentState = State.Enter;
        if (!onAddList)
        {
            for (int i = 1; i <= boothTrm.childCount; i++)
            {
                roomNumList.Add(i);
            }
            onAddList = true;
        }
    }
    private void OnEnable()
    {
        if (GameObject.Find("NPCSpawn") != null)
        {
            _npcSpawn = GameObject.Find("NPCSpawn").GetComponent<NPCSpawn>();
        }
    }

    private void Start()
    {
        System.Random random = new System.Random();
        int randomIndex = random.Next(roomNumList.Count);
        randomNum = roomNumList[randomIndex];
        roomNumList.Remove(randomNum);

        int roomPosNum = Random.Range(1, 4);
        restNum = Random.Range(0, 2);
        maxTimer = Random.Range(80, 120);
        exitMaxTimer = Random.Range(180, 240);
        exitTimer = exitMaxTimer;
        timer = maxTimer;
        Transform room = boothTrm.Find("Room" + randomNum);

        outDoorPos = room.Find("Positions/OutDoorPos");
        inDoorPos = room.Find("Positions/InDoorPos");
        roomPos = room.Find($"Positions/Position{roomPosNum}");
        currentDoor = room.GetComponentInChildren<Door>();
    }

    private void Update()
    {
        StateCheck();
    }

    private void StateCheck()
    {
        switch (currentState)
        {
            case State.Enter:
                Enter();
                break;
            case State.Stay:
                Stay();
                break;
            case State.Exit:
                Exit();
                break;
        }
    }

    private void Exit()
    {
        targetTrm = npcSpot.Find("ExitPos");
        navMeshAgent.SetDestination(targetTrm.position);
        if (ArrivalCheck())
        {
            roomNumList.Add(randomNum);
            Destroy(gameObject);
        }
    }

    private void Stay()
    {
        exitTimer -= Time.deltaTime;
        if (exitTimer <= 0)
        {
            currentState = State.Exit;
            exitTimer = exitMaxTimer;
        }

        if (onStayDoor)
        {
            StartCoroutine(DoorOpen());
        }
        else if (onInRoom)
        {
            InRoom();
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                onInRoom = false;
                onRest = true;
            }
        }
        else if (onRest)
        {
            if (someoneRest && !onResting)
            {
                timer = maxTimer;
                onRest = false;
                onInRoom = true;

                return;
            }
            StartCoroutine("RestRoutine");
        }
    }

    private IEnumerator DoorOpen()
    {
        targetTrm = outDoorPos;
        navMeshAgent.SetDestination(targetTrm.position);
        if (ArrivalCheck() && !currentDoor.isOpening && !currentDoor.isOpen)
        {
            currentDoor.Interact();
            onStayDoor = false;
            yield return new WaitForSeconds(1f);
            onInRoom = true;
        }
    }

    private void InRoom()
    {
        targetTrm = roomPos;
        navMeshAgent.SetDestination(targetTrm.position);
        if (ArrivalCheck() && currentDoor.isOpen && !doorCheck)
        {
            if (!firstArrivalCheck && QuestManager.Instance.QuestDataDic.ContainsKey(1000))
            {
                GameObject.Find("NPCSpawn").GetComponent<NPCSpawn>().SpawnNPC();
                firstArrivalCheck = true;
            }
            currentDoor.Interact();
            doorCheck = true;
        }
    }

    private IEnumerator RestRoutine()
    {
        timer = maxTimer;
        if (!restReady)
        {
            someoneRest = true;
            onResting = true;
            targetTrm = inDoorPos;
            navMeshAgent.SetDestination(targetTrm.position);
            if (!currentDoor.isOpening && ArrivalCheck())
            {
                currentDoor.Interact();
                restReady = true;
            }
        }
        else if (restReady)
        {
            targetTrm = npcSpot.GetChild(restNum);
            navMeshAgent.SetDestination(targetTrm.position);
            yield return new WaitForSeconds(1f);
            if (currentDoor.isOpen && doorCheck)
            {
                currentDoor.Interact();
                doorCheck = false;
            }
            if (ArrivalCheck())
            {
                yield return new WaitForSeconds(10f);
                onRest = false;
                onStayDoor = true;
                doorCheck = false;
                someoneRest = false;
                onResting = false;
                restReady = false;
                StopCoroutine("RestRoutine");
            }
        }
    }
    private void Enter()
    {
        targetTrm = npcSpot.Find("CounterPos");
        navMeshAgent.SetDestination(targetTrm.position);

        if (ArrivalCheck())
        {
            transform.localRotation = targetTrm.localRotation;
        }
    }

    public override void Interact()
    {
        if (isTalking == false)
        {
            QuestManager.Instance.QuestProgressSet(1000);
            DialogueManager.StartDialogue(dialogueData, null, () =>
            {
                promptMessage = string.Empty;
                transform.LookAt(playerTrm.position);
            }, () =>
            {
                isTalking = true;
                currentState = State.Stay;
            });
        }

    }

    private bool ArrivalCheck()
    {
        Vector3 navMeshAgentPos = navMeshAgent.destination;
        navMeshAgentPos.y = 0;
        Vector3 trmPos = transform.position;
        trmPos.y = 0;

        return navMeshAgentPos == trmPos;
    }
}
