
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void QuitButtonInPause()
    {
        SceneManager.LoadSceneAsync(MenuScene);
    }
    public void ResumButton()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FirstPersonCharacter.gameIsPaused = false;
    }

}