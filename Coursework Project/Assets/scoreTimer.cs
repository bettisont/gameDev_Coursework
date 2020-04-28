using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreTimer : MonoBehaviour
{

    public Text scoreText;
    public float elapsedTime;



    // Update is called once per frame
    void Update()
    {

        elapsedTime +=  Time.deltaTime;
        scoreText.text = elapsedTime.ToString();
    }
}
