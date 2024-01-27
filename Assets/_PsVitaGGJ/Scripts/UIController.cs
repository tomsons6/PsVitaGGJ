using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public GameObject MenuCanvas;
    public GameObject GameCanvas;
    public GameObject WinLoseCanvas;
    public GameObject WinLoseCanvas_Win;
    public GameObject WinLoseCanvas_Lose;

    // Use this for initialization
    void Start () {
        ApplicationStart();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplicationStart()
    {
        SwitchTo_MainMenu();
    }

    public void SwitchTo_MainMenu()
    {
        MenuCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        WinLoseCanvas.SetActive(false);
    }

    public void SwitchTo_Game()
    {
        MenuCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        WinLoseCanvas.SetActive(false);
    }

    public void SwitchTo_WinLose(bool gameSuccess)
    {
        MenuCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        WinLoseCanvas.SetActive(true);

        WinLoseCanvas_Win.SetActive(gameSuccess);
        WinLoseCanvas_Lose.SetActive(!gameSuccess);
    }

    public void ReloadGame()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(2f);
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        //Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}
