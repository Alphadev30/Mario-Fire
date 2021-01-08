using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlocks : MonoBehaviour
{
    public Transform bottomCollision;
    private Animator anim;
    private Vector3 moveDirection = Vector3.up;
    private Vector3 originalPosition;
    private Vector3 animPosition;
    private bool startAnim;
    private bool canAnimate = true;

    public LayerMask playerLayer;
    private AudioSource audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        originalPosition = transform.position;
        animPosition = transform.position;
        animPosition.y += 0.30f;
    }

    // Update is called once per frame
    void Update()
    {
        checkForCollision();
        animateUpDown();
    }

    void checkForCollision()
    {
        if(canAnimate)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottomCollision.position, Vector2.down, 0.1f, playerLayer);

            if (hit)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    //Increase Score
                    //Coins.scoreCount++; GetComponent<DamgeToPlayer>().dealDamage();
                    //GetComponent<Coins>().scoreCount += 1;
                    audioManager.Play();
                    anim.Play("blockIdle");
                    startAnim = true;
                    canAnimate = false;
                }
            }

        }
        
    }

    void animateUpDown()
    {
        if (startAnim)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime * 2);
            if (transform.position.y >= animPosition.y)
            {
                moveDirection = Vector3.down;
            }
            else if (transform.position.y <= originalPosition.y)
            {
                startAnim = false;
            }
        }
    }
}
