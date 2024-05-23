using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SettingManager : MonoSingleton<SettingManager>
{
    [SerializeField] private Image backgroundPanel; // 알파 조절
    [SerializeField] private Image conrnerPanel; // 코너
    [SerializeField] private Image settingPanel;

    [SerializeField] private AudioClip[] clips;
    private AudioSource[] audioSources;
    private bool canHoverSound; // canHoverSound가 여러번 나지 않게

    private bool isBetweenOn = false; // betweenSettingPanel
    private bool isSettionOn = false; // settingPanel

    private InputReader _input;

    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        if (_input == null)
            _input = Resources.Load<InputReader>("Baek/PlayerInputReader");
    }

    private void Start()
    {
        SettingPanelOff(0);
    }

    private void Update()
    {
        #region Setting창 띄우는 Input

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isBetweenOn && !isSettionOn)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true; ;

                _input.OffFloor();
                ConrnerPanelOn(0.5f);
                isBetweenOn = true;
            }
            else if (isBetweenOn && !isSettionOn)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                _input.OnFloor();
                ConrnerPanelOff(0.5f);
                isBetweenOn = false;
            }
        }

        #endregion

    }

    private void ConrnerPanelOn(float fadeTime)
    {
        // conrnerPanel이동 DOLocalMove 0.5f
        conrnerPanel.rectTransform.DOAnchorPosX(0, fadeTime).SetEase(Ease.InSine);
        backgroundPanel.DOFade(0.6f, fadeTime).SetEase(Ease.InSine);
    }

    private void ConrnerPanelOff(float fadeTime)
    {
        // conrnerPanel이동 DOLocalMove
        conrnerPanel.rectTransform.DOAnchorPosX(-900, fadeTime).SetEase(Ease.InSine);
        backgroundPanel.DOFade(0, fadeTime).SetEase(Ease.InSine);
    }

    public void SettingPanelOn(float dotweenTime)
    {
        settingPanel.rectTransform.DOScale(1, dotweenTime).SetEase(Ease.InSine);
    }

    public void SettingPanelOff(float dotweenTime)
    {
        settingPanel.rectTransform.DOScale(0, dotweenTime).SetEase(Ease.InSine);
    }

    public void PlayButtonClick()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _input.OnFloor();
        ConrnerPanelOff(0.5f);
        isBetweenOn = false;
    }

    public void SettingButtonClick()
    {
        isSettionOn = true;
        SettingPanelOn(0.3f);
    }

    public void CloseSettingButtonClick()
    {
        isSettionOn = false;
        SettingPanelOff(0.3f);
    }

    public void ExitButtonClick()
    {
        SceneControlManager.FadeOut(() => SceneManager.LoadScene("MainMenu"));
    }

    public void HoverSound()
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource.isPlaying) continue;

            audioSource.clip = clips[0];
            audioSource.Play();
        }
    }

    public void ClickSound()
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource.isPlaying) continue;

            audioSource.clip = clips[1];
            audioSource.Play();
        }
    }


}
