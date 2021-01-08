using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{

    public float moveSpeed = 1f;
    private Rigidbody2D myBody;
    private Animator anim;

    private bool moveLeft;

    public Transform down_collision;
    public Transform leftCollison, rightColllision, topCollision;
    public LayerMask playerLayer;

    private bool canMove ;
    private bool stunned ;

    private Vector3 leftCollisonPositon, rightColllisionPosition; 

    void Awake() 
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        leftCollisonPositon = leftCollison.position;
        rightColllisionPosition = rightColllision.position;   
    }

    void Start() 
    {
        moveLeft = true;
        canMove = true;
    }

    void Update() 
    {
        if(canMove)
        {
            if(moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }
        
        checkCollision();
    }

    void checkCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCollison.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightColllision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, 0.2f, playerLayer);
        
        if(topHit != null)
        {
            if(topHit.gameObject.tag == "Player")
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = 
                    new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 2f);

                    stunned = true;
                    canMove = false; 
                    anim.SetBool("Stun", true);  
                    myBody.velocity = new Vector2(0, 0);

                    //Beetle Code Here;
                    if(tag == "Beetle")
                    {
                        anim.SetBool("Stun", true); 
                        StartCoroutine(Dead(0.5f));
                    }
                }

                
            }
        }
        if(leftHit)
        {
            if(leftHit.collider.gameObject.tag == "Player")
            {
                if(!stunned)
                {
                    //Apply damage 
                    leftHit.collider.gameObject.GetComponent<DamgeToPlayer>().dealDamage();
                }
                else
                {
                    if(tag != "Beetle")
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                    
                }
            }
        }
       
        if(rightHit)
        {
            if(rightHit.collider.gameObject.tag == "Player")
            {
                if(!stunned)
                {
                    //Apply damage 
                    rightHit.collider.gameObject.GetComponent<DamgeToPlayer>().dealDamage();
                }
                else
                {
                    if(tag != "Beetle")
                    {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }
        
        if(!Physics2D.Raycast(down_collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;

        Vector3 tempScale = transform.localScale;

        if(moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            leftCollison.position = leftCollisonPositon;
            rightColllision.position = rightColllisionPosition;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);

            leftCollison.position = rightColllisionPosition;
            rightColllision.position = leftCollisonPositon;
        }
        transform.localScale = tempScale;
    }

    IEnumerator Dead(float Timer)
    {
        yield return new WaitForSeconds(Timer);
        gameObject.SetActive(false);
    }

    
    // To kill the emenies
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Bullet")
        {
            if(tag == "Beetle")
            {
                anim.SetBool("Stun", true); 
                canMove = false;
                myBody.velocity = new Vector2(0,0);
                StartCoroutine(Dead(0.3f));
            }

            if(tag == "Snail")
            {
                if(!stunned)
                {
                    anim.SetBool("Stun", true); 
                    stunned = true;
                    canMove = false; 
                    myBody.velocity = new Vector2(0, 0);
                }
                else
                {
                    StartCoroutine(Dead(0.1f));
                }  
            }
        }    
        
    }








    // public float moveSpeed = 1f;
    // public Transform leftCollison, rightColllision, topCollision;
    // public LayerMask playerLayer;

    // private Vector3 leftCollisonPositon, rightColllisionPosition; 
    // private Rigidbody2D myBody;
    // private Animator anim;
    // private SpriteRenderer Sprite;

    // private bool canMove = true;
    // private bool stunned = false;

    // [SerializeField]
    // protected Transform PointA, PointB;
    // protected Vector3 currentTarget;

    // private bool moveLeft;
    // //public Transform down_collision;


    // // Start is called before the first frame update
    // void Start()
    // {
    //     anim = GetComponentInChildren<Animator>();
    //     Sprite = GetComponentInChildren<SpriteRenderer>();
    //     myBody = GetComponent<Rigidbody2D>();
    //     leftCollisonPositon = leftCollison.position;
    //     rightColllisionPosition = rightColllision.position;

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
    //     {
    //         return;
    //     }

    //     checkCollisionWithPlayer();
    //     Movement();
        
    // }

    // void Movement()
    // {
    //     if (canMove)
    //     {
    //         if(currentTarget == PointA.position)
    //         {
    //             Sprite.flipX = false;
    //             moveLeft = true;
    //         }
    //         else if(currentTarget == PointB.position)
    //         {
    //             Sprite.flipX = true;
    //             moveLeft = false;
    //         }
    //         if(transform.position == PointA.position)
    //         {
    //             currentTarget = PointB.position;
    //             anim.SetTrigger("idle");
    //         }
    //         else if(transform.position == PointB.position)
    //         {
    //             currentTarget = PointA.position;
    //             anim.SetTrigger("idle");
    //         }
    //         transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);
    //     }

    //     if(moveLeft)
    //     {
    //         leftCollison.position = leftCollisonPositon;
    //         rightColllision.position = rightColllisionPosition;
    //     }
    //     else if(!moveLeft)
    //     {
    //         leftCollison.position = rightColllisionPosition;
    //         rightColllision.position = leftCollisonPositon;
    //     }
    // }

    // void checkCollisionWithPlayer()
    // {
    //     RaycastHit2D leftHit = Physics2D.Raycast(leftCollison.position, Vector2.left, 0.1f, playerLayer);
    //     RaycastHit2D rightHit = Physics2D.Raycast(rightColllision.position, Vector2.right, 0.1f, playerLayer);

    //     Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, 0.2f, playerLayer);
    //     if(topHit != null)
    //     {
    //         if(topHit.gameObject.tag == "Player")
    //         {
    //             if (!stunned)
    //             {
    //                 topHit.gameObject.GetComponent<Rigidbody2D>().velocity = 
    //                 new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

    //                 stunned = true;
    //                 canMove = false; 
    //                 anim.SetBool("Stun", true);  
    //                 myBody.velocity = new Vector2(0, 0);

    //                 //Beetle Code Here;
    //                 if(tag == "Beetle")
    //                 {
    //                     anim.SetBool("Stun", true); 
    //                     StartCoroutine(Dead(0.5f));
    //                 }
    //             }

                
    //         }
    //     }
    //     if(leftHit)
    //     {
    //         if(leftHit.collider.gameObject.tag == "Player")
    //         {
    //             if(!stunned)
    //             {
    //             //Apply damage 
    //             }
    //             else
    //             {
    //                 if(tag != "Beetle")
    //                 {
    //                     myBody.velocity = new Vector2(15f, myBody.velocity.y);
    //                     StartCoroutine(Dead(3f));
    //                 }
                    
    //             }
    //         }
    //     }
       
    //     if(rightHit)
    //     {
    //         if(rightHit.collider.gameObject.tag == "Player")
    //         {
    //             if(!stunned)
    //             {
    //             //Apply damage 
    //             }
    //             else
    //             {
    //                 if(tag != "Beetle")
    //                 {
    //                     myBody.velocity = new Vector2(-15f, myBody.velocity.y);
    //                     StartCoroutine(Dead(3f));
    //                 }
    //             }
    //         }
    //     }
    // }

    // IEnumerator Dead(float Timer)
    // {
    //     yield return new WaitForSeconds(Timer);
    //     gameObject.SetActive(false);
    // }

    // void OnTriggerEnter2D(Collider2D other) 
    // {
    //     if (other.tag == "Bullet")
    //     {
    //         if(tag == "Beetle")
    //         {
    //             anim.SetBool("Stun", true); 
    //             canMove = false;
    //             myBody.velocity = new Vector2(0,0);
    //             StartCoroutine(Dead(0.3f));
    //         }

    //         if(tag == "Snail")
    //         {
    //             if(!stunned)
    //             {
    //                 anim.SetBool("Stun", true); 
    //                 stunned = true;
    //                 canMove = false; 
    //                 myBody.velocity = new Vector2(0, 0);
    //             }
    //             else
    //             {
    //                 StartCoroutine(Dead(0.1f));
    //             }  
    //         }
    //     }    
        
    // }
}