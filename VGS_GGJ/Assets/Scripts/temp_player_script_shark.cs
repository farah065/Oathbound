using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp_player_script_shark : MonoBehaviour
{
    Rigidbody2D rb;
    bool die = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (!die)
        {
            rb.velocity = new Vector2(2 * x, 2 * y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("explosion"))
        {
            die = true;
        }
    }
}
