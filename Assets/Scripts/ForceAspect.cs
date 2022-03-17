using UnityEngine;

public class ForceAspect : MonoBehaviour {

	Camera cam;

	void Awake(){
		cam = GetComponent<Camera>();
	}

	void Start(){
		cam.aspect = 16.0f / 10.0f;
	}
}
