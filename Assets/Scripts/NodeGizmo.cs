using UnityEngine;

public class NodeGizmo : MonoBehaviour {
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}