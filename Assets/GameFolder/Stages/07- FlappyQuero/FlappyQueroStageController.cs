using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyQueroStageController : MonoBehaviour
{
    public float stageTimer;
    public float spawnerTimer;

    //1- coxilhas
    //2- cerros
    //3- proximidade de POA
    //4- stage win (salgado filho)
    public int stageMoment;

    public Transform[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        stageMoment = 1;
    }

    // Update is called once per frame
    void Update()
    {
        stageTimer += Time.deltaTime;
        spawnerTimer += Time.deltaTime;

        if(stageTimer > 60)
        {
            stageMoment = 4;
        }
        else if(stageTimer > 40){
            stageMoment = 3;
        }
        else if(stageTimer > 20)
        {
            stageMoment = 2;
        }

        /*--SPAWNS --*/
        switch (stageMoment)
        {
            case 1:
                if(spawnerTimer >= 3)
                {
                    spawnerTimer = 0;
                    InstantiateNewEnemy();
                }
                break;
            case 2:
                if (spawnerTimer >= 2)
                {
                    spawnerTimer = 0;
                    InstantiateNewEnemy();
                }
                break;
            case 3:
                if (spawnerTimer >= 1.5f)
                {
                    spawnerTimer = 0;
                    InstantiateNewEnemy();
                }
                break;
            case 4:
                if (spawnerTimer >= 1f)
                {
                    spawnerTimer = 0;
                    InstantiateNewEnemy();
                }
                break;
        }
    }

    private void InstantiateNewEnemy()
    {
        int enemy = Random.Range(0, stageMoment);
        int randomY = Random.Range(-7, 7);
        Instantiate(
            enemies[0],
            new Vector3(40, randomY, 0),
            Quaternion.identity,
            this.transform.GetChild(0)
        );
        
    }
}
