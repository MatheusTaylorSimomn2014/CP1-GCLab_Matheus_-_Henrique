namespace GCLab;

class Publisher
{
    public event Action? OnSomething;

    public void Raise() => OnSomething?.Invoke();

    public void ClearSubscribers()
    {
        OnSomething = null;
    }
}