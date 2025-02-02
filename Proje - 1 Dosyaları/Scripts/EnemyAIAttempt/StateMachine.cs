using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    //property for the patrol state
    public PatrolState patrolState;
    public void Initialize()
    {
        //setup default state

        patrolState = new PatrolState();
        ChangeState(patrolState);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (activeState != null) 
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check activeState if != null
        if (newState != activeState) {
            //run cleanup on activeState.
            activeState.Exit();
        }
        //change to a new state
        activeState = newState;

        if (activeState != null)
        {
            //Setup new state.
            activeState.stateMachine = this;

            activeState.enemy = GetComponent<Enemy>();
            //assign state enemy class.
            activeState.Enter();
        }
    }
}
