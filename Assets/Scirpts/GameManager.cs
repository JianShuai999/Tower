using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class MonsterInfo
{
    public GameObject monster;
    public Transform bornPoint;
    public float waitTime;
    public Transform endPoint;
}

public class GameManager : MonoBehaviour
{
    public List<MonsterInfo> monsterInfolist = new List<MonsterInfo>();
    private float MonsterDeadCount;
    public LayerMask layermark;
    public GameObject tower;
    public float Money;
    public Text costtext;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateMonster());
    }

    // Update is called once per frame
    void Update()
    {
        costtext.text = Money.ToString();
        if (Input.GetMouseButtonDown(0))
        {
            Ray tempray =  Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit tempraycastHit;
            if (Physics.Raycast(tempray, out tempraycastHit, 100000, layermark))
            {
                Plate tempplateform = tempraycastHit.collider.GetComponentInParent<Plate>();
                if ((tempplateform != null)&&!tempplateform.hastower)
                {
                    float tempCost = tower.GetComponent<Tower>().cost;
                    if (Money >=tempCost)
                    {
                        GameObject temptower = GameObject.Instantiate(tower);
                        temptower.transform.parent = null;
                        temptower.transform.position = tempplateform.towerPoint.position;
                        temptower.transform.rotation = tempplateform.towerPoint.rotation;
                        tempplateform.hastower = true;

                        Money -= tempCost;
                    }
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
            Monster tempMostergo = tempMonster.GetComponent<Monster>();
            tempMonster.GetComponent<Monster>().onDeadAction += OnMonsterDead;
            tempMostergo.endPoint = item.endPoint;

            yield return new WaitForSeconds(item.waitTime);
        }

    }

    public void OnMonsterDead(Monster pmonster)
    {
        Money += pmonster.addcost;
        MonsterDeadCount++;
        if (MonsterDeadCount == monsterInfolist.Count)
        {
            SceneManager.LoadScene("SucceedScene");
        }
    }
}
