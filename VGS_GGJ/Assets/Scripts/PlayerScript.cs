using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 3;
    public float jumpSpeed = 3;
    public bool grounded = true;
    public Rigidbody2D playerbody;
    public rootgenscript rgn;
    public dooropenscript dors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var moveActionValue = moveAction.ReadValue<float>();
        move(moveActionValue);
        if (jumpAction.triggered && grounded)
            jump();

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
        if (other.gameObject.tag == "skullkey")
        {
            rgn.enraged = true;
            dors.open = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "door")
            playerbody.velocity = new Vector2(playerbody.velocity.x, 40);

    }
}
