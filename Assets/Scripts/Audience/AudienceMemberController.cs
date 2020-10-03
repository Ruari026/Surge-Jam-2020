using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceMemberController : MonoBehaviour
{
    // State Handling
    [SerializeField]
    private AudienceStates startState;
    private AudienceState currentState;

    private AudienceIdleState idleState;
    private AudienceFocusedState focusedState;
    private AudienceExitState exitState;

    // Timer
    public float currentTime = 0.0f;
    [SerializeField]
    public float maxTime = 1.0f;

    // Interaction-
    public GameObject questionUI;
    public QuestionAnswersScriptableObject theQuestion;

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

    public void ChangeState(string newState)
    {
        Debug.Log("Changing States: AudienceState-" + newState);

        switch(newState)
        {
            case "Idle":
                currentState = idleState;
                break;

            case "Focused":
                currentState = focusedState;
                break;


            case "Exit":
                currentState = exitState;
                break;
        }

        currentState.StartState(this);
    }
}
