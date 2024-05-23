using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Chapter/ChapterSO")]
public class ChapterSO : ScriptableObject
{
    [SerializeField] private string _chapterName; public string ChapterName { get { return _chapterName; } }
    [SerializeField] private string _chapterInfo; public string ChapterInfo { get { return _chapterInfo; } }
    [SerializeField] private string _nextSceneName; public string NextSceneName { get { return _nextSceneName; } }
}
