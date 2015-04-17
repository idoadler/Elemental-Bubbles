using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour {

	public Movement bossMovement;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
			bossMovement.SetNextNodeOnPath();
		else if (other.gameObject.layer == LayerMask.NameToLayer("Bubble"))
			other.GetComponent<Rigidbody>().velocity = -other.GetComponent<Rigidbody>().velocity;
	}
}
