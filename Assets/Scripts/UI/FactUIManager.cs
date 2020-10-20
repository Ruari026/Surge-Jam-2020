using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactUIManager : MonoBehaviour
{
    public AnswerTypes factType;
    public Text textToUpdate;

    public virtual void UpdateFact()
    {
        PersistantData data = PersistantData.instance;
        float marbleAmount = data.GetMostPickedAnswerPercentage();

        string[] split = textToUpdate.text.Split('%');
        string s = (marbleAmount * 100).ToString("0.00");
        textToUpdate.text = split[0] + s + "%" + split[1];
    }
}
