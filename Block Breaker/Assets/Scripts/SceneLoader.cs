using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadNextScene(){
        //get current scene index in the build settings and load next scene (SceneManagement required)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadPreviousScene(){
        //get current scene index in the build settings and load next scene (SceneManagement required)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }


    public void LoadFirstScene(){
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGameSession();
    }

  

    public void ExitGame(){
        Application.Quit();
    }
}
