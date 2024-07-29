using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickUp : MonoBehaviour
{
    public float Score;
    public float Coins;

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("gigg");
        if (col.gameObject.tag == "CO")
        {
            Destroy (col.gameObject);
            Score += 1;
        }

         if (col.gameObject.tag == "Coin")
        {
            Destroy (col.gameObject);
            Coins += 1;
        }
    }
}
