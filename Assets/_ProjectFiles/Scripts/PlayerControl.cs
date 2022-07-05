using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    [SerializeField] private LayerMask _PlatformLayerMask;

    [SerializeField] private Animator _animator;
    [SerializeField] private int _speed;
    public int jumpVel;
    private bool _grounded = false;

    
    private BoxCollider2D _boxCollider;//jump try
    private Rigidbody2D _rb;

    private void Awake()
    {
       // _boxCollider = transform.GetComponent<BoxCollider2D>();//jump try
        _rb = transform.GetComponent<Rigidbody2D>();//jump try
    }
    private void Update()
    {
        float horiMove = Input.GetAxisRaw("Horizontal");
       // float jump = Input.GetAxisRaw("Jump");


        MovePlayer(horiMove);

        Vector3 Lscale = transform.localScale;

        #region Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_grounded)
            {
                JumpPlayer();
            }
            _grounded = false;
        }

       /* if (_grounded && jump > 0)
        {
            float jumpVel = 20f;
            _animator.SetBool("canJump", true);
            _rb.velocity = Vector2.up * jumpVel;//jump try

        }
        else
        {
            _animator.SetBool("canJump", false);
        }*/
        #endregion

        #region Run
        if (_grounded && horiMove > 0)
        {
            _animator.SetBool("isRunning", true);
            Lscale.x = Mathf.Abs(Lscale.x);
        }
        else if (_grounded && horiMove < 0)
        {
            _animator.SetBool("isRunning", true);
            Lscale.x = -1*Mathf.Abs(Lscale.x);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
        transform.localScale = Lscale;
        #endregion

        //if ellen y<-8 debug.log
        if (this.gameObject.transform.position.y < -8)
        {
            Debug.Log("GameOver");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _grounded = true;
            _animator.SetBool("canJump", false);
            Debug.Log("ON GROUND");
        }
    }
    private void MovePlayer(float horizontal)
    {
        Vector3 position = transform.position;

        position.x += horizontal * _speed * Time.deltaTime;
        transform.position = position;
    }

    private void JumpPlayer()
    {
        // float jumpVel = 20f;
        _rb.velocity = Vector2.up * jumpVel;//jump try
        _animator.SetBool("canJump", true);
    }
   
    //jump try
    /*private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down ,.1f, _PlatformLayerMask);
        Debug.Log("ONGROUND");
        return raycastHit2D.collider != null;
    }*/
}
