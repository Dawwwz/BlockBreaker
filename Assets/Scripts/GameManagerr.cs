using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System.IO;
public class GameManagerr : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;
    public UIManager ui;

    public GameObject[] level_Prefab;
    public int current_Level;

    public int score;
    public int lives ;
    Score score_Obj = new Score();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }
    public void NewGame()
    {
        Time.timeScale = 1;
        score = 0;
        lives = 4;
        LoadLevel();
    }
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        ui = FindObjectOfType<UIManager>();
        NewGame();
    }
    private void LoadLevel()
    {
        if (level_Prefab.Length > current_Level) { Instantiate(level_Prefab[current_Level]); }
        else
        {
            current_Level = 0;
            Instantiate(level_Prefab[current_Level]);
        }
    }
    public void Miss()
    {
        lives--;
        ui.Heart_Animation(lives);
        if (lives > 0) {
            ResetLevel();
        } else {
            CreateText();
            ui.Lose_Panel_Active();
        }
    }
    private void ResetLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();
    }
    public void Score(Brick brick)
    {
        score += brick.points;
        ui.Score_Display(score);
        if (Cleared()) {
            
            CreateText();
            current_Level++;
            ui.Win_Panel_Active();
        }
    }
    private bool Cleared()
    {
        List<Brick> brickList = new List<Brick>();
        brickList = FindObjectsOfType<Brick>().ToList();

        foreach(Brick block in brickList)
        {
            if (!block.unbreakable)
            {
                return false;
            }
        }

        return true; 
    }
    void CreateText()
    {
        score_Obj.level = current_Level + 1;
        score_Obj.score = score;
        score_Obj.data = System.DateTime.Now.ToString();
        string content = JsonUtility.ToJson(score_Obj);
        //Path of the file
        string path = Application.persistentDataPath + "/Score.txt";
        //Create File if it doesn't exist
        if (!File.Exists(path))
        {
            File.WriteAllText(path,string.Empty);
        }
        //Content of the file
     
        //Add some to text to it
        File.AppendAllText(path, content);
    }
    
}
public class Score
{
    public int score;
    public int level;
    public string data;
}