using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropenscript : MonoBehaviour
{
    public bool open = false;
    public bool close = false;
    public bool engaged = false;
    public float ascendingSpeed;
    public float speed = 0;
    private Transform elevatorTR;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        elevatorTR = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            open = false;
            GetComponent<BoxCollider2D>().enabled = true;
            anim.SetBool("open", true);
            StartCoroutine(closeopen());
        }
        if (close)
        {
            close = false;
            anim.SetBool("close", true);
            StartCoroutine(closeclose());
        }
        if (engaged)
        {
            elevatorTR.position = new Vector3(elevatorTR.position.x, elevatorTR.position.y + ascendingSpeed);
        }
    }
    IEnumerator closeopen()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("open", false);
    }
    IEnumerator closeclose()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("close", false);
    }
}
