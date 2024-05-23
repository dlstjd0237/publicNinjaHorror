using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangingCCTV : Interactable
{
    [SerializeField] GameObject CCTVPanel;
    [SerializeField] private GameObject noise;
    private GameObject playercam;
    private CinemachineVirtualCamera[] virtualcam;
    private InputReader _inputReader;
    private AudioSource _audio;
    private int cctvCount;
    private bool watching = false;
    private bool interact = false;
    private bool interacting = false;
    private string savePrompet;

    [Header("CCTV할 카메라 = virtaul cam에 CCTV Tag 달아주기!")]
    [SerializeField] private List<CinemachineVirtualCamera> cinemachine = new List<CinemachineVirtualCamera>();


    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        if (_inputReader == null)
            _inputReader = Resources.Load<InputReader>("Baek/PlayerInputReader");
    }

    void Start()
    {
        for (int i = 0; i < cinemachine.Count; ++i)
        {
            if (cinemachine[i].gameObject.tag != "CCTV")
            {
                cinemachine.Remove(cinemachine[i]);
            }
        }
        savePrompet = promptMessage;
        cinemachine.Clear();
        virtualcam = FindObjectsOfType<CinemachineVirtualCamera>();
        for (int i = 0; i < virtualcam.Length; ++i)
        {
            if (virtualcam[i].gameObject.name == "PlayerCam")
            {
                playercam = virtualcam[i].gameObject;
            }
            else
            {
                cinemachine.Add(virtualcam[i]);
                virtualcam[i].gameObject.SetActive(false);
            }
        }

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && watching)
        {
            _inputReader.OnFloor();
            interact = true;
            watching = false;
            CCTVPanel.SetActive(false);
            playercam.SetActive(true);
            noise.SetActive(true);
            StartCoroutine(Delay());

        }

        if (watching && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Left
            ChangeLeft();
        }
        else if (watching && Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Left
            ChangeRight();
        }


    }

    public void ChangeLeft()
    {
        _audio.Play();
        StopAllCoroutines();
        noise.SetActive(true);
        StartCoroutine(Delay());
        --cctvCount;

        if (cctvCount < 0)
        {
            cctvCount = 3;
        }

        for (int i = 0; i < virtualcam.Length - 1; ++i)
        {
            if (i == cctvCount)
            {
                cinemachine[i].gameObject.SetActive(true);
            }
            else
            {
                cinemachine[i].gameObject.SetActive(false);
            }
        }
    }
    public void ChangeRight()
    {
        _audio.Play();
        StopAllCoroutines();
        noise.SetActive(true);
        StartCoroutine(Delay());
        ++cctvCount;

        if (cctvCount > 3)
        {
            cctvCount = 0;
        }

        for (int i = 0; i < virtualcam.Length - 1; ++i)
        {
            if (i == cctvCount)
            {
                cinemachine[i].gameObject.SetActive(true);
            }
            else
            {
                cinemachine[i].gameObject.SetActive(false);
            }
        }
    }

    public override void Interact()
    {

        if (!interacting)
        {
            _inputReader.OffFloor();
            interacting = true;
            StopAllCoroutines();
            noise.SetActive(true);
            StartCoroutine(Delay());
            //if () 
            promptMessage = string.Empty;
            //파넬
            CCTVPanel.SetActive(true);
            //플레이어캠
            playercam.SetActive(false);
            //CCTV_index[0] turn on 
            cinemachine[0].gameObject.SetActive(true);
            watching = true;
        }
    }

    private IEnumerator Delay()
    {

        if (interact)
        {
            yield return new WaitForSeconds(1f);
            noise.SetActive(false);
            promptMessage = savePrompet;
            interacting = false;
            interact = false;
        }
        else
        {
            yield return new WaitForSeconds(1f);
            noise.SetActive(false);
        }

    }
}
