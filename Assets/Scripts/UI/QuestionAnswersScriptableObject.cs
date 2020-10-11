using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestionAnswerSet", menuName ="ScriptableObjects/QuestionAnswerSO")]
public class QuestionAnswersScriptableObject : ScriptableObject
{
    public string question;
    public QuestionTypeSet[] answers = new QuestionTypeSet[4];
}

[System.Serializable]
public struct QuestionTypeSet
{
    public string answer;
    [EnumFlags]
    public AnswerTypes type;
}

[System.Flags]
public enum AnswerTypes
{
    Nothing = 0,                // 000000
    RELATIONSHIPS = 1,      // 000001
    EDUCATION = 2,          // 000010
    ECONOMY = 4,            // 000100
    NATURE = 8,             // 001000
    HUMANITY = 16,          // 010000
    ARTS = 36,              // 100000
    Everything = 0xFFFFFFF
}