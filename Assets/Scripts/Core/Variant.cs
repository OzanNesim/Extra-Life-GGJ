using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variant : MonoBehaviour
{
    
    void Awake()
    {

        int rnd = Random.Range(0, transform.childCount - 1);
        Debug.Log(rnd);

        foreach (Transform trans in transform)
        {
            trans.gameObject.SetActive(false);
        }

        transform.GetChild(rnd).gameObject.SetActive(true);
    }
}
