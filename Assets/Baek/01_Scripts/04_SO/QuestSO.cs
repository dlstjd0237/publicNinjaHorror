using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "ScriptableObject/Quest/QuestData")]
public class QuestSO : ScriptableObject
{
    public String QuestInfo;
    public int QuestID;
    public int QuestAchievementConditions;
    [HideInInspector] public int CurrentProgress;
}
