using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    public static int Coin;
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        Coin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Coin.ToString();
    }
}
