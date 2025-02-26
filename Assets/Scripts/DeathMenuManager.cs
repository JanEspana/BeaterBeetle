using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public  MenuManager player;
    public TMP_Text roundcount;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<Canvas>().enabled = false; 
    }

    public void restartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void activateCanvas()
    {
        foreach (GameObject go in gameObjects)
        {
            go.GetComponent<Canvas>().enabled = false;
        }

        gameObject.GetComponent<Canvas>().enabled=true;

        roundcount.text = player.Round.ToString();
        
    }
}
