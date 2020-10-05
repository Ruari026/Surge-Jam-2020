using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    [SerializeField]
    private Material[] possibleMaterials;
    [SerializeField]
    private Transform oobPlane;

    // Start is called before the first frame update
    void Start()
    {
        int pickedMaterial = Random.Range(0, possibleMaterials.Length);
        this.GetComponent<Renderer>().material = possibleMaterials[pickedMaterial];

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
}
