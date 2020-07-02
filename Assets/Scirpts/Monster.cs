using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public float hurt;
    public float Blood;
    private Transform endPoint;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        endPoint = GameObject.Find("EndPoint").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Home tempHome = collision.collider.GetComponentInParent<Home>();
        if (tempHome != null)
        {
            tempHome.Hurt(hurt);
            Destroy(gameObject);
        }
    }

    public void Hurt(float pHurtValue)
    {
        Blood -= pHurtValue;
        if (Blood <= 0)
        {
            Blood = 0;
            OnDead();
        }
    }

    public void OnDead()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("SucceedScene");
    }
}
