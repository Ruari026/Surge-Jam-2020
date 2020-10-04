using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceFocusedState : AudienceState
{
    public override void StartState(AudienceMemberController theAudienceMember)
    {
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
        theAudienceMember.animController.SetTrigger("Hide");
        
        theAudienceMember.questionLeft.SetActive(false);
        theAudienceMember.questionRight.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        Vector3 targetPos = GameObject.FindGameObjectWithTag("FocusedPoint").transform.position;
        theAudienceMember.transform.position = targetPos;
        
        theAudienceMember.animController.SetTrigger("Show");
    }

    public override void UpdateState(AudienceMemberController theAudienceMember)
    {
        theAudienceMember.currentTime += Time.deltaTime;
        if (theAudienceMember.currentTime > theAudienceMember.maxTime)
        {
            LevelManager.instance.IncreaseInstability();
            theAudienceMember.ChangeState(AudienceStates.AUDIENCE_EXIT);

            QuestionUIManager.instance.CloseQuestionUI(false);
        }
    }
}
