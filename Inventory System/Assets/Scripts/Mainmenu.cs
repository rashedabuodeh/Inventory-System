
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{

    public string GameScene, MenuScene;
    public void PlayButton()
    {
        SceneManager.UnloadSceneAsync(MenuScene);
        SceneManager.LoadScene(GameScene);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}