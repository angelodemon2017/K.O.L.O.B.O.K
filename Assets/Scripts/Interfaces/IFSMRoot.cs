public interface IFSMRoot
{
    IState CurrentState { get; }

    void ChangeState(IState state);
}