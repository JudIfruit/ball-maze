using UnityEngine;
using UnityEngine.SceneManagement; 

public class BallWin : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Win"))
        {
            Debug.Log("WIN");
            RestartGame();
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
