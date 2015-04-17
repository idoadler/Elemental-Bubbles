using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class NodeGizmo : MonoBehaviour {

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }

}