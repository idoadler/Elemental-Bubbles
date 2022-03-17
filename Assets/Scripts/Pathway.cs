using UnityEngine;


[System.Serializable]
public class PathNode {
    public Transform node;
    public PathNode(Transform node) {
        this.node = node;
    }
}

public class Pathway : MonoBehaviour {

    public PathNode[] nodes;

}