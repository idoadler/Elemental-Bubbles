using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class Movement : MonoBehaviour {



    Pathway myPath;

    int nextPathNodeIndex = 0;
    PathNode nextPathNode = null;
    float reachDistance = 0.3f;


    public float speed = 1;

    void Awake() {
        myPath = GetComponent<Pathway>();

    }

    void Start() {
        StartWalkingOnPath();
    }

    public void StartWalkingOnPath() {
        getNextPathNode();

        StopAllCoroutines();
        StartCoroutine(walkToNextNode());
    }

    void getNextPathNode() {
        nextPathNode = myPath.nodes[nextPathNodeIndex];
        nextPathNodeIndex = ++nextPathNodeIndex % myPath.nodes.Length;
    }

    IEnumerator walkToNextNode() {

        float distance = Vector3.Distance(transform.position, nextPathNode.node.position);

        while (true) {

            Vector3 walkVector = (nextPathNode.node.position - transform.position).normalized * speed * Time.deltaTime;
            transform.Translate(Vector3.left, Space.Self);

            if (distance < reachDistance) {
                getNextPathNode();
            }

            yield return null;
        }

    }

}