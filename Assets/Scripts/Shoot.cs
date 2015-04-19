using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public BubbleConnector[] bullets;
	public float speed = 1f;

	private int next;

	public SpriteRenderer glowSprite;
	public float glowTime = 0.3f;
	public float glowOffTime = 0.1f;
	public float destinationScale = 0.1462512f;

	bool isInMiddleOfShot = false;

	Color shipClueColor;

	public SpriteRenderer[] shipParts;

	void Start()
	{
		next = Random.Range (0,bullets.Length);
		shipClueColor = getNextColor();
//		gameObject.GetComponent<MeshRenderer>().material.color = bullets[next].GetComponent<MeshRenderer>().sharedMaterial.color;

		StartCoroutine(clueColorCo());
	}

	void Update() {
		if (Input.GetMouseButtonDown(0) && isInMiddleOfShot == false ){
			StartCoroutine(prepCo ());
		}
	}

	IEnumerator prepCo(){
		isInMiddleOfShot = true;

		Color nextColor = getNextColor();
		int current = next;
		next = Random.Range (0,bullets.Length);

		float timePassed = 0;
		while( timePassed < glowTime ){

			float ratio = timePassed / glowTime;

			glowSprite.transform.localScale = Vector3.one * ratio * destinationScale;
			glowSprite.color = new Color(
				nextColor.r,
				nextColor.g,
				nextColor.b,
				ratio
				);

			timePassed += Time.deltaTime;
			yield return null;
		}

		glowSprite.transform.localScale = Vector3.one * destinationScale;
		glowSprite.color = nextColor;

		timePassed = 0;
		while( timePassed < glowOffTime ){
			
			float ratio = timePassed / glowOffTime;
			
			glowSprite.transform.localScale = Vector3.one * (1 - ratio) * destinationScale;
			glowSprite.color = new Color(
				nextColor.r,
				nextColor.g,
				nextColor.b,
				1 - ratio
				);
			
			timePassed += Time.deltaTime;
			yield return null;
		}

		shoot (current);

		isInMiddleOfShot = false;
	}

	Color getNextColor(){
		return bullets[next].color;
	}

	IEnumerator clueColorCo(){
		while( true ){

			foreach(SpriteRenderer shipPart in shipParts){
				shipPart.color = new Color(
					Mathf.Lerp (shipPart.color.r, shipClueColor.r, Time.deltaTime),
					Mathf.Lerp (shipPart.color.g, shipClueColor.g, Time.deltaTime),
					Mathf.Lerp (shipPart.color.b, shipClueColor.b, Time.deltaTime),
					1
					);
			}

			yield return null;
		}
	}

	void shoot(int index){
		GameObject projectile = (GameObject)Instantiate( bullets[index].gameObject, transform.position, Quaternion.identity);
		projectile.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * speed;
		//gameObject.GetComponent<MeshRenderer>().material.color = bullets[next].GetComponent<MeshRenderer>().sharedMaterial.color;

		shipClueColor = getNextColor();
	}
}