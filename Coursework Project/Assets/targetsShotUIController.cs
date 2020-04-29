using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class targetsShotUIController : MonoBehaviour
{

    public Text targetCounter;
    GameObject[] targets;
    public float timeStart = 0;
    public static int destroyedCount = 0;
    public static int totalTargets;
    List<GameObject> targetsList;
    // Start is called before the first frame update
    void Start()
    {
        targetCounter.text = timeStart.ToString();
        targets = GameObject.FindGameObjectsWithTag("target");
        targetsList = new List<GameObject>(targets);
        totalTargets = targets.Length;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var target in targetsList)
        {
            if (!target.activeInHierarchy)
            {
                destroyedCount++;
                targetsList.Remove(target);
            }
        }
        //textBox.text = Mathf.Round(timeStart).ToString();
        targetCounter.text = ("Targets Shot: " + destroyedCount + " / " + totalTargets);
    }
}
