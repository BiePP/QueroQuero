using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlappyQueroStageController : MonoBehaviour
{
    private GameController GC;

    public float stageTimer;
    public float spawnerTimer;
    public float spawnerNestTimer;

    //1- coxilhas e cerros
    //2- cerros
    //3- auto estrada
    //4- proximidades de POA
    //5- Salgado Filho
    public int stageMoment;

    public enum StageMode
    {
        StoryMode,
        EnduranceMode,
    }
    public StageMode stageMode;

    [Tooltip("Added for knowledge of the \"alive\" status of the Flappy Quero-Quero.")]
    public Transform QueroFlappy;
    FlappyController flappyController;

    [Tooltip("Added for showing on screen changes made into the score.")]
    public Text totalScoreText;
    public int totalScore;
    private Text finalTotalScore;

    [Tooltip("Added for allowing control of BG changes through stage moments by the Teleporter.")]
    public Transform BGTeleporter;
    BGTeleporter BGTeleporterScript;
    public int spawnPositionX;

    [Tooltip("Added for allowing control the stopping moment of the BG when Quero-quero faints.")]
    public Transform BGRunner;
    BGRunner BGRunnerScript;

    [Tooltip("Added for allowing the plane to descent when it's time.")]
    public Transform plane;
    Animator planeAnimator;

    public Camera mainCamera;
    AudioSource audioCamera;
    private Transform gameOverScreen;
    private CanvasGroup GOScreenCanvas;

    public AudioClip winningTheme;

    public Transform[] enemies;
    private int enemyCounter;

    // Start is called before the first frame update
    void Start()
    {
        //choose the game mode
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        if(GC.gameMode == GameController.GameMode.StoryMode)
        {
            stageMode = StageMode.StoryMode;
        }
        else
        {
            stageMode = StageMode.EnduranceMode;
        }

        //prepares the Stage
        stageMoment = 1;
        BGTeleporterScript = BGTeleporter.GetComponent<BGTeleporter>();
        BGRunnerScript = BGRunner.GetComponent<BGRunner>();

        flappyController = QueroFlappy.GetComponent<FlappyController>();

        planeAnimator = plane.GetComponent<Animator>();

        audioCamera = mainCamera.GetComponent<AudioSource>();
        audioCamera.Play();
        audioCamera.loop = true;

        totalScore = 0;
        enemyCounter = 0;

        //MainCamera -> Canvas -> GameOverScreen
        gameOverScreen = flappyController.mainCamera.transform.GetChild(0).GetChild(0);
        GOScreenCanvas = gameOverScreen.GetComponent<CanvasGroup>();
        
        finalTotalScore = GOScreenCanvas.transform.GetChild(0).GetComponent<Text>();

        if (stageMode == StageMode.StoryMode)
        {
            totalScoreText.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
            totalScoreText.text = "Objetivo: Persiga o pássaro gigante azul.";

            finalTotalScore.text = "Não Desista!";
            finalTotalScore.transform.GetChild(0).GetComponent<Text>().text = "O quero-quero caiu!";

            Destroy(GOScreenCanvas.transform.GetChild(1).gameObject);
        }
        else
        {
            Color planeColor = plane.GetComponent<SpriteRenderer>().color;
            plane.GetComponent<SpriteRenderer>().color = new Color(planeColor.r, planeColor.g, planeColor.b, 0);
            totalScoreText.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stageTimer += Time.deltaTime;
        spawnerTimer += Time.deltaTime;
        spawnerNestTimer += Time.deltaTime;

        if(stageTimer > 90 && stageMode == StageMode.StoryMode)
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

        /*-- TOTAL SCORE AND QUEST --*/
        if(stageMode == StageMode.EnduranceMode)
        { 
            totalScoreText.text = "Total: " + totalScore;
        }
        

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
            StartCoroutine(StopStageMusic());
        }
    }

    private void InstantiateNewEnemy()
    {
        enemyCounter++;
        int enemy = Random.Range(0, stageMoment);
        if(enemy == 3)
        {
            enemy = Random.Range(0, 3);
        }
        int randomY = Random.Range(-7, 7);
        Transform bird = Instantiate(
            enemies[enemy],
            new Vector3(spawnPositionX, randomY, 0),
            Quaternion.identity,
            this.transform.GetChild(0)
        );
        bird.name = bird.name + " - Enemy " + enemyCounter;
            
        /*-- Se for instanciado um rabo-de-palha... --*/
        if(enemy == 2)
        {
            int newY;
            if (randomY > 3f)
                newY = -7;
            else
                newY = 7;

            enemyCounter++;

            bird = Instantiate(
                enemies[enemy],
                new Vector3(spawnPositionX, newY, 0),
                Quaternion.identity,
                this.transform.GetChild(0)
            );

            bird.name = bird.name + " - Enemy " + enemyCounter;
        }
    }

    private void InstantiateNewNest()
    {
        enemyCounter++;
        
        Transform bird = Instantiate(
            enemies[3],
            new Vector3(spawnPositionX, -5, 0),
            Quaternion.identity,
            this.transform.GetChild(0)
        );

        bird.name = bird.name + " - Enemy " + enemyCounter;
    }

    private void StartGameOverScreen() {
        planeAnimator.Play("Escape", -1);
        //If Game Over Screen didn't shown up yet...
        if(GOScreenCanvas.alpha != 1)
        {
            BGRunnerScript.active = false;
            StartCoroutine(StarGOScreenRaycast());
        }
        
    }

    private IEnumerator StarGOScreenRaycast()
    {
        if(stageMode == StageMode.EnduranceMode)
        {
            //get Child 0 from Game Over Canvas (Text, points) and attribute totalScore to it
            finalTotalScore.text = totalScore.ToString();
        }      

        //Shows Screen Game Over
        GOScreenCanvas.alpha = 1;
        //Wait some seconds because otherwise the player may hit the buttons without notice
        //yield return WaitForUnscaledSeconds(1f);
        yield return new WaitForSeconds(1f);
        GOScreenCanvas.blocksRaycasts = true;

        
    }

    private static IEnumerator WaitForUnscaledSeconds(float time)
    {
        float ttl = 0;
        while(time > ttl)
        {
            ttl += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private IEnumerator StopStageMusic()
    {
        audioCamera.loop = false;
        yield return new WaitForSeconds(8f);
        audioCamera.Stop();
    }

    

}
