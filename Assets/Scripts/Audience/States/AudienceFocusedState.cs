using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceFocusedState : AudienceState
{
    public override void StartState(AudienceMemberController theAudienceMember)
    {
        // Getting the question to show
        if (theAudienceMember.useGlobalQuestionSet)
        {
            theAudienceMember.theQuestion = QuestionsManager.instance.GetQuestion();
        }
        else
        {
            int pickedQuestion = Random.Range(0, theAudienceMember.possibleStartQuestions.Length);
            theAudienceMember.theQuestion = theAudienceMember.possibleStartQuestions[pickedQuestion];
        }

        // Showing Question To Player
        if (QuestionUIManager.instance.OpenQuestionUI(theAudienceMember))
        {
            theAudienceMember.StartCoroutine(this.StartAnim(theAudienceMember));
        }
        else
        {
            theAudienceMember.ChangeState(AudienceStates.AUDIENCE_IDLE, false);
        }
    }

    private IEnumerator StartAnim(AudienceMemberController theAudienceMember)
    {
        theAudienceMember.spriteAnimController.SetTrigger("Hide");
        theAudienceMember.bubbleAnimController.SetTrigger("Interact");

        if (theAudienceMember.hasTimer)
        {
            theAudienceMember.currentTime /= 2;
        }

        yield return new WaitForSeconds(0.5f);
        
        theAudienceMember.questionLeft.SetActive(false);
        theAudienceMember.questionRight.SetActive(false);

        Vector3 targetPos = GameObject.FindGameObjectWithTag("FocusedPoint").transform.position;
        theAudienceMember.transform.position = targetPos;
        
        foreach(GameObject g in theAudienceMember.possibleIdleSprites)
        {
            g.SetActive(false);
        }
        theAudienceMember.focusedSprite.SetActive(true);

        theAudienceMember.spriteAnimController.SetTrigger("Show");
    }

    public override void UpdateState(AudienceMemberController theAudienceMember)
    {
        if (theAudienceMember.hasTimer)
        {
            theAudienceMember.currentTime += Time.deltaTime;

            // Updating UI
            float pos = theAudienceMember.currentTime / theAudienceMember.maxTime;
            Vector3 newScale = Vector3.one;
            newScale.x = pos;
            Color newColor = Color.Lerp(Color.green, Color.red, pos);

            theAudienceMember.leftFill.transform.localScale = newScale;
            theAudienceMember.leftFill.color = newColor;

            theAudienceMember.rightFill.transform.localScale = newScale;
            theAudienceMember.rightFill.color = newColor;


            // Checking Exit Condition
            if (theAudienceMember.currentTime > theAudienceMember.maxTime)
            {
                LevelManager.instance.IncreaseInstability();

                theAudienceMember.ChangeState(AudienceStates.AUDIENCE_EXIT);

                QuestionUIManager.instance.CloseQuestionUI(false);
            }
        }
    }
}
