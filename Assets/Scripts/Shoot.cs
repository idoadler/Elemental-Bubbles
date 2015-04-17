using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject[] bullets;
	public float speed = 1f;

	void Update() {
		if (Input.GetMouseButtonDown(0))
		{
			GameObject projectile = (GameObject)Instantiate( bullets[Random.Range (0,bullets.Length)], transform.position, Quaternion.identity);
			projectile.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * speed;
		}
	}
}