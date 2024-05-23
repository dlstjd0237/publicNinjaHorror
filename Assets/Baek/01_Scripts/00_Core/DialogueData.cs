using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[Serializable]
public class DialogueInfo
{
    public string Talker;
    public string Content;
    public RuntimeAnimatorController DialogueAnimator;
}

[CreateAssetMenu(menuName = "ScriptableObject/DialogueDataSO")]

public class DialogueData : ScriptableObject
{
    [SerializeField] private List<DialogueInfo> infoList; public List<DialogueInfo> InfoList { get { return infoList; } }


}