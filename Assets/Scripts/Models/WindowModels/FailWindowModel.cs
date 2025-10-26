using System;

public class FailWindowModel : WindowModelBase
{
    public Action OnRepeat;

    public void Repeat()
    {
        OnRepeat?.Invoke();
    }
}