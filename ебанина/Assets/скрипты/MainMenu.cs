using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayCurrentLevel()
    {

    }

    public void OpenLevelList()
    {
        SceneManager.LoadScene(1);
    }
}
