using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float Speed = 10f;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {   
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(DisableBullet(5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    void Move()
    {
        Vector3 temp = transform.position;
        temp.x += Speed*Time.deltaTime;
        transform.position = temp;
    }

    public float speed{
        get{
            return Speed;
        }
        set{
            Speed = value;
        }
    }

    

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Beetle" || other.gameObject.tag == "Snail" || other.gameObject.tag == "Spider")
        {
            anim.SetTrigger("Explode");
            StartCoroutine(DisableBullet(0.2f));
        }
    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}




