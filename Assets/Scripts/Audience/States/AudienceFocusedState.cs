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
    }

    public override void UpdateState(AudienceMemberController theAudienceMember)
    {
        theAudienceMember.currentTime += Time.deltaTime;
        if (theAudienceMember.currentTime > theAudienceMember.maxTime)
        {

        }
    }
}
