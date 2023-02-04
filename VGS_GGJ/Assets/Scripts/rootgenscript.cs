using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootgenscript : MonoBehaviour
{
    Transform lastpoint;
    public float genDistance;
    public GameObject rootext;
    public GameObject explodefab;
    Queue<GameObject> nodes = new Queue<GameObject>();
    public bool enraged = false;
    bool exploding;
    public float exptime;
    GameObject curr;
    bool immune = false;
    int exindex = 0;
    void Awake()
    {
        GameObject baseloc = GameObject.FindGameObjectWithTag("base");
        lastpoint = baseloc.transform.GetChild(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform playerLocation = GetComponent<Transform>();
        if (!(enraged && nodes.Count == 0))
        {
            float xdiff = playerLocation.position.x - lastpoint.position.x;
            float ydiff = playerLocation.position.y - 0.7f - lastpoint.position.y;
            if (genDistance * genDistance < (xdiff * xdiff) + (ydiff * ydiff))
            {
                float midx = (playerLocation.position.x + lastpoint.position.x) / 2;
                float midy = (playerLocation.position.y - 0.7f + lastpoint.position.y) / 2;
                float angle = Mathf.Rad2Deg * Mathf.Atan2(ydiff, xdiff);
                Vector3 pos = new Vector3(midx, midy, 0);
                curr = Instantiate(rootext, pos, Quaternion.Euler(0, 0, angle));
                lastpoint = curr.transform.GetChild(0);
                nodes.Enqueue(curr);
            }

            if (enraged && !exploding)
            {
                exploding = true;
                StartCoroutine(explode());
            }
        }
        if (GetComponent<Rigidbody2D>().velocity.y < 2)
            StartCoroutine(getImmune());
    }
    IEnumerator explode()
    {
        exindex++;
        GameObject expnode = nodes.Dequeue();
        Transform locationofexp = expnode.transform;
        GameObject expobj = null;
        if (exindex % 5 == 0)  
            expobj = Instantiate(explodefab, locationofexp.position, Quaternion.Euler(0,0,0));
        Destroy(expnode);
        if (exindex % 5 == 0)
        {
            yield return new WaitForSeconds(exptime + SlowCorruption.curRate);
            exptime *= 0.95f;
            Destroy(expobj);
        }
        exploding = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("rootnode") && collision.gameObject != curr && !immune)
        {
            enraged = true;
        }
    }
    IEnumerator getImmune()
    {
        immune = true;
        yield return new WaitForSeconds(1);
        immune = false;
    }
}
