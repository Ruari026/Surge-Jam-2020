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
        if (QuestionUIManager.instance.OpenUI(theAudienceMember))
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
        // Animation for moving away from background
        theAudienceMember.spriteAnimController.SetTrigger("Hide");
        theAudienceMember.bubbleAnimController.SetTrigger("Interact");

        if (theAudienceMember.hasTimer)
        {
            theAudienceMember.currentTime /= 2;
        }

        yield return new WaitForSeconds(0.5f);
        
        theAudienceMember.questionLeft.SetActive(false);
        theAudienceMember.questionRight.SetActive(false);

        // Moving to set position in world for asking question to the user
        Vector3 targetPos = GameObject.FindGameObjectWithTag("FocusedPoint").transform.position;
        theAudienceMember.transform.position = targetPos;
        // Changing sprite to talking sprite anim
        foreach(GameObject g in theAudienceMember.possibleIdleSprites)
        {
            g.SetActive(false);
        }
        theAudienceMember.focusedSprite.SetActive(true);
        
        // Anim for showing up to new focused position
        theAudienceMember.spriteAnimController.SetTrigger("Show");
        // Changing layer so that the audience member appears in front of the background fade (foreground layer == 10)
        foreach (GameObject g in theAudienceMember.possibleIdleSprites)
        {
            g.layer = 10;
        }
        theAudienceMember.focusedSprite.layer = 10;
        theAudienceMember.successSprite.layer = 10;
        theAudienceMember.failSprite.layer = 10;
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

                QuestionUIManager.instance.CloseUI(false);
            }
        }
    }
}
