using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class randomposition : MonoBehaviour
{

    void Start()
    {
        float xpnn = 0;
        float ypnn = 0;
        float zpnn = 0;
        float xpn = Random.Range(-1,1);
        float ypn = Random.Range(-1,1);
        float zpn = Random.Range(-1,1);
        if(xpn == 0)
        {
            xpnn = -1;
        }
        else{
            xpnn = 1;
        }

        if(ypn == 0)
        {
            ypnn = -1;
        }
        else{
            ypnn = 1;
        }

        if(zpn == 0)
        {
            zpnn = -1;
        }
        else{
            zpnn = 1;
        }
        transform.localPosition = new Vector3(Random.Range(0.6f,0.8f) * xpnn,Random.Range(0.6f,0.8f) * ypnn,Random.Range(0.6f,0.8f) * zpnn);
    }


}
