public struct AppStateSignal
{
    public IAppState State;

    public AppStateSignal(IAppState newState)
    {
        State = newState;
    }
}
