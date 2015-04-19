using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {

	private bool started = false;

	public Color[] colors;
	public SpriteRenderer[] indicators;
	public int health = 3;

	public void hit()
	{
		if (started == false) 
		{
			init();
		} 
		else 
		{
			health--;
			if (health > 0)
				init();
			else
				Destroy(transform.parent.gameObject);
				// destroy shield and root
		}
	}

	private int color;
	void init()
	{
		color = Random.Range (0, colors.Length);
		foreach (SpriteRenderer renderer in indicators)
			renderer.color = colors [color];
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
