using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestionAnswerSet", menuName ="ScriptableObjects/QuestionAnswerSO")]
public class QuestionAnswersScriptableObject : ScriptableObject
{
    public string question;
    public AnswerDetailsSet[] answers = new AnswerDetailsSet[4];
}

[System.Serializable]
public struct AnswerDetailsSet
{
    public string answer;
    public AnswerTypes type;
    public int nextQuestion;
}

public enum AnswerTypes
{
    RELATIONSHIPS = 0,
    EDUCATION = 1,          
    ECONOMY = 2,            
    NATURE = 3,             
    HUMANITY = 4,          
    ARTS = 5,
    NONE = 6
}