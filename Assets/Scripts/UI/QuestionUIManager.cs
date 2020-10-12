using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUIManager : MonoBehaviour
{
    private static QuestionUIManager _Instance;
    public static QuestionUIManager instance
    {
        get
        {
            if (_Instance != null)
            {
                return _Instance;
            }
            else
            {
                Debug.LogError("ERROR: No Instance Exists");
                return null;
            }
        }
    }

    private AudienceMemberController interactingAudienceMember;

    [Header("UI Parents")]
    [SerializeField]
    private GameObject theUI;
    [SerializeField]
    private GameObject twoAnswerUI;
    [SerializeField]
    private GameObject threeAnswerUI;
    [SerializeField]
    private GameObject fourAnswerUI;

    [Header("Question Timer")]
    [SerializeField]
    private bool hasTimer = false;
    [SerializeField]
    private Image timerBar;

    [Header("Individual Answers")]
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private Text[] answerOneTexts;
    [SerializeField]
    private Text[] answerTwoTexts;
    [SerializeField]
    private Text[] answerThreeTexts;
    [SerializeField]
    private Text[] answerFourTexts;

    private void OnEnable()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Debug.LogError("ERROR: Instance Already Exists");
        }
    }

    public bool OpenQuestionUI(AudienceMemberController openingAudienceMember)
    {
        if (interactingAudienceMember == null)
        {
            interactingAudienceMember = openingAudienceMember;

            SetUIFields(interactingAudienceMember.theQuestion);

            StartCoroutine(DelayUIOpen());

            return true;
        }
        else
        {
            return false;
        }
    }
    
    private IEnumerator DelayUIOpen()
    {
        yield return new WaitForSeconds(0.25f);

        theUI.SetActive(true);
    }

    private void Update()
    {
        if (interactingAudienceMember != null && hasTimer)
        {
            float pos = interactingAudienceMember.currentTime / interactingAudienceMember.maxTime;
            Vector3 newScale = Vector3.one;
            newScale.x = pos;
            Color newColor = Color.Lerp(Color.green, Color.red, pos);

            timerBar.transform.localScale = newScale;
            timerBar.color = newColor;
        }
    }

    public void CloseQuestionUI(bool questionAnswered)
    {
        // Audience Member Handling
        if (interactingAudienceMember != null)
        {
            interactingAudienceMember.success = true;
            interactingAudienceMember.ChangeState(AudienceStates.AUDIENCE_EXIT);
            interactingAudienceMember = null;
        }
        
        if (questionAnswered)
        {
            // Add a marble
            LevelManager.instance.SpawnMarble();
        }

        // Close UI
        theUI.SetActive(false);
    }

    public void CloseQuestionUI(int answerNumber)
    {
        // Audience Member Handling
        if (interactingAudienceMember != null)
        {
            // Add a marble
            AnswerTypes answer = interactingAudienceMember.theQuestion.answers[answerNumber].type;
            LevelManager.instance.SpawnMarble(answer);

            interactingAudienceMember.success = true;
            interactingAudienceMember.ChangeState(AudienceStates.AUDIENCE_EXIT);
            
            interactingAudienceMember = null;
        }

        // Close UI
        theUI.SetActive(false);
    }

    public void SetUIFields(QuestionAnswersScriptableObject set)
    {
        // Updating UI For Question
        questionText.text = set.question;

        for (int i = 0; i < set.answers.Length; i++)
        {
            switch (i)
            {
                case 0:
                    foreach (Text t in answerOneTexts)
                        t.text = set.answers[i].answer;
                    break;

                case 1:
                    foreach (Text t in answerTwoTexts)
                        t.text = set.answers[i].answer;
                    break;

                case 2:
                    foreach (Text t in answerThreeTexts)
                        t.text = set.answers[i].answer;
                    break;

                case 3:
                    foreach (Text t in answerFourTexts)
                        t.text = set.answers[i].answer;
                    break;
            }
        }

        // Showing Relevant UI
        twoAnswerUI.SetActive(false);
        threeAnswerUI.SetActive(false);
        fourAnswerUI.SetActive(false);

        switch (set.answers.Length)
        {
            case 2:
                twoAnswerUI.SetActive(true);
                break;

            case 3:
                threeAnswerUI.SetActive(true);
                break;

            case 4:
                fourAnswerUI.SetActive(true);
                break;
        }
    }
}
