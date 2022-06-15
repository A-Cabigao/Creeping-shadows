using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if(sceneIndex > 4)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
                SceneManager.LoadScene(sceneIndex);
        }
    }
}
