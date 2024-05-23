using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class SceneControlManager : MonoSingleton<SceneControlManager>
{
    private Image _image = null;
    private Color _cr;
    public GameObject Player => _player;
    private GameObject _player;
    private TextMeshProUGUI txtInfo;
    private InputReader _inputReader;
    private AudioSource _audio;
    private float _fadeCool = 2; public float FadeCool { get => _fadeCool; set => _fadeCool = value; }
    [SerializeField] private float _textTypingSpeed = 0.1f;
    [SerializeField] private List<TextSO> _textInfoList;

    private void Start()
    {
        _fadeIn(null);
    }

    /// <summary>
    /// 까메지는거
    /// </summary>
    /// <param name="action"></param>
    public static void FadeOut(Action action) =>
        Instance._fadeOut(action);
    /// <summary>
    /// 투명해지는거
    /// </summary>
    /// <param name="action"></param>
    public static void FadeIn(Action action) =>
     Instance._fadeIn(action);


    /// <summary>
    /// 1=>0
    /// </summary>
    /// <param name="action"></param>
    /// 

    private void _fadeIn(Action action)
    {
        _image.raycastTarget = false;
        StartCoroutine(fadeInCo(action));
    }
    private IEnumerator fadeInCo(Action action)
    {
        _cr = _image.color;
        while (_image.color.a >= 0)
        {
            _cr.a -= Time.deltaTime / _fadeCool;
            _image.color = _cr;
            yield return null;
        }
        action?.Invoke();
    }

    /// <summary>
    /// 0=>1
    /// </summary>
    /// <param name="action"></param>
    private void _fadeOut(Action action)
    {
        _image.raycastTarget = true;
        StopAllCoroutines();
        StartCoroutine(fadeOutCo(action));
    }
    private IEnumerator fadeOutCo(Action action)
    {
        _cr = _image.color;
        while (_image.color.a <= 1)
        {
            _cr.a += Time.deltaTime / _fadeCool;
            _image.color = _cr;
            yield return null;
        }
        action?.Invoke();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _image = transform.Find("Canvas/FadeImage").GetComponent<Image>();
        txtInfo = transform.Find("Canvas/InfoText").GetComponent<TextMeshProUGUI>();
        if (_inputReader == null)
            _inputReader = Resources.Load<InputReader>("Baek/PlayerInputReader");
        if (GameObject.Find("Player") is not null)
            _player = GameObject.Find("Player");
        else
            _player = null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.Find("Player") is not null)
            _player = GameObject.Find("Player");
        else
            _player = null;

        for (int i = 0; i < _textInfoList.Count; i++)
        {
            if (_textInfoList[i].Day == SceneManager.GetActiveScene().name)
            {
                SetText(_textInfoList[i].TextInfoList, null, () => _fadeIn(null));
                return;
            }
        }
        _fadeIn(() => { });

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    public static void SetText(List<TextSOInfo> textInfo) => Instance._setText(textInfo, null, null);
    public static void SetText(List<TextSOInfo> textInfo, Action startAction, Action endAction) => Instance._setText(textInfo, startAction, endAction);

    private void _setText(List<TextSOInfo> textList, Action startAction, Action endAction)
    {
        StartCoroutine(SetTextCoroutine(textList, startAction, endAction));
    }

    private IEnumerator SetTextCoroutine(List<TextSOInfo> textList, Action startAction, Action endAction)
    {
        txtInfo.text = string.Empty;
        _inputReader.OffFloor();
        startAction?.Invoke();
        var Wait = new WaitForSeconds(_textTypingSpeed);
        for (int i = 0; i < textList.Count; i++)
        {
            string info = textList[i].Info;
            int count = 0;
            if (textList[i].InfoAudio != null)
            {
                _audio.clip = textList[i].InfoAudio;
                _audio.Play();
            }


            while (count != info.Length)
            {
                if (count < info.Length)
                {
                    txtInfo.text += info[count].ToString();
                    count++;
                    yield return Wait;
                }
            }
            txtInfo.text += "\n";
            yield return new WaitUntil(() => Keyboard.current.fKey.wasPressedThisFrame);

        }
        endAction?.Invoke();
        txtInfo.text = string.Empty;
        _inputReader.OnFloor();
    }
}
