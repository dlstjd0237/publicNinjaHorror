using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class TextSOInfo
{
    public string Info;
    public AudioClip InfoAudio;
}

[CreateAssetMenu(menuName = "ScriptableObject/TextSO")]
public class TextSO : ScriptableObject
{
    public string Day;
    public List<TextSOInfo> TextInfoList;
}
