using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChanageSceneManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void ONRESTARTGAME()
    {       
        SceneManager.LoadScene("GameScene");       
    }
    public void EXIT() 
    { 
        Application.Quit();
    }

}
