using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class TextSceneManager : MonoBehaviour
{
    [SerializeField] private Image fadeImg;
    [SerializeField] private TextMeshProUGUI storyText;

    [Space]
    [Header("Text")]
    [SerializeField] string[] storyDialogue;

    private string[] dialogues;
    private int talknum;

    private void Awake()
    {
        fadeImg = GetComponentInChildren<Image>();
        storyText = GetComponentInChildren <TextMeshProUGUI>();
    }

    private void Start()
    {
        TextStroy();
    }

    public void TextStroy()
    {
        Debug.Log("Fade");
        fadeImg.DOFade(1, 1f).OnStart(() =>
        {
            StartDialogue(storyDialogue);
        })
        .OnComplete(() =>
        {
            fadeImg.DOFade(0, 1f);
        });
    }

    public void StartDialogue(string[] talks)
    {
        dialogues = talks;

        StartCoroutine(Typing(dialogues[talknum]));
    }

    public void NextTalk()
    {
        storyText.text = null;

        talknum++;

        if(talknum == dialogues.Length)
        {
            EndTalk();
            return;
        }

        StartCoroutine(Typing(dialogues[talknum]));
    }

    private void EndTalk()
    {
        talknum = 0;
        Debug.Log("´ë»ç ³¡");
    }

    IEnumerator Typing(string story)
    {
        storyText.text = null;

        if (story.Contains("\\n")) story = story.Replace("\\n", "\n");

        for (int i = 0; i < story.Length; i++)
        {
            storyText.text += story[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);
        NextTalk();
    }
}
