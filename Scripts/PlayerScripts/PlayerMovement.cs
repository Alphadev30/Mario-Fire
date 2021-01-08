using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator _playerAnim;
    private SpriteRenderer _playerSprite;
    private bool isGrounded;
    private bool jumped;
    
    
    [SerializeField]
    public float speed = 5;

    [SerializeField]
    public float _jumpForce = 9.0f;

    public Transform groundCheckPosition;
    public LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        checkIfGrounded();
        PlayerJump();
    }

    void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxisRaw("Horizontal") ;  //  CrossPlatformInputManager.GetAxis("Horizontal")
        if (move > 0)
        {
            _playerSprite.flipX = false;
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
            _playerAnim.SetInteger("Move", Mathf.Abs((int)move));

        }
        else if(move < 0)
        {
            rigid.velocity = new Vector2(-speed, rigid.velocity.y);
            _playerSprite.flipX = true;
            _playerAnim.SetInteger("Move", Mathf.Abs((int)move));
        }
        else
        {
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
            _playerAnim.SetInteger("Move", 0);
        }
    }

    void checkIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, layer);
        if(isGrounded)
        {
            if(jumped)
            {
                jumped = false;
                _playerAnim.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if(isGrounded)
        {
            if(CrossPlatformInputManager.GetButtonDown("JumpButton")) //Input.GetKey(KeyCode.Space)) //
            {
                jumped = true;
                rigid.velocity = new Vector2(rigid.velocity.x, _jumpForce);
                _playerAnim.SetBool("Jump", true);
            }
        }
    }

    

}
