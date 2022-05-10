using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //to deal with text into Canvas

public class MainMenuController : MonoBehaviour
{
    public Camera mainCamera;

    private Transform startMenuScreen;
    private CanvasGroup startMenuCanvasGroup;

    private Transform MMScreen;
    private CanvasGroup MMCanvasGroup;

    private Transform highScoreScreen;
    private CanvasGroup highScoreCanvasGroup;

    private bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        //MainCamera -> Canvas -> StartMenuScreen
        startMenuScreen = mainCamera.transform.GetChild(0).GetChild(0);
        startMenuCanvasGroup = startMenuScreen.GetComponent<CanvasGroup>();
        //MainCamera -> Canvas -> MMScreen
        MMScreen = mainCamera.transform.GetChild(0).GetChild(1);
        MMCanvasGroup = MMScreen.GetComponent<CanvasGroup>();
        //MainCamera -> Canvas -> HighScoreScreen
        highScoreScreen = mainCamera.transform.GetChild(0).GetChild(2);
        highScoreCanvasGroup = highScoreScreen.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStart && Input.anyKeyDown)
        {
            gameStart = true;
            ShowMainMenuOptions();
        }
    }

    private void HideAnyCanvas()
    {
        startMenuCanvasGroup.alpha = 0;
        startMenuCanvasGroup.blocksRaycasts = false;

        MMCanvasGroup.alpha = 0;
        MMCanvasGroup.blocksRaycasts = false;

        highScoreCanvasGroup.alpha = 0;
        highScoreCanvasGroup.blocksRaycasts = false;
    }

    public void ShowMainMenuOptions()
    {
        HideAnyCanvas();
        //Shows Screen Game Over
        MMCanvasGroup.alpha = 1;
        MMCanvasGroup.blocksRaycasts = true;
    }

    public void ShowHighScoreMenu()
    {
        HideAnyCanvas();
        highScoreCanvasGroup.alpha = 1;
        highScoreCanvasGroup.blocksRaycasts = true;
        GetHighScore();
    }

    public void StartDemoEndurance()
    {
        SceneManager.LoadScene("FlappyStage");
    }

    private void GetHighScore()
    {
        List<List<string>> list = FlappyQueries.Display10thBestScore();

        

        //super: 1- hi
        //2- namelist 3- scorelist 
        int i = 0;
        foreach(List<string> item in list)
        {
            highScoreScreen.transform.GetChild(2).GetChild(i).GetComponent<Text>().text = item[0];
            highScoreScreen.transform.GetChild(3).GetChild(i).GetComponent<Text>().text = item[1];
            i++;
        }
        
    }
}
