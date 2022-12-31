using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScriptFunction : MonoBehaviour
{
    public string scene_name = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
    public void StartGame()
    {
        Debug.Log("starting game");
        SceneManager.LoadScene(scene_name);
    }
}
