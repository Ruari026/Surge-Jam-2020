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

            // Determining if the UI needs to open as a dialogue UI or question answer set UI
            if (interactingAudienceMember.theQuestion.answers.Length > 0)
            {
                // Set has answer options so open up question answer UI
                UpdateQuestionAnswerUI(interactingAudienceMember.theQuestion);
                StartCoroutine(OpenQuestionAnswerUI());
            }
            else
            {
                // No answer options so open as general dialogue
                UpdateDialogueUI(interactingAudienceMember.theQuestion);
                StartCoroutine(OpenDialogueUI());
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public void CloseUI(bool questionAnswered)
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
            MarbleSpawner.instance.SpawnMarble();
            PersistantData.instance.AddScore();
        }

        // Close UI
        theQuestionAnswerUI.SetActive(false);
        backgroundFade.SetActive(false);
        timerParent.SetActive(false);
    }

    public void CloseUI(int answerNumber)
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

            // Determining Next Dialogue Step
            if (answer.nextQuestion >= 0)
            {
                // Load Next Question Answer Set
                int nextQuestion = interactingAudienceMember.theQuestion.answers[answerNumber].nextQuestion;
                QuestionAnswersScriptableObject nextSet = interactingAudienceMember.possibleProgressionQuestions[nextQuestion];

                interactingAudienceMember.theQuestion = nextSet;

                UpdateQuestionAnswerUI(interactingAudienceMember.theQuestion);
            }
            else
            {
                // End of dialogue tree
                interactingAudienceMember.success = true;
                interactingAudienceMember.ChangeState(AudienceStates.AUDIENCE_EXIT);

                interactingAudienceMember = null;

                // Close All UI
                theQuestionAnswerUI.SetActive(false);
                theDialogueUI.SetActive(false);

                backgroundFade.SetActive(false);
                timerParent.SetActive(false);
            }

            OnEventDialogueFinished();
        }   
    }


    /*
    ========================================================================================================================================================================================================
    Handling Dialogue Sets
    ========================================================================================================================================================================================================
    */
    private IEnumerator OpenDialogueUI()
    {
        yield return new WaitForSeconds(0.75f);

        theDialogueUI.SetActive(true);
        backgroundFade.SetActive(true);

        if (hasTimer)
        {
            timerParent.gameObject.SetActive(true);
        }
    }

    private void UpdateDialogueUI(QuestionAnswersScriptableObject set)
    {
        dialogueText.text = set.question;
    }


    /*
    ========================================================================================================================================================================================================
    Handling Question Answer Sets
    ========================================================================================================================================================================================================
    */
    private IEnumerator OpenQuestionAnswerUI()
    {
        yield return new WaitForSeconds(0.75f);

        theQuestionAnswerUI.SetActive(true);
        backgroundFade.SetActive(true);

        if (hasTimer)
        {
            timerParent.gameObject.SetActive(true);
        }
    }

    private void UpdateQuestionAnswerUI(QuestionAnswersScriptableObject set)
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
