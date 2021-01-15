using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;

public class Test : MonoBehaviour
{
    enum State
    {
        Init,
        Begin,
        Middle,
        End
    }

    enum Trigger
    {
        NEXT,
        RESET
    }

    StateMachine<State, Trigger> machine;

    // Start is called before the first frame update
    void Start()
    {
        machine = new StateMachine<State, Trigger>(State.Init);

        machine.Configure(State.Init)
            
            .Permit(Trigger.NEXT, State.Begin);

        machine.Configure(State.Begin)
            .OnEntry(BeginState)
            .Permit(Trigger.NEXT, State.Middle);

        machine.Configure(State.Middle)
            .OnEntry(MiddleState)
            .Permit(Trigger.NEXT, State.End);

        machine.Configure(State.End)
            .OnEntry(EndState)
            .Permit(Trigger.RESET, State.Begin);

        machine.Fire(Trigger.NEXT);
    }

    void BeginState()
    {
        Debug.Log("Begin");
    }

    void MiddleState()
    {
        Debug.Log("Middle");
    }

    void EndState()
    {
        Debug.Log("End");
    }

    float stateDelay = 5f;
    float currentDelay = 0f;
    // Update is called once per frame
    void Update()
    {
        currentDelay -= Time.deltaTime;
        if(currentDelay <= 0)
        {
            currentDelay = stateDelay;
            if (machine.CanFire(Trigger.NEXT))
            {
                machine.Fire(Trigger.NEXT);
            }
            
        }
    }
}
