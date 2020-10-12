using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    [SerializeField]
    private Material[] possibleMaterials;
    [SerializeField]
    private AnswerTypes marbleType;
    [SerializeField]
    private Transform oobPlane;

    // Start is called before the first frame update
    void Start()
    {
        oobPlane = GameObject.FindGameObjectWithTag("Out Of Bounds").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        if (pos.y < oobPlane.position.y)
        {
            LevelManager.instance.EndGame();
        }
    }

    public void RandomizeMarbleType()
    {
        int pickedMaterial = Random.Range(0, possibleMaterials.Length);
        this.GetComponent<Renderer>().material = possibleMaterials[pickedMaterial];
    }

    public void SetMarbleType(AnswerTypes answerType)
    {
        marbleType = answerType;

        Material pickedMaterial = this.GetComponent<Renderer>().material;
        switch (answerType)
        {
            case AnswerTypes.RELATIONSHIPS:
                pickedMaterial = possibleMaterials[0];
                break;

            case AnswerTypes.EDUCATION:
                pickedMaterial = possibleMaterials[1];
                break;

            case AnswerTypes.ECONOMY:
                pickedMaterial = possibleMaterials[2];
                break;

            case AnswerTypes.NATURE:
                pickedMaterial = possibleMaterials[3];
                break;

            case AnswerTypes.HUMANITY:
                pickedMaterial = possibleMaterials[4];
                break;

            case AnswerTypes.ARTS:
                pickedMaterial = possibleMaterials[5];
                break;
        }
        this.GetComponent<Renderer>().material = pickedMaterial;
    }
}
