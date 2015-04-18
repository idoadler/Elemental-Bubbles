using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class Movement : MonoBehaviour {

	public static Movement instance;

    Pathway myPath;

    int nextPathNodeIndex = 0;
    PathNode nextPathNode = null;
    float reachDistance = 0.3f;

	public Transform lever;
	public Transform leverBase;

	public GameObject bossBody;
    public float speed = 1;

    void Awake() {
        myPath = GetComponent<Pathway>();
		instance = this;
    }

    void Start() {
        StartWalkingOnPath();
    }

    public void StartWalkingOnPath() {
        SetNextNodeOnPath();

        StopAllCoroutines();
        StartCoroutine(walkToNextNode());
    }

    public void SetNextNodeOnPath() {
        nextPathNode = myPath.nodes[nextPathNodeIndex];
        nextPathNodeIndex = ++nextPathNodeIndex % myPath.nodes.Length;
    }

    IEnumerator walkToNextNode() {

        float distance = Vector3.Distance(transform.position, nextPathNode.node.position);

        while (true) {

            Vector3 walkVector = (nextPathNode.node.position - transform.position).normalized * speed * Time.deltaTime;
            transform.Translate(walkVector, Space.World);
			bossBody.transform.rotation = Quaternion.Slerp(
				bossBody.transform.rotation,
				Quaternion.LookRotation(nextPathNode.node.position - transform.position,Vector3.back),
				speed * Time.deltaTime
			);
            distance = Vector3.Distance(transform.position, nextPathNode.node.position);

            if (distance < reachDistance) {
                SetNextNodeOnPath();
            }

            yield return null;
        }

    }

}