using UnityEngine;
using TMPro;

public class DigitalClock : Clock
{
    [SerializeField] private TMP_Text _hours;
    [SerializeField] private TMP_Text _minutes;
    [SerializeField] private TMP_Text _seconds;

    [SerializeField] private GameObject _editorPanel;
    [SerializeField] private TMP_InputField _hoursInput;
    [SerializeField] private TMP_InputField _minutesInput;
    [SerializeField] private TMP_InputField _secondsInput;

    public override void UpdateVisual(TimeData timeData)
    {
        if (!_canVisualize) return;

        _hours.text = timeData.hours.ToString(@"00");
        _minutes.text = timeData.minutes.ToString(@"00");
        _seconds.text = timeData.seconds.ToString(@"00");
    }

    public override void OnEditClick() 
    { 
        base.OnEditClick();

        _hoursInput.text = _hours.text;
        _minutesInput.text = _minutes.text;
        _secondsInput.text = _seconds.text;

        _editorPanel.SetActive(true);
    }

    public override void OnOkClick()
    {
        base.OnOkClick();

        TimeData timeData = new()
        {
            hours = int.Parse(_hoursInput.text),
            minutes = int.Parse(_minutesInput.text),
            seconds = int.Parse(_secondsInput.text)
        };

        EditTime(timeData);

        _editorPanel.SetActive(false);
    }

    public override void OnCancelClick()
    {
        base.OnCancelClick();
        _editorPanel.SetActive(false);
    }
}
