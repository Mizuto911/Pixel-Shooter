using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    Animator animator;
    float animationDuration = 1f;

    private void Start()
    {
        animator = GameObject.Find("Canvas").transform.Find("Transition").GetComponent<Animator>();
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene("SampleScene"));
    }

    public void MainMenu()
    {
        StartCoroutine(LoadScene("MainMenu"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadScene(string SceneName)
    {
        Time.timeScale = 1f;
        animator.SetTrigger("Next");
        yield return new WaitForSeconds(animationDuration);
        SceneManager.LoadScene(SceneName);
    }
}
