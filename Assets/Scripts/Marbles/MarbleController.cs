using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    [SerializeField]
    private Material[] possibleMaterials;
    [SerializeField]
    private float oobThreshold = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        int pickedMaterial = Random.Range(0, possibleMaterials.Length - 1);
        this.GetComponent<Renderer>().material = possibleMaterials[pickedMaterial];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        if (pos.z < oobThreshold)
        {
            LevelManager.instance.EndGame();
        }
    }
}
