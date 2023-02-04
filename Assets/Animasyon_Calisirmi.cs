using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animasyon_Calisirmi : MonoBehaviour
{
    public Animator anim;
    public LayerMask layerMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            this.anim.SetBool("jump", true);
        }
        else
        {
            this.anim.SetBool("jump", false);
        }
    }
}
