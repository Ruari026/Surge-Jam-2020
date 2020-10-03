using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceIdleState : AudienceState
{
    public override void StartState(AudienceMemberController theAudienceMember)
    {
        int pickedSprite = Random.Range(0, theAudienceMember.possibleIdleSprites.Length);
        for (int i = 0; i < theAudienceMember.possibleIdleSprites.Length; i++)
        {
            if (i == pickedSprite)
                theAudienceMember.possibleIdleSprites[i].SetActive(true);
            else
                theAudienceMember.possibleIdleSprites[i].SetActive(false);
        }
    }

    public override void UpdateState(AudienceMemberController theAudienceMember)
    {
        if (theAudienceMember.transform.position.x < 0)
        {
            theAudienceMember.questionLeft.SetActive(false);
            theAudienceMember.questionRight.SetActive(true);
        }
        else
        {
            theAudienceMember.questionLeft.SetActive(true);
            theAudienceMember.questionRight.SetActive(false);
        }

        theAudienceMember.currentTime += Time.deltaTime;
        if (theAudienceMember.currentTime > theAudienceMember.maxTime)
        {
            LevelManager.instance.IncreaseInstability();
            theAudienceMember.ChangeState(AudienceStates.AUDIENCE_EXIT);
        }
    }
}
