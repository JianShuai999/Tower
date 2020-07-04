using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public float hurt;
    public float Blood;
    public float addcost;
    public Transform endPoint;
    private NavMeshAgent navMeshAgent;
    public GameObject DeadEffect;
    public UnityAction<Monster> onDeadAction;

    // Start is called before the first frame update
    void Start()
    {
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
        GameObject tempDeadEffect =  GameObject.Instantiate(DeadEffect);
        tempDeadEffect.transform.parent = null;
        tempDeadEffect.transform.position = transform.position;
        tempDeadEffect.transform.rotation = transform.rotation;
        onDeadAction?.Invoke(this);
        Destroy(gameObject);
     }
}
