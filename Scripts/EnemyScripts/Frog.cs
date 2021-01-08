using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Animator anim;

    private bool animationStarted;
    private bool animationFinished;

    private int jumpTimes;
    private bool jumpLeft = true;

    public LayerMask playerLayer;
    private GameObject player;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(frogJump());
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() 
    {
        if(Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
        {
            player.GetComponent<DamgeToPlayer>().dealDamage();
        }    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(animationFinished && animationStarted)
        {
            animationStarted = false;
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator frogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        animationStarted = true;
        animationFinished = false;
        jumpTimes++;

        if(jumpLeft)
        {
            anim.Play("jumpLeft");
        }
        else
        {
            anim.Play("jumpRight");
        }

        StartCoroutine(frogJump());
    }

    void animationFrogFinished()
    {
        animationFinished = true;
        
        if(jumpLeft)
        {
            anim.Play("idle");
        }
        else
        {
            anim.Play("idleRight");
        }

        if(jumpTimes == 3)
        {
            jumpTimes = 0;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            
            jumpLeft = !jumpLeft;
        }
    }

}
