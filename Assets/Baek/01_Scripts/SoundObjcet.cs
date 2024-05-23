using UnityEngine;

public class SoundObjcet : MonoBehaviour
{
    [SerializeField] private float _returnTime;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Invoke(nameof(ReturnObj), _returnTime);
        _audio.Play();
    }

    private void ReturnObj()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        PoolManager.ReturnToPool(gameObject);
    }
}
