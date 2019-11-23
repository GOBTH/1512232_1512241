using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarHandler : MonoBehaviour
{
    public GameObject[] stars;
    private int coinsCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        coinsCount = GameObject.FindGameObjectsWithTag("coin").Length;
    }

    // Update is called once per frame
    public void starsAcheived()
    {
        int coinsLeft = 0;
        int coinsCollected = 0;
        coinsLeft = GameObject.FindGameObjectsWithTag("coin").Length;
        coinsCollected = coinsCount - coinsLeft;

        float percentage = float.Parse(coinsCollected.ToString()) / float.Parse(coinsCount.ToString()) * 100f;
        
        if(percentage >=33f && percentage < 66)
        {
            stars[0].SetActive(true);
        }else if(percentage >=66 && percentage < 70)
        {
            stars[0].SetActive(true);
            stars[2].SetActive(true);
        }
        else if (percentage >= 70)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }
    }
}
