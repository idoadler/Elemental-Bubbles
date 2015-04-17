using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour {

	public Movement bossMovement;
	public Vector3 velocityChange;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
			bossMovement.SetNextNodeOnPath();
		else if (other.gameObject.layer == LayerMask.NameToLayer("Bubble"))
			other.GetComponent<Rigidbody>().velocity = new Vector3 (other.GetComponent<Rigidbody>().velocity.x * velocityChange.x,other.GetComponent<Rigidbody>().velocity.y * velocityChange.y,other.GetComponent<Rigidbody>().velocity.z * velocityChange.z);
	}
}
