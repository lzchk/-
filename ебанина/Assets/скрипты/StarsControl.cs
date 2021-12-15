using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsControl : MonoBehaviour
{
    public GameObject star1, star2, star3;
    public static int open_star1, open_star2, open_star3;
    void Start()
    {
        open_star1 = PlayerPrefs.GetInt("stars1", 1);
        open_star2 = PlayerPrefs.GetInt("stars2", 1);
        open_star3 = PlayerPrefs.GetInt("stars3", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (open_star1 == 1)
        {
            star1.SetActive(false);
        }

        if (open_star1 == 2)
        {
            star1.SetActive(true);
        }

        if (open_star2 == 1)
        {
            star2.SetActive(false);
        }

        if (open_star2 == 2)
        {
            star2.SetActive(true);
        }

        if (open_star3 == 1)
        {
            star3.SetActive(false);
        }

        if (open_star3 == 2)
        {
            star3.SetActive(true);
        }


        // ===================


        if (CoinText.Coin >= 3)
        {
            openStar1();
        }

        if (CoinText.Coin >= 4)
        {
            openStar2();
        }

        if (CoinText.Coin >= 5)
        {
            openStar3();
        }
    }


    public void openStar1()
    {
        open_star1 = 2;
        PlayerPrefs.SetInt("stars1", open_star1);
    }

    public void openStar2()
    {
        open_star2 = 2;
        PlayerPrefs.SetInt("stars2", open_star2);
    }

    public void openStar3()
    {
        open_star3 = 2;
        PlayerPrefs.SetInt("stars3", open_star3);
    }

    //public void goToOneLvl()
    //{
    //    SceneManager.LoadScene(1);
    //}
}

