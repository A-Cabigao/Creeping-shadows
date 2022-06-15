using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject loseGameUI;
    [SerializeField]
    private GameObject pauseUI;

    public static bool gameIsPaused { get; private set; }

    private bool gameIsOver = false;

    private void Start()
    {
        gameIsPaused = false;
    }

    private void OnEnable()
    {
        if(FindObjectOfType<Player>() != null)       
            FindObjectOfType<Player>().PlayerHasDied += EndGame;    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsOver)
        {
            switch(gameIsPaused)
            {
                case true:
                    UnpauseGame();
                    break;

                case false:
                    PauseGame();
                    break;
            }
        }
    }

    // Function for end of game
    // Shows the lose ui screen
    private void EndGame()
    {
        loseGameUI.SetActive(true);
        gameIsPaused = true;
        gameIsOver = true;
        FindObjectOfType<Player>().PlayerHasDied -= EndGame;
    }

    // Public function for pausing game
    public void PauseGame()
    {
        pauseUI.SetActive(true);
        gameIsPaused = true;
    }

    // Public function to unpause game
    public void UnpauseGame()
    {
        pauseUI.SetActive(false);
        gameIsPaused = false;
    }
}
