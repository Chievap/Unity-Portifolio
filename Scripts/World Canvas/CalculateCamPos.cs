using UnityEngine;
using System.Collections;

public class CalculateCamPos : MonoBehaviour {
    public int curCanvasNumber;
    public int lerpSpeed;
    public Vector3 prefCamPos;
    public GameObject manager;
    public bool moving;
    public float speed = 2.5f;

    void Update() {
        if (Input.GetButtonDown("Submit")) {
            
            MoveNext();
        }

        Lookat();
    }

    public void MoveNext() {
        curCanvasNumber++;
        moving = true;
        
    }

    public void MoveBack() {
        curCanvasNumber--;
        moving = true;
    }
	
	void LateUpdate () {
        transform.position = Vector3.Lerp(transform.position, manager.GetComponent<CanvasManager>().canvasPos[curCanvasNumber].position, lerpSpeed * Time.deltaTime);
        //transform.LookAt(manager.GetComponent<CanvasManager>().canvas[curCanvasNumber]);    
	}

    public void OnValidate() {
        if (curCanvasNumber > manager.GetComponent<CanvasManager>().canvasPos.Count) {
            curCanvasNumber = manager.GetComponent<CanvasManager>().canvasPos.Count;
        }

        if (curCanvasNumber < 0) {
            curCanvasNumber = 0;
        }
    }

    

    void Lookat() {
        Vector3 dir = manager.GetComponent<CanvasManager>().canvas[curCanvasNumber].transform.position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed * Time.deltaTime);
    }
}
