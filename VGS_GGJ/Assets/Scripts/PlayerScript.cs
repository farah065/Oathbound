using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 3;
    public float jumpSpeed = 3;
    private bool grounded = true;
    private Rigidbody2D playerbody;
    public rootgenscript rgn;
    public dooropenscript dors;
    public AudioSource footsteps;
    public AudioSource keysfx;
    public AudioClip rightstep;
    public AudioClip leftstep;
    public AudioClip keygrab;
    bool firststep = true;
    bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        playerbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveActionValue = moveAction.ReadValue<float>();
        if (Mathf.Abs(playerbody.velocity.x) > 1)
            moving = true;
        else
            moving = false;
        move(moveActionValue);
        if (jumpAction.triggered && grounded)
            jump();
        if (!footsteps.isPlaying && moving && grounded)
        {
            if (firststep)
                footsteps.PlayOneShot(rightstep);
            else
                footsteps.PlayOneShot(leftstep);
            firststep = !firststep;
        }
    }


    public InputAction moveAction;
    public InputAction jumpAction;

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }
    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    private void move(float direction){
        playerbody.velocity = new Vector2(direction * moveSpeed, playerbody.velocity.y);
    }

    private void jump(){
        playerbody.velocity = new Vector2(playerbody.velocity.x, jumpSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Floor")
            grounded = true;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Floor")
            grounded = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "explosion")
            enabled = false;
        if (other.gameObject.tag == "enemy")
            enabled = false;
        if (other.gameObject.tag == "skullkey")
        {
            rgn.enraged = true;
            dors.open = true;
            Destroy(other.gameObject);
            keysfx.PlayOneShot(keygrab);
        }
        if (other.gameObject.tag == "door")
            playerbody.velocity = new Vector2(playerbody.velocity.x, 40);

    }
}
