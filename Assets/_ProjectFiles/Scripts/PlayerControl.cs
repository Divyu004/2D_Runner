using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{
    [SerializeField] private LayerMask _PlatformLayerMask;

    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _doorOpen;
    [SerializeField] private int _speed;
    public int key=0;
    public int jumpVel;
    private bool _grounded = false;
    private bool _crouch = false;
    
    private BoxCollider2D _boxCollider;//jump try
    private Rigidbody2D _rb; 

    [Header("HP")]
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private void Awake()
    {
       // _boxCollider = transform.GetComponent<BoxCollider2D>();//jump try
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

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

        #region Crouch
        if(Input.GetKey(KeyCode.C))
        {
            _crouch = true;
            if (_crouch)
            {
                _animator.SetBool("canCrouch", true);
                
            }
            
        }
       /* else
        {
            _animator.SetBool("canCrouch", false);
        }*/
        else if (Input.GetKeyUp(KeyCode.C))
        {
            _crouch=false;
            _animator.SetBool("canCrouch", false);
        }


        #endregion

        #region HP
        if (this.gameObject.transform.position.y < -8)
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        #endregion
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            SceneManager.LoadScene("GameOver");
        }

        if(collision.gameObject.tag == "Key")
        {
            keyCount();
            collision.gameObject.SetActive(false);
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
        _rb.velocity = Vector2.up * jumpVel;//jump 
        _animator.SetBool("canJump", true);
    }

    private void CrouchPlayer()
    {

        _animator.SetBool("canCrouch", true);
    }
    private void TakeDamage(int damage)
    {
        currentHealth-=damage;
        healthBar.SetHeatlh(currentHealth);
    }
   
    private void keyCount()
    {
        key += key;
    }
    //jump try
    /*private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down ,.1f, _PlatformLayerMask);
        Debug.Log("ONGROUND");
        return raycastHit2D.collider != null;
    }*/
}
