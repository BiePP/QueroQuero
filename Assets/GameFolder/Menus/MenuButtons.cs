using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public FlappyQueroStageController sc;
    private int totalScore;

    public string nickname;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartStage()
    {
        SaveScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        SaveScore();
        Time.timeScale = 1;
        SceneManager.LoadScene("Opening");
    }

    public void GetNick(string nn)
    {
        nickname = nn;
    }

    private void SaveScore()
    {
        totalScore = sc.GetComponent<FlappyQueroStageController>().totalScore;
        this.GetComponent<FlappyQueries>().AddScore(totalScore, nickname);
    }
}
