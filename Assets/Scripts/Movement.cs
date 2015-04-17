using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class Movement : MonoBehaviour {



    Pathway myPath;

    int nextPathNodeIndex = 0;
    PathNode nextPathNode = null;
    float reachDistance = 0.3f;


	public GameObject bossBody;
    public float speed = 1;

    void Awake() {
        myPath = GetComponent<Pathway>();

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
			//bossBody.transform.LookAt(Vector3.Slerp(bossBody.transform.forward,nextPathNode.node.transform.position,0.5f));
			bossBody.transform.rotation = Quaternion.Slerp(
				bossBody.transform.rotation,
				Quaternion.LookRotation(nextPathNode.node.position - transform.position),
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