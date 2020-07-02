using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public float Blood;

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
        Destroy(gameObject);
            SceneManager.LoadScene("FailScene");
    }
}
