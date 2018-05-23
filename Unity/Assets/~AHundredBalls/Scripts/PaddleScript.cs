using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{

    private Animator anim;

    void Start()
    {
        // get the animator component
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // check if down arrow is pressed
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // modify parameter we created ealier
            anim.SetBool("isOpened", true);
        }
        else
        {
            // set the parameter to false
            anim.SetBool("isOpened", false);
        }
    }

}
