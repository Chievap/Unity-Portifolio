using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Unit : Grid {


	public GameObject target;
	public Vector3 oldTargetPos;
	public float speed = 20;
	Vector3[] path;
	int targetIndex;
	public float timersecs;

    public bool first;
    public bool towerDefense;


	void Awake() {
        target = GameObject.Find("Player");
        if (!towerDefense) {
            oldTargetPos = target.transform.position;
        }
		//PathRequestManager.RequestPath(transform.position,target.position, OnPathFound);
		//StartCoroutine(Timer());
	}

	public void StartPath(){
		StartCoroutine(Timer());
	}

	public IEnumerator Timer (){
       
		yield return new WaitForSeconds(timersecs);
		//print("Corotine");
		if(target.transform.position != oldTargetPos || first == true){
			PathRequestManager.RequestPath(transform.position,target.transform.position, OnPathFound);
			oldTargetPos = target.transform.position;
            first = false;
		}
		StartCoroutine(Timer());
	}

	
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path[0];

		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) { 
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
			yield return null;

		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}