using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceExitState : AudienceState
{
    public override void StartState(AudienceMemberController theAudienceMember)
    {
        theAudienceMember.StartCoroutine(DelayStart(theAudienceMember));
    }

    private IEnumerator DelayStart(AudienceMemberController theAudienceMember)
    {
        foreach (GameObject g in theAudienceMember.possibleIdleSprites)
        {
            g.SetActive(false);
        }
        theAudienceMember.focusedSprite.SetActive(false);

        if (theAudienceMember.success)
        {
            theAudienceMember.successSprite.SetActive(true);
        }
        else
        {
            theAudienceMember.failSprite.SetActive(true);
        }
        theAudienceMember.questionLeft.SetActive(false);
        theAudienceMember.questionRight.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        theAudienceMember.animController.SetTrigger("Hide");

        yield return new WaitForSeconds(0.5f);

        GameObject.Destroy(theAudienceMember.gameObject);
    }

    public override void UpdateState(AudienceMemberController theAudienceMember)
    {

    }
}
