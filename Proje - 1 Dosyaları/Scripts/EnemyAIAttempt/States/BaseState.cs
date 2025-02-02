

public abstract class BaseState
{
    public Enemy enemy;
    public StateMachine stateMachine;
    //instance of enemy class
    //instance of statemachine class

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}