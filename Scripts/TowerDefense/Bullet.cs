using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public enum BulletType { Normal, Sniper, Explode }
    public Transform target;
    public float speed;
    public BulletType type;
    public GameObject explsion;

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (target == null) {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter(Collision col) {
        switch (type) {
            case BulletType.Normal:
                col.gameObject.GetComponent<HP>().TakeDamage(50);
                break;
            case BulletType.Sniper:
                col.gameObject.GetComponent<HP>().TakeDamage(100);
                break;
            case BulletType.Explode:
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
                for(int i = 0; i <= hitColliders.Length; i++) {
                    if(hitColliders[i].transform.tag == "Enemy") {
                        hitColliders[i].GetComponent<HP>().TakeDamage(70);
                    }
                    Instantiate(explsion, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                break;
        }
        Destroy(gameObject);
    }
}
