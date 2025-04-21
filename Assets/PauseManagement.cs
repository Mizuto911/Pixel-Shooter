using UnityEngine;

public class PauseManagement : MonoBehaviour
{
    bool isPaused = false;
    GameObject pausePanel;

    private void Start()
    {
        pausePanel = GameObject.Find("Canvas").transform.Find("PauseUI").transform.Find("PausePanel").gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        if(isPaused)
        {
            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
