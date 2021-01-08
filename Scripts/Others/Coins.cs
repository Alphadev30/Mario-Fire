using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    private Text coinTextScore;
    private AudioSource audioManager;
    public int scoreCount;

    void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

     
    // Start is called before the first frame update
    void Start()
    {
        coinTextScore = GameObject.Find("CoinsText").GetComponent<Text>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Coin")
        {
            other.gameObject.SetActive(false);

            audioManager.Play();
            scoreCount++;
            coinTextScore.text = "X" + scoreCount;

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
