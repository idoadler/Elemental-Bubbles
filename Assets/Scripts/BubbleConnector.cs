using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public enum EBubbleColor{
	RED,
	BLUE,
	YELLOW,
	GREEN,
	SILVER
}

public class BubbleConnector : MonoBehaviour {

	static List<BubbleConnector> registeredBubbles = new List<BubbleConnector>();
	static void Register(BubbleConnector myself){
		registeredBubbles.Add(myself);
	}

	public EBubbleColor color;	
	public Color myColor;
	public List<BubbleConnector> connections = new List<BubbleConnector>();

	public bool amBoss = false;

	[HideInInspector]
	public bool checkBool = false;

	public bool isPresentAtStart;

	bool isChainToDestroy = false;
	bool isFree = true;

	Vector3 randomRotate = Vector3.zero;
	float randomSpeed = 0;

	void Awake(){
		registeredBubbles.Add(this);
		isFree = !isPresentAtStart;
	}

	void Start(){
		if( isPresentAtStart == true ){
			List<BubbleConnector> allCloseBubbles = getAllConnectedBubbles(1.3f);
			foreach( BubbleConnector bubble in allCloseBubbles ){
				if( bubble.connections.Contains(this) == false ){
					bubble.connections.Add(this);
				}
				if( connections.Contains(bubble) == false ){
					connections.Add(bubble);
				}
			}
		}

		randomRotate = new Vector3(
			Random.Range(-10, 10),
			Random.Range (-10, 10),
			Random.Range(-10, 10)
			);

		randomSpeed = Random.Range (-30.0f, 30.0f);
	}

	void Update(){
		if( amBoss == false){
			if( isFree == true ){
				transform.Rotate(randomRotate, randomSpeed * Time.deltaTime * 5);
			}else{
				transform.Rotate (
					Movement.instance.lever.position - Movement.instance.leverBase.position,
					50 * Time.deltaTime
				);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
		{
			BubbleConnector otherBubble = other.GetComponent<BubbleConnector>();

			if (otherBubble.color == color)
			{
				bool isThirdInChain = false;
				foreach(BubbleConnector bubble in otherBubble.connections ){
					if( bubble.color == color ){
						isThirdInChain = true;
					}
				}

				if( isThirdInChain == true ){
					otherBubble.DestroyMeAndMyChain();
					Destroy(gameObject);

					checkAndDestroyLeftovers();
				}else{
					connectNewBubble(otherBubble);
				}
			}
			else
			{
				connectNewBubble(otherBubble);
			}
		}
	}

	void connectNewBubble(BubbleConnector other){
		if( other == this )
			return;

		List<BubbleConnector> allCloseBubbles = getAllConnectedBubbles( Vector3.Distance(transform.position, other.transform.position) * 1.1f);
		foreach( BubbleConnector bubble in allCloseBubbles ){
			if( bubble.connections.Contains(this) == false ){
				bubble.connections.Add(this);
			}
			if( connections.Contains(bubble) == false ){
				connections.Add(bubble);
			}
		}
		
		gameObject.layer = LayerMask.NameToLayer("Boss");
		Destroy (gameObject.GetComponent<Rigidbody>());
		gameObject.transform.SetParent(other.transform.parent);
		isFree = false;
	}

	public bool CheckIfAmConnectedToBoss(){

		if( isChainToDestroy == true ){
			return true;
		}

		if( amBoss == true ){
			return true;
		}

		if( gameObject.layer == LayerMask.NameToLayer("Bubble") ){
			return true;
		}

		List<BubbleConnector> checkList = connections;
		List<BubbleConnector> switchList = new List<BubbleConnector>();

		while( checkList.Count > 0 ){

			foreach(BubbleConnector bubble in checkList){

				if( bubble.amBoss == true ){
					uncheckAllBubbles();
					return true;
				}

				if( bubble.checkBool == false ){
					bubble.checkBool = true;
					foreach(BubbleConnector bubble2 in bubble.connections){
						switchList.Add(bubble2);
					}
				}

			}

			checkList = switchList;
			switchList = new List<BubbleConnector>();
		}

		uncheckAllBubbles();
		return false;
	}

	//contains recursion
	public void DestroyMeAndMyChain(){
		if( isChainToDestroy == true ){
			return;
		}
		foreach(BubbleConnector bubble in connections){
			bubble.connections.Remove(this);
		}

		foreach(BubbleConnector bubble in connections){
			if( bubble.color == color && bubble.amBoss == false){
				bubble.DestroyMeAndMyChain();
			}
		}

		connections.Clear();
		connections = null;
		Destroy (gameObject);

		isChainToDestroy = true;
	}

	List<BubbleConnector> getAllConnectedBubbles(float minConnectionDistance){
		List<BubbleConnector> returnedConnections = new List<BubbleConnector>();

		foreach(BubbleConnector bubble in registeredBubbles){
			if( bubble != this ){
				if( Vector3.Distance(bubble.transform.position, transform.position) <= minConnectionDistance ){
					returnedConnections.Add(bubble);
				}
			}
		}

		return returnedConnections;
	}

	void checkAndDestroyLeftovers(){
		List<BubbleConnector> doomedList = new List<BubbleConnector>();
		foreach(BubbleConnector bubble in registeredBubbles){
			if( bubble.amBoss == false ){
				if( bubble.CheckIfAmConnectedToBoss() == false ){
					doomedList.Add(bubble);
				}
			}
		}

		foreach( BubbleConnector bubble in doomedList){
			foreach(BubbleConnector bubble2 in bubble.connections){
				bubble2.connections.Remove(bubble);
			}

			bubble.connections.Clear();
			bubble.connections = null;
			Destroy (bubble.gameObject);
		}


	}

	void uncheckAllBubbles(){
		foreach(BubbleConnector bubble in registeredBubbles){
			bubble.checkBool = false;
		}
	}

	void OnDestroy(){
		registeredBubbles.Remove(this);
	}

}
