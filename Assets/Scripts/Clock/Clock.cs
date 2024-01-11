using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour, IClock
{
    protected ClockManager _manager;

    [SerializeField] private GameObject _confirmationPanel;

    [SerializeField] private Button _editBtn;
    [SerializeField] private Button _OkBtn;
    [SerializeField] private Button _cancelBtn;

    private TimeData _timeData;
    protected bool _canVisualize = true;


    public void Init(ClockManager manager)
    {
        _manager = manager;

        _editBtn.onClick.AddListener(OnEditClick);
        _cancelBtn.onClick.AddListener(OnCancelClick);
        _OkBtn.onClick.AddListener(OnOkClick);

        Timer();
    }

    public void UpdateTime(TimeData timeData)
    {
        _timeData = timeData;
    }
    
    private async void Timer()
    {
        UpdateVisual(_timeData);

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        if (_timeData.seconds == 59)
        {
            _timeData.seconds = 0;

            if (_timeData.minutes == 59)
            {
                _timeData.minutes = 0;

                if (_timeData.hours == 23)
                {
                    _timeData.hours = 0;
                }
                else
                {
                    _timeData.hours++;
                }
            }
            else
            {
                _timeData.minutes++;
            }
        }
        else
        {
            _timeData.seconds++;
        }
        Timer();
    } 
    
    protected void EditTime(TimeData timeData)
    {
        _manager.UpdateEditedTime(timeData);
    }


    public virtual void OnOkClick()
    {
        _confirmationPanel.SetActive(false);
        _canVisualize = true;
    }

    public virtual void OnCancelClick()
    {
        _confirmationPanel.SetActive(false);
        _canVisualize = true;
    }

    public virtual void OnEditClick() 
    { 
        _confirmationPanel.SetActive(true);
        _canVisualize = false;
    }

    public virtual void UpdateVisual(TimeData timeData) { }
}