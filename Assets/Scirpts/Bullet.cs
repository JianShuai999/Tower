using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float hurt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Monster tempHome = collision.collider.GetComponentInParent<Monster>();
        if (tempHome != null)
        {
            tempHome.Hurt(hurt);
            Destroy(gameObject);
        }
    }
}
