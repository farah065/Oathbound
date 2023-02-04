using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCorruption : MonoBehaviour
{
    public static float curRate;
    public float slowRate;
    public int duration;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Power Up (SC)")
        {
            Destroy(collision.gameObject);
            StartCoroutine(activateSC());
        }
    }

    IEnumerator activateSC()
    {
        curRate = slowRate;
        yield return new WaitForSeconds(duration);
        curRate = 0;
    }
}
