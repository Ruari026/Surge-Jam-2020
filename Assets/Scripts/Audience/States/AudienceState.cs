using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceState
{
    public virtual void StartState(AudienceMemberController theAudienceMember)
    {

    }

    public virtual void UpdateState(AudienceMemberController theAudienceMember)
    {

    }
}

public enum AudienceStates
{
    AUDIENCE_IDLE,
    AUDIENCE_FOCUSED,
    AUDIENCE_EXIT
}