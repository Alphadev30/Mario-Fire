using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D mybody;
    private Animator anim;

    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPostion;
    private Vector3 movePostion;

    public GameObject birdEgg;
    public LayerMask playerLayer;
    private bool attacked; 

    private bool canMove;
    private float speed = 2f;

    void Awake() 
    {
        mybody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        originPostion = transform.position;
        originPostion.x += 6f;

        movePostion = transform.position;
        movePostion.x -= 6f;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTheBird();
        dropTheEgg();
    }

    void MoveTheBird()
    {
        if(canMove)
        {
            transform.Translate(moveDirection * speed *Time.smoothDeltaTime);

            if(transform.position.x >= originPostion.x)
            {
                moveDirection = Vector3.left;
                ChangeDirection(0.5f);
            }
            else if(transform.position.x <= movePostion.x)
            {
                moveDirection = Vector3.right;
                ChangeDirection(-0.5f);
            }
        }
    }

    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void dropTheEgg()
    {
        if(!attacked)
        {
            if(Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1,
                transform.position.z), Quaternion.identity);
                attacked = true;
                anim.SetBool("Birdfly", true);
            }
        }
    }

    IEnumerator Dead(float Timer)
    {
        yield return new WaitForSeconds(Timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Bullet")
        {
            anim.SetTrigger("Dead");

            GetComponent<BoxCollider2D>().isTrigger = true;
            mybody.bodyType = RigidbodyType2D.Dynamic;

            canMove = false;

            StartCoroutine(Dead(2f));

        }
    }
}
