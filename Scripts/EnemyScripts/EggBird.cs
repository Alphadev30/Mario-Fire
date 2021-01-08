using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBird : MonoBehaviour
{
    void Awake()
    {
        SpriteRenderer sprRend;
        sprRend = gameObject.AddComponent<SpriteRenderer>();
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<DamgeToPlayer>().dealDamage(); 
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
}
