using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    [SerializeField] private float _textTypingSpeed = 0.15f;
    private InputReader _inputReader;

    private UIDocument _doc;
    private VisualElement _contain;
    private Label _currentSpeakingText;
    private Label _infoText;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _doc = GetComponent<UIDocument>();
        var _root = _doc.rootVisualElement;
        _contain = _root.Q<VisualElement>("contain");
        _currentSpeakingText = _root.Q<Label>("name-label");
        _infoText = _root.Q<Label>("content-label");

        if (_inputReader == null)
            _inputReader = Resources.Load<InputReader>("Baek/PlayerInputReader");
    }




    public static void StartDialogue(DialogueData dialogueData) =>
        Instance._StartDialogue(dialogueData, null, null, null);
    public static void StartDialogue(DialogueData dialogueData, Animator animator) =>
    Instance._StartDialogue(dialogueData, animator, null, null);
    public static void StartDialogue(DialogueData dialogueData,Action startAction, Action endAction) =>
       Instance._StartDialogue(dialogueData, null, startAction, endAction);
    public static void StartDialogue(DialogueData dialogueData, Animator animator, Action startAction, Action endAction) =>
    Instance._StartDialogue(dialogueData, animator, startAction, endAction);


    private void _StartDialogue(DialogueData dialogueData, Animator animator, Action startAction, Action endAction)
    {
        _contain.RemoveFromClassList("on");
        StartCoroutine(SetText(dialogueData.InfoList, startAction, endAction, animator));
    }

    private IEnumerator SetText(List<DialogueInfo> textList, Action startAction, Action endAction, Animator animator)
    {
        _inputReader.OffFloor();
        startAction?.Invoke();
        var Wait = new WaitForSeconds(_textTypingSpeed); //Å¸ÀÔÇÎ ¼Óµµ
        for (int i = 0; i < textList.Count; i++)
        {
            _currentSpeakingText.text = "";
            string name = textList[i].Talker;
            _currentSpeakingText.text = name;
            _infoText.text = "";

            if (animator != null && textList[i].DialogueAnimator)
            {
                animator.runtimeAnimatorController = textList[i].DialogueAnimator;
            }

            string info = textList[i].Content;
            int count = 0;
            while (count != info.Length)
            {
                if (count < info.Length)
                {
                    _infoText.text += info[count].ToString();
                    count++;
                    yield return Wait;
                }
            }
            yield return new WaitUntil(() => Keyboard.current.fKey.wasPressedThisFrame);

        }
        _contain.AddToClassList("on");
        endAction?.Invoke();
        _inputReader.OnFloor();
    }
}