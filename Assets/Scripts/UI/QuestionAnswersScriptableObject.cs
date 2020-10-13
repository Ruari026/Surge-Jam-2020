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
    public AnswerTypes type;
}

public enum AnswerTypes
{
    RELATIONSHIPS = 0,
    EDUCATION = 1,          
    ECONOMY = 2,            
    NATURE = 3,             
    HUMANITY = 4,          
    ARTS = 5,
}