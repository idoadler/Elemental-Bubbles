using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
		{
			if (other.gameObject.GetComponent<MeshRenderer>().material.color == gameObject.GetComponent<MeshRenderer>().material.color)
			{
				Destroy( other.gameObject);
				Destroy(gameObject);
			}
			else
			{
				gameObject.layer = LayerMask.NameToLayer("Boss");
				Destroy(gameObject.GetComponent<Rigidbody>());
				gameObject.transform.SetParent(other.transform.parent);
			}
		}
	}
}
