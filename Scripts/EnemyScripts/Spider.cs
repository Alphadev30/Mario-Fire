using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D myBody;

    private Vector3 moveDirection = Vector3.down;
    private string coroutineName = "ChangeMovement()";

    void Awake() 
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeMovement());
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpider();
    }

    void MoveSpider()
    {
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f,5f));

        if(moveDirection == Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        else
        {
            moveDirection = Vector3.down;
        }

        StartCoroutine(ChangeMovement());
    }

    IEnumerator SpiderIsDead()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Bullet")
        {
            anim.SetBool("isDead", true);
            myBody.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(SpiderIsDead());
            StopCoroutine(ChangeMovement());
            
        }    
        if(other.tag == "Player")
        {
           other.GetComponent<DamgeToPlayer>().dealDamage(); 
        }
    }
}
