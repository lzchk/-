using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private static MusicController instane;

    private void Awake()
    {
        if (instane != null)
            Destroy(gameObject);
        else
        {
            instane = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
