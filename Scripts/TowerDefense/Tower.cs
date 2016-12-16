using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
    public enum TowerType{ Normal, Sniper, Explode}
    public Transform curEnemy;
    public TowerType towerType;
    public float range;
    public GameObject bullets;
    public int waitTime;
    public int moneyCost;


    
    void Update() {
        if (curEnemy != null) {
            if(Vector3.Distance(transform.position,curEnemy.position) > range) {
                curEnemy = null;
            }
        }
    }

    public IEnumerator ShootTimer() {
        yield return new WaitForSeconds(waitTime);
        if(curEnemy != null) {
            Shoot();
        }
        StopCoroutine("ShootTimer");
        StartCoroutine("ShootTimer");
    }


    void OnTriggerEnter(Collider col) {
        print("Enemys Enter");
        if (curEnemy == null) {
            if (col.transform.tag == "Enemy") {
                StartCoroutine("ShootTimer");
                curEnemy = col.transform;
            }
        }
        
    }

    public void Shoot() {
        GameObject tempBullet;
        Vector3 tempPos;
        tempPos = transform.position;
        tempPos.y = transform.position.y + 2;
        switch (towerType) {
            case TowerType.Normal:
                if (curEnemy != null) {
                    tempBullet = Instantiate(bullets,tempPos , Quaternion.identity) as GameObject;
                    tempBullet.GetComponent<Bullet>().type = Bullet.BulletType.Normal;
                    tempBullet.GetComponent<Bullet>().target = curEnemy;
                }
                break;
            case TowerType.Sniper:
                if (curEnemy != null) {
                    tempBullet = Instantiate(bullets, tempPos, Quaternion.identity) as GameObject;
                    tempBullet.GetComponent<Bullet>().target = curEnemy;
                    tempBullet.GetComponent<Bullet>().type = Bullet.BulletType.Sniper;
                    print("Sniper Shoot");
                }
                break;
            case TowerType.Explode:
                if (curEnemy != null) {
                    tempBullet = Instantiate(bullets, tempPos, Quaternion.identity) as GameObject;
                    tempBullet.GetComponent<Bullet>().target = curEnemy;
                    tempBullet.GetComponent<Bullet>().type = Bullet.BulletType.Explode;
                }
                break;
        }
    }
}
    