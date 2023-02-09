using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 3;
    public float jumpSpeed = 3;
    public float ascendingSpeed = 0.001f;
    public static bool grounded = true;
    private Rigidbody2D playerbody;
    public rootgenscript rgn;
    public dooropenscript dors;
    public AudioSource footsteps;
    public AudioSource jumpSFX;
    public AudioSource keysfx;
    public AudioClip rightstep;
    public AudioClip leftstep;
    public AudioClip jump1;
    public AudioClip jump2;
    public AudioClip jump3;
    public AudioClip[] jumpClips;
    public AudioClip jumpLand;
    public AudioClip keygrab;
    bool firststep = true;
    bool jumped = false;
    bool moving = false;

    public int hp = 3;
    public Renderer playerRen;
    public Renderer h1;
    public Renderer h2;
    public Renderer h3;
    public bool invincible;

    public Animator anim;

    public bool inElevator;

    private Transform playerTR;
    public int direction = 1;
    public bool switched;

    private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction jumpAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
        moveAction = playerInput.actions["Move"];
    }
    void Start()
    {
        playerbody = gameObject.GetComponent<Rigidbody2D>();
        playerTR = gameObject.GetComponent<Transform>();
        playerRen = gameObject.GetComponent<Renderer>();

        h1 = GameObject.FindGameObjectWithTag("hp1").GetComponent<Renderer>();
        h2 = GameObject.FindGameObjectWithTag("hp2").GetComponent<Renderer>();
        h3 = GameObject.FindGameObjectWithTag("hp3").GetComponent<Renderer>();

        jumpClips = new AudioClip[] { jump1, jump2, jump3 };

        StartCoroutine(steps());
    }


    void Update()
    {
        var moveActionValue = moveAction.ReadValue<float>();
        if (Mathf.Abs(playerbody.velocity.x) > 1)
            moving = true;
        else
            moving = false;

        if (inElevator)
        {
            playerbody.velocity = new Vector3(0, ascendingSpeed, 0);
            playerTR.position = new Vector3(playerTR.position.x, playerTR.position.y + ascendingSpeed);
            moving = false;
        }

        move(moveActionValue);

        if (grounded)
        {
            anim.SetFloat("speed", Mathf.Abs(playerbody.velocity.x));
            anim.SetBool("jumpDown", false);
            anim.SetBool("jumpUp", false);
            if (jumped)
            {
                jumped = false;
                jumpSFX.volume = 0.4f;
                jumpSFX.PlayOneShot(jumpLand);
            }
        }
        else
        {
            anim.SetFloat("speed", 0);
            if (playerbody.velocity.y > 0)
            {
                anim.SetBool("jumpUp", true);
                int r = Random.Range(0, 2);
                if (!jumped)
                {
                    jumpSFX.volume = 0.6f;
                    jumpSFX.PlayOneShot(jumpClips[r]);
                    jumped = true;
                }
            }
            else
            {
                anim.SetBool("jumpUp", false);
                anim.SetBool("jumpDown", true);
                jumped = true;
            }
        }

        if (playerbody.velocity.x > 0)
        {
            direction = 1;
        }
        else if (playerbody.velocity.x < 0)
        {
            direction = -1;
        }

        if (direction == -1 && !switched)
        {
            switched = true;
            playerTR.localScale = new Vector3(playerTR.localScale.x * -1, playerTR.localScale.y, playerTR.localScale.z);
        }
        if (direction == 1 && switched)
        {
            switched = false;
            playerTR.localScale = new Vector3(playerTR.localScale.x * -1, playerTR.localScale.y, playerTR.localScale.z);
        }
    }



    void OnEnable()
    {
        playerInput.enabled = true;
        jumpAction.performed += jump;
    }
    void OnDisable()
    {
        playerInput.enabled = false;
        jumpAction.performed -= jump;
    }

    private void move(float moveActionValue){
        playerbody.velocity = new Vector2(moveActionValue * moveSpeed, playerbody.velocity.y);
    }

    private void jump(InputAction.CallbackContext context){
        if(grounded)
            playerbody.velocity = new Vector2(playerbody.velocity.x, jumpSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "explosion")
            killPlayer();
        if (other.gameObject.tag == "enemy" && !invincible)
            StartCoroutine(hitTaken());
        if (other.gameObject.tag == "trap" && !invincible)
            StartCoroutine(hitTaken());
        if (other.gameObject.tag == "skullkey")
        {
            rgn.enraged = true;
            dors.open = true;
            Destroy(other.gameObject);
            anim.SetBool("skull", true);
        }
        if (other.gameObject.tag == "door")
        {
            transform.position = other.transform.position;
            playerbody.velocity = new Vector2(0, 0);
            inElevator = true;
            dors.close = true;
            dors.engaged = true;
            dors.ascendingSpeed = ascendingSpeed;
        }
        if(other.gameObject.tag == "Power Up (JMP)")
        {
            Destroy(other.gameObject);
            StartCoroutine(Jmpboost());
        }
    }

    private void killPlayer(){
        if (!inElevator)
        {
            enabled = false;
            h1.enabled = false;
            h2.enabled = false;
            h3.enabled = false;
            anim.SetBool("dead", true);
        }
    }
    IEnumerator Jmpboost()
    {
        float tempjmp = jumpSpeed;
        jumpSpeed *= 2;
        yield return new WaitForSeconds(5);
        jumpSpeed = tempjmp;
    }

    IEnumerator steps()
    {
        while (!moving || !grounded || !enabled)
            yield return null;
        if (firststep)
            footsteps.PlayOneShot(rightstep);
        else
            footsteps.PlayOneShot(leftstep);
        yield return new WaitForSeconds(0.53f);
        firststep = !firststep;
        StartCoroutine(steps());
    }

    IEnumerator hitTaken()
    {
        hp--;
        if (hp <= 0)
        {
            h1.enabled = false;
            killPlayer();
        }
        else
        {
            if (hp == 2)
                h3.enabled = false;
            else if (hp == 1)
                h2.enabled = false;
            invincible = true;
            for (int i = 0; i < 3; i++)
            {
                playerRen.enabled = false;
                yield return new WaitForSeconds(0.2f);
                playerRen.enabled = true;
                yield return new WaitForSeconds(0.2f);
            }
            invincible = false;
        }
    }
}
