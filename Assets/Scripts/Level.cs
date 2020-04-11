using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    [SerializeField] float delaySeconds = 2f;
    
   public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options Menu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            ActivatePauseMenu();
        }
        else
        {
            DeactivatePauseMenu();
        }
    }

    void ActivatePauseMenu()
    {
        var MyCanvas = FindObjectOfType<Canvas>();
        for (int i = 0; i < MyCanvas.transform.childCount - 1; i++)
        {
            if(MyCanvas.transform.GetChild(i).transform.name == "Pause Panel")
            {
                Time.timeScale = 0;
                MyCanvas.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    void DeactivatePauseMenu()
    {
        var MyCanvas = FindObjectOfType<Canvas>();
        for (int i = 0; i < MyCanvas.transform.childCount - 1; i++)
        {
            if (MyCanvas.transform.GetChild(i).transform.name == "Pause Panel")
            {
                Time.timeScale = 1;
                MyCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

}
