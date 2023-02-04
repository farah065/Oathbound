using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobbingskull : MonoBehaviour
{
    Rigidbody2D rb;
    bool bobbing = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bobbing)
        {
            bobbing = true;
            StartCoroutine(bob());
        }
    }
    IEnumerator bob()
    {
        rb.velocity = new Vector2(0, 0.3f);
        yield return new WaitForSeconds(1);
        rb.velocity = new Vector2(0, -0.3f);
        yield return new WaitForSeconds(1);
        bobbing = false;
    }
}
