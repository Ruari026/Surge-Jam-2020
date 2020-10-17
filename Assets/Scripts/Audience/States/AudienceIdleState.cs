using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceIdleState : AudienceState
{
    private bool started = false;

    public override void StartState(AudienceMemberController theAudienceMember)
    {
        if (!started)
        {
            theAudienceMember.StartCoroutine(StartAnim(theAudienceMember));
            started = true;
        }
    }

    private IEnumerator StartAnim(AudienceMemberController theAudienceMember)
    {
        // Randomize Sprite Colors
        int pickedColor = Random.Range(0, theAudienceMember.possibleColors.Length);
        foreach (GameObject g in theAudienceMember.possibleIdleSprites)
        {
            g.GetComponent<SpriteRenderer>().color = theAudienceMember.possibleColors[pickedColor];
        }
        theAudienceMember.focusedSprite.GetComponent<SpriteRenderer>().color = theAudienceMember.possibleColors[pickedColor];
        theAudienceMember.successSprite.GetComponent<SpriteRenderer>().color = theAudienceMember.possibleColors[pickedColor];
        theAudienceMember.failSprite.GetComponent<SpriteRenderer>().color = theAudienceMember.possibleColors[pickedColor];

        // Randomize Idle Sprite
        int pickedSprite = Random.Range(0, theAudienceMember.possibleIdleSprites.Length);
        for (int i = 0; i < theAudienceMember.possibleIdleSprites.Length; i++)
        {
            if (i == pickedSprite)
                theAudienceMember.possibleIdleSprites[i].SetActive(true);
            else
                theAudienceMember.possibleIdleSprites[i].SetActive(false);
        }

        yield return new WaitForSeconds(1.0f);

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
    }

    public override void UpdateState(AudienceMemberController theAudienceMember)
    {
        if (started && theAudienceMember.hasTimer)
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
            }
        }
    }
}
