using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlappyQueroStageController : MonoBehaviour
{
    public float stageTimer;
    public float spawnerTimer;
    public float spawnerNestTimer;

    //1- coxilhas e cerros
    //2- cerros
    //3- auto estrada
    //4- proximidades de POA
    //5- Salgado Filho
    public int stageMoment;

    [Tooltip("Added for knowledge of the \"alive\" status of the Flappy Quero-Quero.")]
    public Transform QueroFlappy;
    FlappyController flappyController;

    [Tooltip("Added for showing on screen changes made into the score.")]
    public Text totalScoreText;
    public int totalScore;

    [Tooltip("Added for allowing control of BG changes through stage moments by the Teleporter.")]
    public Transform BGTeleporter;
    BGTeleporter BGTeleporterScript;
    public int spawnPositionX;

    [Tooltip("Added for allowing the plane to descent when it's time.")]
    public Transform plane;
    public Animator planeAnimator;

    public Transform[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        stageMoment = 1;
        BGTeleporterScript = BGTeleporter.GetComponent<BGTeleporter>();

        flappyController = QueroFlappy.GetComponent<FlappyController>();

        planeAnimator = plane.GetComponent<Animator>();

        totalScore = 0;

    }

    // Update is called once per frame
    void Update()
    {
        stageTimer += Time.deltaTime;
        spawnerTimer += Time.deltaTime;
        spawnerNestTimer += Time.deltaTime;

        if(stageTimer > 90)
        {
            ChangeStageMoment(5);
        }
        else if(stageTimer > 60 && flappyController.alive)
        {
            totalScore += ((int)(Time.deltaTime * 20f) + 1);
            ChangeStageMoment(4);
        }
        else if(stageTimer > 40 && flappyController.alive)
        {
            totalScore += ((int)(Time.deltaTime * 12f) + 1);
            ChangeStageMoment(3);
        }
        else if(stageTimer > 20 && flappyController.alive)
        {
            totalScore += ((int)(Time.deltaTime * 6f) + 1);
            ChangeStageMoment(2);
        }
        else if (flappyController.alive)
        {
            totalScore += (int)((Time.deltaTime * 2f) + 1);
        }

        /*-- TOTAL SCORE --*/
        totalScoreText.text = "Total: " + totalScore;

        /*-- SPAWNS --*/
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
                if(spawnerNestTimer >= 4f)
                {
                    spawnerNestTimer = 0;
                    InstantiateNewNest();
                }
                break;
        }

        if (!flappyController.alive)
        {
            StartGameOverScreen();
        }
    }

    private void ChangeStageMoment(int stageMoment)
    {
        this.stageMoment = stageMoment;
        BGTeleporterScript.stageMoment = this.stageMoment;
        if(stageMoment == 5)
        {
            planeAnimator.SetBool("land", true);
        }
    }

    private void InstantiateNewEnemy()
    {
        int enemy = Random.Range(0, stageMoment);
        if(enemy == 3)
        {
            enemy = Random.Range(0, 3);
        }
        int randomY = Random.Range(-7, 7);
        Instantiate(
            enemies[enemy],
            new Vector3(spawnPositionX, randomY, 0),
            Quaternion.identity,
            this.transform.GetChild(0)
        );
            
        /*-- Se for instanciado um rabo-de-palha... --*/
        if(enemy == 2)
        {
            int newY;
            if (randomY > 3f)
                newY = -7;
            else
                newY = 7;
            Instantiate(
                enemies[enemy],
                new Vector3(spawnPositionX, newY, 0),
                Quaternion.identity,
                this.transform.GetChild(0)
            );
        }
    }

    private void InstantiateNewNest()
    {
        Instantiate(
            enemies[3],
            new Vector3(spawnPositionX, -5, 0),
            Quaternion.identity,
            this.transform.GetChild(0)
        );
    }

    private void StartGameOverScreen() {
        planeAnimator.Play("Escape", -1);

        //MainCamera -> Canvas -> GameOverScreen
        Transform gameOverScreen = flappyController.mainCamera.transform.GetChild(0).GetChild(0);
        CanvasGroup GOScreenCanvas = gameOverScreen.GetComponent<CanvasGroup>();
        
        StartCoroutine(StarGOScreenRaycast(GOScreenCanvas));
    }

    private IEnumerator StarGOScreenRaycast(CanvasGroup GOScreenCanvas)
    {
        GOScreenCanvas.alpha = 1;
        yield return new WaitForSeconds(1f);
        GOScreenCanvas.blocksRaycasts = true;
    }

}
