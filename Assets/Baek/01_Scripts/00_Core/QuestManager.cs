using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestManager : MonoSingleton<QuestManager>
{
    public Dictionary<int, QuestSO> QuestDataDic = new();
    public Dictionary<int, Label> QuestLabelDic = new();
    #region ����Ʈ ��Ŷ
    private UIDocument _doc;
    private VisualElement _questBox, _questInfoBox;
    private Label _headLabel;
    [SerializeField] private VisualTreeAsset _questAsset;
    private AudioSource _audio;
    public bool GetClipboard { get; set; }
    #endregion

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        ToolkitInit();
    }


    private void ToolkitInit() // ����Ʈ ��Ŷ �ʱ�ȭ
    {
        _doc = GetComponent<UIDocument>();
        var _root = _doc.rootVisualElement;
        _questBox = _root.Q<VisualElement>("clipboard-box");
        _headLabel = _questBox.Q<Label>("head-label");
        _questInfoBox = _questBox.Q<VisualElement>("labelinfo-box");
    }

    public void QuestProgressSet(int QuestID) // ����Ʈ�� �ش�Ǵ� �ൿ�� ������ ����Ʈ ���൵ �ö�
    {
        if (QuestClearChake(QuestID))
            return;
        QuestDataDic[QuestID].CurrentProgress++;
        QuestLabelDic[QuestID].text = $"{QuestDataDic[QuestID].QuestInfo} {QuestDataDic[QuestID].CurrentProgress}/{QuestDataDic[QuestID].QuestAchievementConditions}";
        if (QuestDataDic[QuestID].CurrentProgress >= QuestDataDic[QuestID].QuestAchievementConditions)
        {
            _audio.Play();
            _questInfoBox.Remove(QuestLabelDic[QuestID]);
            QuestLabelDic.Remove(QuestID);
            QuestDataDic.Remove(QuestID);
            if (QuestDataDic.Count <= 0)
            {
                AllQuestComplete();
            }
        }

    }

    private void AllQuestComplete()
    {
        if (GameObject.Find("QuestEvent") == null)
            return;
        GameObject.Find("QuestEvent").GetComponent<QuestEvent>().CompleteQuest();
    }
#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            AllQuestComplete();
    }
#endif
    public void Questing() // TabŰ�� �������� ����Ʈ â �߰�
    {
        if (GetClipboard)
            _questBox.ToggleInClassList("on");
    }


    public void TakeQuest(QuestSO questSO)
    {
        var quest = _questAsset.Instantiate().Q<Label>();

        questSO.CurrentProgress = 0;
        QuestDataDic.Add(questSO.QuestID, questSO);
        QuestLabelDic.Add(questSO.QuestID, quest.Q<Label>("questlabel-label"));
        QuestLabelDic[questSO.QuestID].text = $"{QuestDataDic[questSO.QuestID].QuestInfo} {QuestDataDic[questSO.QuestID].CurrentProgress}/{QuestDataDic[questSO.QuestID].QuestAchievementConditions}";
        _questInfoBox.Add(QuestLabelDic[questSO.QuestID]);

    }
    public bool QuestClearChake(int Key)
    {
        if (!QuestDataDic.ContainsKey(Key) || !QuestLabelDic.ContainsKey(Key)) //��ųʸ��� �������� ���� �׶��� ����Ʈ�� Ŭ���� �ߴٴ°�
            return true;
        else
            return false;
    }

}
