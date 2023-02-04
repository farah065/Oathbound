using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D enemyBody;
    private Vector3 originalLocation;
    private Vector3 target;
    private NavMeshAgent agent;
    public float visionDistance = 1; 
    void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        originalLocation = transform.position;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis =false;

        target = transform.position;
    }

    void Update()
    {
        setTargetPosition();
        setAgentPosition();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "explosion")
            Destroy(gameObject);
    }

    void setTargetPosition(){
        /*
        RaycastHit2D rayLeft = Physics2D.Raycast(new Vector2(transform.position.x - 0.7f, transform.position.y), Vector2.left, visionDistance);
        RaycastHit2D rayRight = Physics2D.Raycast(new Vector2(transform.position.x + 0.7f, transform.position.y), Vector2.right, visionDistance);
        if(rayLeft.collider != null){
            Debug.Log(rayLeft.collider.gameObject.tag);
            if(rayLeft.collider.gameObject.tag == "Player")
                target = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        else if (rayRight.collider != null)
        {
            Debug.Log(rayRight.collider.gameObject.tag);
            if (rayRight.collider.gameObject.tag == "Player")
                target = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        else
            target = transform.position;
        */
        float number_of_rays = 180;
        float angle = 360 / number_of_rays;
        float cast_angle = 0;

        for (int i = 0; i < number_of_rays; i++)
        {
            var dir = Quaternion.Euler(0, 0, cast_angle) * transform.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, visionDistance);
            if (hit && hit.collider.gameObject.tag == "Player")
            {
                target = GameObject.FindGameObjectWithTag("Player").transform.position;
            }
            cast_angle += angle;
        }
    }
    void setAgentPosition(){
        agent.SetDestination(target);
    }
    
}
