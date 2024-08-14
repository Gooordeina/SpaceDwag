using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coinsystem : MonoBehaviour
{
    public Text coinText;
    public static int coinCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coin: " + Mathf.Round(coinCount);
    }
}
