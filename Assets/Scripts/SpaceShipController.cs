using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class SpaceShipController : MonoBehaviour {

    public float maxSpeed;
    public float accelaration;
    public float deaccelaration;

    Vector2 currentVelocity = Vector2.zero;

    Rigidbody myRigidbody;

    void Awake() {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update() {

        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        Vector2 velocityVector = Vector2.zero;
        velocityVector.x += deltaX;
        velocityVector.y += deltaY;

        velocityVector = velocityVector.normalized * Time.deltaTime * (accelaration + deaccelaration);

        Vector2 deaccelarationVector = currentVelocity.normalized * -1 * deaccelaration * Time.deltaTime;

        if (deaccelarationVector.magnitude > (currentVelocity + velocityVector).magnitude) {
            currentVelocity = Vector2.zero;
        } else {
            currentVelocity = currentVelocity + velocityVector + deaccelarationVector;
        }
        
        if (currentVelocity.magnitude > maxSpeed) {
            currentVelocity = currentVelocity.normalized * maxSpeed;
        }

        Vector3 inverted;
        inverted.x = currentVelocity.x;
        inverted.y = 0;
        inverted.z = currentVelocity.y;
        //transform.Translate(inverted * Time.deltaTime);

        myRigidbody.velocity = inverted;

		Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target = new Vector3(target.x, target.y, 0f);
		transform.LookAt(target);

    }	

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
			myRigidbody.velocity = Vector3.zero;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
			Application.LoadLevel(0);
	}
}