using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour {
    public List<Transform> canvasPos = new List<Transform>();
    public List<Transform> canvas = new List<Transform>();

    public void LoadScene(string levelName) {
        Application.LoadLevel(levelName);
    }



}

