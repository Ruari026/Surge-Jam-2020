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

    public delegate void EventDialogueFinished();
    public static EventDialogueFinished OnEventDialogueFinished;

    private AudienceMemberController interactingAudienceMember;

    [Header("General UI Elements")]
    [SerializeField]
    private GameObject backgroundFade;

    [Header("Dialogue UI Elements")]
    [SerializeField]
    private GameObject theDialogueUI;
    [SerializeField]
    private Text dialogueText;

    [Header("Question UI Elements")]
    [SerializeField]
    private GameObject theQuestionAnswerUI;
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
    private GameObject timerParent;
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


    /*
    ========================================================================================================================================================================================================
    Unity Methods
    ========================================================================================================================================================================================================
    */
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


    /*
    ========================================================================================================================================================================================================
    UI Access (General Opening & Closing)
    ========================================================================================================================================================================================================
    */
    public bool OpenUI(AudienceMemberController openingAudienceMember)
    {
        // Preventing a new audience member from interacting with the UI if another is already focused
        if (interactingAudienceMember == null)
        {
            interactingAudienceMember = openingAudienceMember;

            // Set has answer options so open up question answer UI
            UpdateUI(interactingAudienceMember.theQuestion);
            StartCoroutine(DelayUIOpen(interactingAudienceMember.theQuestion));

            return true;
        }
        else
        {
            return false;
        }
    }

    public void UIInteract(int answerNumber)
    {
        // Audience Member Handling
        if (interactingAudienceMember != null)
        {
            // Details of chosen answer
            AnswerDetailsSet answer = interactingAudienceMember.theQuestion.answers[answerNumber];

            // Checking if a marble needs to be spawned
            if (answer.type != AnswerTypes.NONE)
            {
                MarbleSpawner.instance.SpawnMarble(answer.type);
                PersistantData.instance.AddScore(answer.type);
            }

            // Reserving Special Numbers for Special Events
            if (answer.nextQuestion == -2)
            {
                // Special Case for Resetting Tutorial
                TutorialManager.instance.RestartTutorial();
            }

            // Determining Next Dialogue Step
            if (answer.nextQuestion >= 0)
            {
                // Load Next Question Answer Set
                int nextQuestion = interactingAudienceMember.theQuestion.answers[answerNumber].nextQuestion;
                QuestionAnswersScriptableObject nextSet = interactingAudienceMember.possibleProgressionQuestions[nextQuestion];
                ProgressUI(nextSet);
            }
            else
            {
                CloseUI(true);
            } 
        }
    }

    private void ProgressUI(QuestionAnswersScriptableObject nextSet)
    {
        interactingAudienceMember.theQuestion = nextSet;

        // Closing Existing UI
        theQuestionAnswerUI.SetActive(false);
        theDialogueUI.SetActive(false);

        // Checking if the next UI needs to be question answer or general dialogue
        UpdateUI(interactingAudienceMember.theQuestion);

        // Showing relevant UI
        if (nextSet.answers.Length > 1)
        {
            theQuestionAnswerUI.SetActive(true);
        }
        else
        {
            theDialogueUI.SetActive(true);
        }
        backgroundFade.SetActive(true);

        if (hasTimer)
        {
            timerParent.gameObject.SetActive(true);
        }
    }

    public void CloseUI(bool questionAnswered)
    {
        // End of dialogue tree
        interactingAudienceMember.success = questionAnswered;
        interactingAudienceMember.ChangeState(AudienceStates.AUDIENCE_EXIT);

        interactingAudienceMember = null;

        // Close All UI
        theQuestionAnswerUI.SetActive(false);
        theDialogueUI.SetActive(false);

        backgroundFade.SetActive(false);
        timerParent.SetActive(false);

        OnEventDialogueFinished?.Invoke();
    }


    /*
    ========================================================================================================================================================================================================
    Handling Question Answer Sets
    ========================================================================================================================================================================================================
    */
    private IEnumerator DelayUIOpen(QuestionAnswersScriptableObject set)
    {
        yield return new WaitForSeconds(0.75f);

        if (set.answers.Length > 1)
        {
            theQuestionAnswerUI.SetActive(true);
        }
        else
        {
            theDialogueUI.SetActive(true);
        }
        backgroundFade.SetActive(true);

        if (hasTimer)
        {
            timerParent.gameObject.SetActive(true);
        }
    }

    private void UpdateUI(QuestionAnswersScriptableObject set)
    {
        // Updating UI For Question
        questionText.text = set.question;
        dialogueText.text = set.question;

        // Answer Option UI's
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

        // Reset All UI's
        twoAnswerUI.SetActive(false);
        threeAnswerUI.SetActive(false);
        fourAnswerUI.SetActive(false);

        // Showing Relevant UI
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
