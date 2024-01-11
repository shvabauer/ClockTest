using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class AnalogClock : Clock
{
    [SerializeField] private Image _hoursHand;
    [SerializeField] private Image _minutesHand;
    [SerializeField] private Image _secondsHand;

    [SerializeField] private ToggleGroup _toggleGroup;

    [SerializeField] private Rotatable _hoursEditor;
    [SerializeField] private Rotatable _minutesEditor;

    public override void UpdateVisual(TimeData timeData)
    {
        if (!_canVisualize) return;

        var timeInSeconds = timeData.hours * 60 * 60 +
                            timeData.minutes * 60 + 
                            timeData.seconds;

        var zRotSeconds = timeInSeconds * -360.0f / 60.0f;
        var zRotMinutes = timeInSeconds * -360.0f / (60.0f * 60.0f);
        var zRotHours = timeInSeconds * -360.0f / (60.0f * 60.0f * 12.0f);

        _secondsHand.gameObject.transform.DORotate(new Vector3(0, 0, zRotSeconds), 0.1f, RotateMode.Fast);
        _minutesHand.gameObject.transform.DORotate(new Vector3(0, 0, zRotMinutes), 0.1f, RotateMode.Fast);
        _hoursHand.gameObject.transform.DORotate(new Vector3(0, 0, zRotHours), 0.1f, RotateMode.Fast);
    }

    public override void OnEditClick() 
    {
        base.OnEditClick();

        _minutesEditor.gameObject.SetActive(true);
        _hoursEditor.gameObject.SetActive(true);
        _toggleGroup.gameObject.SetActive(true);
    }

    public override void OnOkClick()
    {
        base.OnOkClick();

        int s = 60 + (int)(_secondsHand.gameObject.GetComponent<RectTransform>().eulerAngles.z / -6);
        int m = 60 + (int)(_minutesHand.gameObject.GetComponent<RectTransform>().eulerAngles.z / -6);
        int h = (60 + (int)(_hoursHand.gameObject.GetComponent<RectTransform>().eulerAngles.z / -6)) / 5;

        var toggle = _toggleGroup.ActiveToggles().FirstOrDefault();

        switch (toggle.name)
        {
            case "Toggle AM": break;
            case "Toggle PM": h += 12; break;
        }

        if (h >= 24)
        {
            h -= 24;
        }

        TimeData timeData = new()
        {
            hours = h,
            minutes = m,
            seconds = s
        };

        EditTime(timeData);

        _minutesEditor.gameObject.SetActive(false);
        _hoursEditor.gameObject.SetActive(false);
        _toggleGroup.gameObject.SetActive(false);
    }

    public override void OnCancelClick()
    {
        base.OnCancelClick();

        _minutesEditor.gameObject.SetActive(false);
        _hoursEditor.gameObject.SetActive(false);
        _toggleGroup.gameObject.SetActive(false);
    }
}
