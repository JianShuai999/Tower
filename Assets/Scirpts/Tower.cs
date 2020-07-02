using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Transform Fire;
    public float attackDistance;
    public float attackInterval;//间隔时间
    private float preAttackTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Fire = transform.Find("Point").Find("Fire");
    }

    // Update is called once per frame
    void Update()
    {
        Monster tempMonster = GameObject.FindObjectOfType<Monster>();
        if (tempMonster != null)
        {
            if (Vector3.Distance(transform.position, tempMonster.transform.position) <= attackDistance)
            {
                Aim(tempMonster.transform);
                if((Time.time - preAttackTime) > attackInterval)
                {
                    Shoot();
                    preAttackTime = Time.time;
                }
            }
        }

    }

    public void Aim(Transform pTarget)
    {
        Quaternion tempQ = Quaternion.LookRotation(pTarget.position - transform.position);
        transform.eulerAngles = new Vector3(0, tempQ.eulerAngles.y, 0);
    }

    public void Shoot()
    {
        GameObject tempBullet = GameObject.Instantiate(bulletPrefab);
        tempBullet.transform.parent = null;
        tempBullet.transform.position = Fire.transform.position;
        tempBullet.transform.rotation = Fire.transform.rotation;
    }
        
}
