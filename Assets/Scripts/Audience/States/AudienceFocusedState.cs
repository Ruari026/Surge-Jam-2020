using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceFocusedState : AudienceState
{
    public override void StartState(AudienceMemberController theAudienceMember)
    {
        Vector3 targetPos = GameObject.FindGameObjectWithTag("FocusedPoint").transform.position;
        theAudienceMember.transform.position = targetPos;

        QuestionUIManager.instance.OpenQuestionUI(theAudienceMember);


        theAudienceMember.questionLeft.SetActive(false);
        theAudienceMember.questionRight.SetActive(false);
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
