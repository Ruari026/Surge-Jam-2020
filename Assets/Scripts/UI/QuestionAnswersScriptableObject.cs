using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestionAnswerSet", menuName ="ScriptableObjects/QuestionAnswerSO")]
public class QuestionAnswersScriptableObject : ScriptableObject
{
    public string question;
    public string[] answers = new string[4];
}
