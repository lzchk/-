using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class itogi : MonoBehaviour
{
    [SerializeField]
    GameObject itogI;
    void Start()
    {
        itogI.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}