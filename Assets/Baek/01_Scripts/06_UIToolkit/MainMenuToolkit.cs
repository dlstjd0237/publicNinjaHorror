using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
public class MainMenuToolkit : MonoBehaviour
{
    private UIDocument _doc;
    [Header("로고 스프라이트")] [SerializeField] private Sprite _logo;
    private List<Button> _btns = new();
    private AudioSource _audio;
    public float ScrollFactor { get; set; } = 500;
    [Header("챕터 버튼")] [SerializeField] private VisualTreeAsset _chapter;
    [Header("멀티 룸")] [SerializeField] private VisualTreeAsset _room;
    [Header("챕터 리스트")] [SerializeField] private ChapterSOList _chapterSOList;
    [Header("클릭 사운드")] [SerializeField] private AudioSource _clickAudio;
    [Header("배경")] [SerializeField] private Sprite _backGround;
    [Header("인트로 건너뛰기")] [SerializeField] private bool _titleStartReturn;
    private ScrollView _chapterScroll, _multiRoomScroll;
    private VisualElement _chapterBox, _mainmenuBox, _menubarcontainBox, _settingcontainBox, _multiPlayerBox;
    private Slider _masterVolumeSlider, _musicVolumeSlider, _sfxVolumeSlider;

    private void Start()
    {
        Init();
        for (int i = 0; i < _chapterSOList.ChapterList.Count; i++)
        {
            AddChapter(_chapterSOList.ChapterList[i]);
        }
        StartCoroutine(nameof(TitleStart));
        BtnSoundAdd();
    }



    private void AddChapter(ChapterSO chapterSO)
    {


        var template = _chapter.Instantiate().Q<Button>();
        var chapterinfobox = template.Q<VisualElement>("chapterinfo-box");

        template.text = chapterSO.ChapterName;
        template.Q<Label>("chaptername-label").text = chapterSO.ChapterName;
        template.Q<Label>("chapterinfo-label").text = chapterSO.ChapterInfo;
        template.RegisterCallback<ClickEvent>((ClickEvent evt) => SceneControlManager.FadeOut(() => SceneManager.LoadScene(chapterSO.NextSceneName)));
        chapterinfobox.RegisterCallback<MouseEnterEvent>((MouseEnterEvent evt) => chapterinfobox.RemoveFromClassList("on"));
        chapterinfobox.RegisterCallback<MouseOutEvent>((MouseOutEvent evt) => chapterinfobox.AddToClassList("on"));
        _btns.Add(template);
        _chapterScroll.Add(template);


    }

    private void Init()
    {
        _doc = GetComponent<UIDocument>();
        _audio = GetComponent<AudioSource>();
        var _root = _doc.rootVisualElement;

        _settingcontainBox = _root.Q<VisualElement>("settingcontain-box"); //설정 창 
        _multiPlayerBox = _root.Q<VisualElement>("multiplay-box"); // 멀티플레이 창
        MultiPlayerInit();
        _chapterScroll = _root.Q<ScrollView>("chapter-scroll"); //챕터 스크롤
        _chapterBox = _root.Q<VisualElement>("chapter-box"); // 챕터 박스
        _mainmenuBox = _root.Q<VisualElement>("mainmenucontain-box");// 메인 매뉴 박스
        _mainmenuBox.Q<Button>("setting-btn").RegisterCallback<ClickEvent>((evt) => { _settingcontainBox.RemoveFromClassList("on"); _menubarcontainBox.AddToClassList("on"); });
        _settingcontainBox.Q<Button>("exit-btn").RegisterCallback<ClickEvent>((evt) => { _settingcontainBox.AddToClassList("on"); _menubarcontainBox.RemoveFromClassList("on"); });
        _btns.Add(_settingcontainBox.Q<Button>("exit-btn"));
        _menubarcontainBox = _mainmenuBox.Q<VisualElement>("menubarcontain-box"); // 메인베뉴 사이드 바 박스
        if (_logo != null)
            _root.Q<VisualElement>("icon-box").style.backgroundImage = _logo.texture;
        if (_backGround != null)
            _mainmenuBox.style.backgroundImage = _backGround.texture;

        _btns.Add(_mainmenuBox.Q<Button>("play-btn"));
        _btns.Add(_mainmenuBox.Q<Button>("setting-btn"));
        _btns.Add(_mainmenuBox.Q<Button>("exit-btn"));
        _btns.Add(_mainmenuBox.Q<Button>("multiplay-btn"));
        _btns.Add(_chapterBox.Q<Button>("exit-btn"));
        _mainmenuBox.Q<Button>("exit-btn").RegisterCallback<ClickEvent>((evt) => { SceneControlManager.FadeOut(() => Application.Quit()); _menubarcontainBox.AddToClassList("on"); });

        _chapterBox.Q<Button>("exit-btn").RegisterCallback<ClickEvent>((evt) => { _chapterBox.AddToClassList("on"); _menubarcontainBox.RemoveFromClassList("on"); });
        _btns.Add(_chapterBox.Q<Button>("exit-btn"));
        _root.Q<Button>("play-btn").RegisterCallback<ClickEvent>((evt) => { _chapterBox.RemoveFromClassList("on"); _menubarcontainBox.AddToClassList("on"); });
        _root.Q<Button>("multiplay-btn").RegisterCallback<ClickEvent>(e => { _multiPlayerBox.RemoveFromClassList("on"); _menubarcontainBox.AddToClassList("on"); });
        //Invoke(nameof(Qwer), 0.5f);
        ScrollSet(_chapterScroll);

        _masterVolumeSlider = _settingcontainBox.Q<Slider>("mastervolume-slider");
        _musicVolumeSlider = _settingcontainBox.Q<Slider>("musicvolume-slider");
        _sfxVolumeSlider = _settingcontainBox.Q<Slider>("sfxvolume-slider");
        if (PlayerPrefs.HasKey("MasterVolume") == false) //MasterVolume에 값이 할당되있지 않다면
        {

            PlayerPrefs.SetFloat("MasterVolume", 0.5f);
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            PlayerPrefs.SetFloat("SFXVoluem", 0.5f);

        }
        _masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVoluem");

        VolumeManager.Instance.VolumeSetMaster(_masterVolumeSlider.value);
        VolumeManager.Instance.VolumeSetMusic(_musicVolumeSlider.value);
        VolumeManager.Instance.VolumeSetSFX(_sfxVolumeSlider.value);

        _masterVolumeSlider.RegisterCallback<ChangeEvent<float>>(evt => { VolumeManager.Instance.VolumeSetMaster(_masterVolumeSlider.value); Debug.Log(_masterVolumeSlider.value); });
        _musicVolumeSlider.RegisterCallback<ChangeEvent<float>>(evt => { VolumeManager.Instance.VolumeSetMusic(_musicVolumeSlider.value); });
        _sfxVolumeSlider.RegisterCallback<ChangeEvent<float>>(evt => { VolumeManager.Instance.VolumeSetSFX(_sfxVolumeSlider.value); });
    }

