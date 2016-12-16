using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class End : MonoBehaviour {
    public int hp;
    public Text text;

    void Update() {
        text.text = "Lives Left: " + hp;
    }
}
