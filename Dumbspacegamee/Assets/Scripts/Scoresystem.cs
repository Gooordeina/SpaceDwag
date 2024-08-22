using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoresystem : MonoBehaviour
{
    public Text scoreText;

    public int scoreCount;
    
  
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Flag: " + Mathf.Round(scoreCount);
    
    }
}
