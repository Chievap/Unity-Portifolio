using UnityEngine;
using System.Collections;

public class MouseInputManager : MonoBehaviour {

    public Camera camera;
    public GameObject cubeToSpawn;
    public GameObject enemyPrefab;
    private RaycastHit hit;
    public GameObject gM;
    public bool towerDefense;

    // Use this for initialization
    void Start() {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (!towerDefense) {
            if (Input.GetButton("Fire1")) {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                    if (hit.transform.tag == "Ground") {
                        Instantiate(cubeToSpawn, hit.point, Quaternion.identity);
                    }
            }
            if (Input.GetButtonDown("Fire2")) {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                    if (hit.transform.tag == "Ground") {
                        GameObject tempEnemy;
                        tempEnemy = Instantiate(enemyPrefab, (hit.point + new Vector3(0, 0.5f, 0)), Quaternion.identity) as GameObject;
                        gM.GetComponent<GM>().enemys.Add(tempEnemy);
                    }
            }
        } else {
            if (Input.GetButton("Fire1")) {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                    if (hit.transform.tag == "SpawnCube") {
                        print("hit ");
                        Destroy(hit.transform.gameObject);
                    }
            }
        }
    }
}