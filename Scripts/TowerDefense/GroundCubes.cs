using UnityEngine;
using System.Collections;

public class GroundCubes : MonoBehaviour {

    public GameObject gM;
    public GameObject curTower;
    public GameObject tmpTower;
    public bool placing = false;
    private int money;
	
	void Awake () {
        gM = GameObject.FindWithTag("A*");
	}
	void Update() {
        if (placing) {
            CheckForFire();
        }
    }

    void CheckForFire() {
        if (curTower != null) {
            if (Input.GetButtonDown("Fire1")) {
                if (gM.GetComponent<Money>().money > tmpTower.GetComponent<Tower>().moneyCost) {
                    money = tmpTower.GetComponent<Tower>().moneyCost;
                    gM.GetComponent<Money>().RemoveMoney(money);
                    tmpTower.AddComponent<BoxCollider>();
                    tmpTower.GetComponent<BoxCollider>().isTrigger = true;
                    if (curTower.GetComponent<Tower>().towerType == Tower.TowerType.Sniper) {
                        tmpTower.GetComponent<BoxCollider>().size = new Vector3(25, 25, 25);
                    } else {
                        tmpTower.GetComponent<BoxCollider>().size = new Vector3(10, 10, 10);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }


    void OnMouseEnter() {
        
        if (!placing) {
            placing = true;
            print("On Mouse Enter");
            curTower = gM.GetComponent<GM>().selectedTower;
            gM.GetComponent<GM>().selecting = true;

            tmpTower = Instantiate(curTower, transform.position, Quaternion.identity) as GameObject;
           
        }
        
    }

    void OnMouseExit() {
        placing = false;
        Destroy(tmpTower);
        gM.GetComponent<GM>().selecting = false;
    }
}
