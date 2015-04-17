using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class Controller : MonoBehaviour {

    public Animator bigGuyAnimator;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            bigGuyAnimator.SetBool("Walk", true);
        }
    }

}