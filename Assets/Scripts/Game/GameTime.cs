using UnityEngine;

public class GameTime : MonoBehaviour
{
    private GameMaster gm;
    private bool gameIsPaused;
    private float gameTime;
    private float runningTime;

    private void Awake()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void Start()
    {     
        gameTime = 0f;
        runningTime = 0f;
    }

    private void Update()
    {
        gameIsPaused = GameMaster.gameIsPaused;

        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            switch(gameIsPaused)
            {
                case true:
                    gameIsPaused = false;
                    break;
                case false:
                    gameIsPaused = true;
                    break;
            }
        }

        runningTime += Time.deltaTime;
        UpdateGameTime();
    }

    // Increases game timer by delta time if game is not paused.
    private void UpdateGameTime()
    {
        if (!gameIsPaused)
            gameTime += Time.deltaTime;
    }

    // Returns value of game time in string format.
    public string GetGameTimeString()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
        string gameTimeString = string.Format("{0:0}:{1:00}", minutes, seconds);

        return gameTimeString;
    }

    // Returns value of running time in string format.
    public string GetRunningTimeString()
    {
        int minutes = Mathf.FloorToInt(runningTime / 60F);
        int seconds = Mathf.FloorToInt(runningTime - minutes * 60);
        string runningTimeString = string.Format("{0:0}:{1:00}", minutes, seconds);

        return runningTimeString;
    }
}