    private void MultiPlayerInit()
    {
        _multiPlayerBox.Q<Button>("exit-btn").RegisterCallback<ClickEvent>(e => { _multiPlayerBox.AddToClassList("on"); _menubarcontainBox.RemoveFromClassList("on"); });
        _btns.Add(_multiPlayerBox.Q<Button>("exit-btn"));
        _multiRoomScroll = _multiPlayerBox.Q<ScrollView>("room-scroll");
        _multiPlayerBox.Q<Button>("createroom-btn").RegisterCallback<ClickEvent>(CreateRoom);
        ScrollSet(_multiRoomScroll);
    }

    private void CreateRoom(ClickEvent evt)
    {

        _multiRoomScroll.Add(_room.Instantiate().Q<Button>());
    }

    private void BtnSoundAdd()
    {
        for (int i = 0; i < _btns.Count; i++)
        {
            _btns[i].RegisterCallback<MouseEnterEvent>((evt) => _audio.Play());
            _btns[i].RegisterCallback<ClickEvent>((evt) => _clickAudio.Play());
        }
    }

    private void Skip()
    {
        var root = _doc.rootVisualElement;
        var Titlebackground = root.Q<VisualElement>("titlecontain-box").Q<VisualElement>("titlebackground-box");
        Titlebackground.AddToClassList("on");
        _titleStartReturn = true;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && _titleStartReturn == false)
        {
            Skip();
            var _root = _doc.rootVisualElement;
            var titlebackground = _root.Q<VisualElement>("titlecontain-box").Q<VisualElement>("titlebackground-box");
            titlebackground.AddToClassList("on");
        }
    }
    private void ScrollSet(ScrollView scroller)
    {
        scroller.RegisterCallback<WheelEvent>((evt) =>
        {
            scroller.scrollOffset = new Vector2(scroller.scrollOffset.x + ScrollFactor * evt.delta.y, 0);
            evt.StopPropagation();
        });
    }

    private IEnumerator TitleStart()
    {
        yield return new WaitForSeconds(2f);
        var _root = _doc.rootVisualElement;
        var titlebackground = _root.Q<VisualElement>("titlecontain-box").Q<VisualElement>("titlebackground-box");
        var icon = titlebackground.Q<VisualElement>("icon-box");
        var company = titlebackground.Q<Label>("company-label");
        var agameby = titlebackground.Q<Label>("agameby-label");
        icon.AddToClassList("on");
        yield return new WaitForSeconds(3);
        icon.AddToClassList("off");
        yield return new WaitForSeconds(2);
        company.AddToClassList("on");
        yield return new WaitForSeconds(3);
        company.AddToClassList("off");
        yield return new WaitForSeconds(2);
        agameby.AddToClassList("on");
        yield return new WaitForSeconds(3);
        agameby.AddToClassList("off");
        yield return new WaitForSeconds(2);
        titlebackground.AddToClassList("on");
    }

}
