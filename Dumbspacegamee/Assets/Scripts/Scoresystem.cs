using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoresystem : MonoBehaviour
{
    public Text scoreText;

    public static int scoreCount;
    public static int coinCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Flag: " + Mathf.Round(scoreCount);
    
    }
}
