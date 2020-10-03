using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMemberController : MonoBehaviour
{
    [SerializeField]
    private AudienceStates startState;
    private AudienceState currentState;

    private AudienceIdleState idleState;
    private AudienceFocusedState focusedState;
    private AudienceExitState exitState;

    // Start is called before the first frame update
    void Start()
    {
        idleState = new AudienceIdleState();
        focusedState = new AudienceFocusedState();
        exitState = new AudienceExitState();

        ChangeState(startState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }


    /*
    ====================================================================================================
    State Handling
    ====================================================================================================
    */
    public void ChangeState(AudienceStates newState)
    {
        switch(newState)
        {
            case AudienceStates.AUDIENCE_IDLE:
                currentState = idleState;
                break;

            case AudienceStates.AUDIENCE_FOCUSED:
                currentState = focusedState;
                break;

            case AudienceStates.AUDIENCE_EXIT:
                currentState = exitState;
                break;
        }

        currentState.StartState(this);
    }
}
