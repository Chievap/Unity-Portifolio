using UnityEngine;
using System.Collections;

public class HP : MonoBehaviour {
    public GameObject gm;
    public int hp = 100;


    void Awake() {
        gm = GameObject.FindWithTag("A*");
    }


    public void TakeDamage(int damage) {
        hp -= damage;

        if(hp <= 0) {
            gm.GetComponent<Money>().AddMoney(20);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col) {
        if(col.transform.tag == "End") {
            col.gameObject.GetComponent<End>().hp--;
            Destroy(gameObject);
        }
    }
	
}
