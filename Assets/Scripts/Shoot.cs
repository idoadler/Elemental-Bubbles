using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject[] bullets;
	public float speed = 1f;

	private int next;

	void Start()
	{
		next = Random.Range (0,bullets.Length);
		gameObject.GetComponent<MeshRenderer>().material.color = bullets[next].GetComponent<MeshRenderer>().sharedMaterial.color;
	}

	void Update() {
		if (Input.GetMouseButtonDown(0))
		{
			GameObject projectile = (GameObject)Instantiate( bullets[next], transform.position, Quaternion.identity);
			projectile.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * speed;
			next = Random.Range (0,bullets.Length);
			gameObject.GetComponent<MeshRenderer>().material.color = bullets[next].GetComponent<MeshRenderer>().sharedMaterial.color;
		}
	}
}