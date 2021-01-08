using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DamgeToPlayer : MonoBehaviour
{
    private Text lifeText;
    private Scene scene;
    private int lifeScoreCount;

    private bool canDamage;

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "X" + lifeScoreCount;
        canDamage = true;
    }

    void Start() 
    {
        Time.timeScale = 1f;
        scene = SceneManager.GetActiveScene();
    }

   

    public void dealDamage()
    {
        if(canDamage)
        {
            lifeScoreCount--;

            if(lifeScoreCount >= 0 )
            {
                lifeText.text = "X" + lifeScoreCount;
            }

            if(lifeScoreCount == 0)
            {
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
                 
            }

            StartCoroutine(WaitForDamge());
        }
    }

    IEnumerator WaitForDamge()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }
  
    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if(scene.name == "SampleScene")
        {
            SceneManager.LoadScene("SampleScene");
        }
        else 
        {
            SceneManager.LoadScene("secondScene");
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Danger")
        {
            //Apply damage 
            //other.gameObject.GetComponent<DamgeToPlayer>().dealDamage();
            dealDamage();
        }

        if(other.gameObject.tag == "flag")
        {
            SceneManager.LoadScene("secondScene");
        }

        
    }


}
