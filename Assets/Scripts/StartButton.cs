using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    public GameObject buttonOn;
    public GameObject buttonOff;

    public GameObject game;

    void OnMouseDown()
    {
        game.active = true;
        gameObject.active = false;
    }

    void OnMouseEnter()
    {
        buttonOn.active = true;
        buttonOff.active = false;
    }

    void OnMouseExit()
    {
        buttonOn.active = false;
        buttonOff.active = true;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
