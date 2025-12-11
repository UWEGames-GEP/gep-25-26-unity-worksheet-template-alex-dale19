
using UnityEngine;

public abstract class GameState
{
    protected GameManager _manager;

    public GameState(GameManager manager)
    {
        _manager = manager;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class GameplayState : GameState
{
    public GameplayState(GameManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Entering Gameplay...");
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
           
            _manager.SwitchState(new PausedState(_manager));
        }
    }

    public override void Exit() { }
}

public class PausedState : GameState
{
    public PausedState(GameManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Game Paused.");
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            _manager.SwitchState(new GameplayState(_manager));
        }
    }

    public override void Exit() { }
}