public interface IClock
{
    public void Init(ClockManager manager);
    public void UpdateTime(TimeData timeData);
}