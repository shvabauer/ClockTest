using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    [SerializeField] private Clock _analogClock;
    [SerializeField] private Clock _digitalClock;
    
    private List<IClock> _clocks;

    private void OnEnable()
    {
        WorldTime.Instance.OnTimeChanged += UpdateTime;
    }

    private void OnDisable()
    {
        WorldTime.Instance.OnTimeChanged -= UpdateTime;
    }

    private void Start()
    {
        _clocks = new List<IClock>
        {
            _digitalClock,
            _analogClock
        };

        InitClocks();

        WorldTime.Instance.GetTime();
    }

    private void InitClocks()
    {
        foreach (var clock in _clocks)
        {
            clock.Init(this);
        }
    }

    private void UpdateTime(TimeData timeData)
    {
        foreach (var clock in _clocks)
        {
            clock.UpdateTime(timeData);
        }
    }

    public void UpdateEditedTime(TimeData timeData)
    {
        UpdateTime(timeData);
    }
}
