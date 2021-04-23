using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartMenuManager : MonoBehaviour
{

    private static StartMenuManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRulebased()
    {
        SceneManager.LoadScene("RuleBasedScene");
    }

    public void SetMLAI()
    {
        SceneManager.LoadScene("MLScene1");
    }

    public void SetWhosDady()
    {
        SceneManager.LoadScene("WhosyourDady");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
