using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public float Blood;
    public GameObject DeadEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        GameObject tempDeadEffect = GameObject.Instantiate(DeadEffect);
        tempDeadEffect.transform.parent = null;
        tempDeadEffect.transform.position = transform.position;
        tempDeadEffect.transform.rotation = transform.rotation;
        transform.GetChild(0).gameObject.SetActive(false);
        Invoke("Load", 2);
    }

    public void Load()
    {
        SceneManager.LoadScene("FailScene");
    }

   


}
