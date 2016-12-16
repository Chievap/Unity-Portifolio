using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GM : MonoBehaviour {

    public  Camera camera;
	public	float 	moveSpeed;
	public 	int 	enemyCount;
	public 	List<GameObject> enemys = new List<GameObject>();
    public  GameObject enemyPrefab;
    public  GameObject cubePrefab;

    public GameObject[] towers;

    public GameObject grid;

    public GameObject selectedTower;

    public bool selecting;

    public void StartPath() {
        grid.GetComponent<Grid>().CreateGrid();
        

        for (int i = 0; i < enemys.Count; i++) {
            enemys[i].GetComponent<Unit>().StartPath();
        }
    }

    void Update() {
        if (selecting) {
            if (Input.GetButtonDown("Fire1")) {
                //Instantiate
            }
        }
    }

	
	public void AddEnemy (){
		enemyCount ++;

	}
    public void BackToMainMenu() {
        Application.LoadLevel("MapGenerator");
    }

    public Vector3 WorldPos() {

        Vector3 mousePosInWorld = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosInWorld.z = 20;
        print(mousePosInWorld    + "Mouse Position");
        return mousePosInWorld;
    }

    public void SetTower(GameObject tower) {
        selectedTower = tower;
    }
}
