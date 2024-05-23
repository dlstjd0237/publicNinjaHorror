using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Chapter/ChapterSOList")]
public class ChapterSOList : ScriptableObject
{
    [SerializeField] private List<ChapterSO> _chapterList; public List<ChapterSO> ChapterList { get { return _chapterList; } }

}
