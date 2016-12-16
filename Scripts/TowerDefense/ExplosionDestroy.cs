using UnityEngine;
using System.Collections;

public class ExplosionDestroy : MonoBehaviour {

    void Awake() {
        Transform parent;
        parent = transform;

        foreach (Transform child in parent) {
            Destroy(child.gameObject , 5);
        }
        Destroy(gameObject, 5);
    }
}
