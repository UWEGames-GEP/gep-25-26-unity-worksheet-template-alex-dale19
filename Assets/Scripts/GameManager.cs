using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private GameState _currentState;

    void Start()
    {
        
        SwitchState(new GameplayState(this));
    }

    void Update()
    {
        
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }

    public void SwitchState(GameState newState)
    {
        
        if (_currentState != null)
        {
            _currentState.Exit();
        }


        _currentState = newState;

        
        if (_currentState != null)
        {
            _currentState.Enter();
        }
    }
}