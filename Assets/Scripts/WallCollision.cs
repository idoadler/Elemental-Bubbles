using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour {

	public Movement bossMovement;

	void OnTriggerEnter(){
		bossMovement.SetNextNodeOnPath();
	}
}
