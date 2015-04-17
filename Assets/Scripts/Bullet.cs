using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
		{
			gameObject.layer = LayerMask.NameToLayer("Boss");
			Destroy(gameObject.GetComponent<Rigidbody>());
			gameObject.transform.SetParent(other.transform.parent);
		}
	}
}
