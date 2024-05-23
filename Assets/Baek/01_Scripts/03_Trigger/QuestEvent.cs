using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _oneDayEvent;
    [SerializeField] private UnityEvent _twoDayEvent;
    [SerializeField] private UnityEvent _threeDayEvent;
    [SerializeField] private UnityEvent _fourDayEvent;
    [SerializeField] private UnityEvent _fiveDayEvent;
    public enum ToDay
    {
        one,
        two,
        three,
        four,
        five
    }
    [SerializeField] private ToDay _toDay;

    public void CompleteQuest()
    {
        switch (_toDay)
        {
            case ToDay.one:
                _oneDayEvent?.Invoke();
                break;
            case ToDay.two:
                _twoDayEvent?.Invoke();
                break;
            case ToDay.three:
                _threeDayEvent?.Invoke();
                break;
            case ToDay.four:
                _fourDayEvent?.Invoke();
                break;
            case ToDay.five:
                _fiveDayEvent?.Invoke();
                break;
        }
    }
}
