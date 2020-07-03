using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class MonsterInfo
{
    public GameObject monster;
    public Transform bornPoint;
    public float waitTime;
}

public class GameManager : MonoBehaviour
{
    public List<MonsterInfo> monsterInfolist = new List<MonsterInfo>();
    private float MonsterDeadCount;
    public LayerMask layermark;
    public GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateMonster());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray tempray =  Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit tempraycastHit;
            if (Physics.Raycast(tempray, out tempraycastHit, 100000, layermark))
            {
                Plate tempplateform = tempraycastHit.collider.GetComponentInParent<Plate>();
                if ((tempplateform != null)&&!tempplateform.hastower)
                {
                    GameObject temptower = GameObject.Instantiate(tower);
                    temptower.transform.parent = null;
                    temptower.transform.position = tempplateform.towerPoint.position;
                    temptower.transform.rotation = tempplateform.towerPoint.rotation;
                    tempplateform.hastower = true;
                }
            }
        }
    }

    public IEnumerator CreateMonster()
    {
        foreach (var item in monsterInfolist)
        {
            GameObject tempMonster = GameObject.Instantiate(item.monster);
            tempMonster.transform.parent = null;
            tempMonster.transform.position = item.bornPoint.transform.position;
            tempMonster.transform.rotation = item.bornPoint.transform.rotation;
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
