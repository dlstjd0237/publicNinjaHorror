using DG.Tweening;
using UnityEngine;

public class Door : Interactable
{
    [HideInInspector] public bool isOpen;
    [HideInInspector] public bool isOpening;
    [SerializeField] private AudioClip[] _audioClip;
    private AudioSource _audioSource = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public override void Interact()
    {
        if (!isOpen)
        {
            AudioChange(_audioClip[0]);
            isOpening = true;
            transform.DOLocalRotate(new Vector3(0, 120, 0), 1).OnComplete(() => isOpening = false);
            promptMessage = "문 닫기";
            isOpen = true;
        }
        else if (isOpen)
        {
            AudioChange(_audioClip[1]);
            isOpening = true;
            if (gameObject.name.Equals("BigRoomDoor"))
                transform.DOLocalRotate(new Vector3(0, -45, 0), 1).OnComplete(() => isOpening = false);
            else
                transform.DOLocalRotate(new Vector3(0, 0, 0), 1).OnComplete(() => isOpening = false);
            promptMessage = "문 열기";
            isOpen = false;
        }
    }
    private void AudioChange(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
