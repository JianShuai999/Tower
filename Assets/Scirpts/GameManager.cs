using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class MonsterInfo
{
    public GameObject monster;
    public float waitTime;
}

public class GameManager : MonoBehaviour
{
    public Transform bornPoint;
    public List<MonsterInfo> monsterInfolist = new List<MonsterInfo>();
    private float MonsterDeadCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateMonster());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CreateMonster()
    {
        foreach (var item in monsterInfolist)
        {
            GameObject tempMonster = GameObject.Instantiate(item.monster);
            tempMonster.transform.parent = null;
            tempMonster.transform.position = bornPoint.transform.position;
            tempMonster.transform.rotation = bornPoint.transform.rotation;
            tempMonster.GetComponent<Monster>().onDeadAction += OnMonsterDead;

            yield return new WaitForSeconds(item.waitTime);
        }

    }

    public void OnMonsterDead()
    {
        MonsterDeadCount++;
        if (MonsterDeadCount == monsterInfolist.Count)
        {
            SceneManager.LoadScene("SucceedScene");
        }
    }
}
