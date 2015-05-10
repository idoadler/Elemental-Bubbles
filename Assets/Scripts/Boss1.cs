using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {

	private bool started = false;

	public Color[] colors;
	public SpriteRenderer[] indicators;
	public int health = 3;
    public AudioSource bigBoom;
    public AudioSource smallBoom;

	private void hit()
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
            {
                bigBoom.Play();
                Destroy(transform.parent.gameObject);
            }
				// destroy shield and root
		}
	}

	private int color;
	public void init()
	{
        smallBoom.Play();
	    started = true;
	    color = Random.Range (0, colors.Length);
	    foreach (SpriteRenderer renderer in indicators)
		    renderer.color = colors [color];
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer ("Bubble")) {
			BubbleConnector otherBubble = other.GetComponent<BubbleConnector> ();
			if (otherBubble.color == colors[color] || !started)
			{
				Destroy(other.gameObject);
				hit();
			}
		}
	}
}
