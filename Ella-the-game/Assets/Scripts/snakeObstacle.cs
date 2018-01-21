﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeObstacle : MonoBehaviour {
    private Animator snakeAnimator;
    [SerializeField]
    private int healthEffect ;
    GameObject rayCastPoint;
    private bool affectedPlayer;
    // Use this for initialization
    void Start () {
        snakeAnimator = GetComponentInParent<Animator>();
        rayCastPoint = this.transform.Find("Bones/Body/Neck_1/Neck_2/Head/rayCastPoint").gameObject;
       
        affectedPlayer = false;
    }
	
	// Update is called once per frame
	void Update () {
       // Debug.DrawRay(rayCastPoint.transform.position, -Vector2.right * (float)0.3, Color.red);
        //Debug.DrawRay(rayCastPoint.transform.position, Vector2.up * (float)0.3, Color.red);
    }
    public void stopAttacking()
    {
        snakeAnimator.SetBool("attack", false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Player")
        {
            snakeAnimator.SetBool("attack", true);
            gameObject.layer = LayerMask.NameToLayer("ignoredObstacles");
            applyDamage();
            
           

        }
    }
    public void applyDamage()
    {
        if (!affectedPlayer)
        {
            RaycastHit2D hitFront = Physics2D.Raycast(rayCastPoint.transform.position, -Vector2.right * (float)0.3, LayerMask.NameToLayer("Player"));
            RaycastHit2D hitUp = Physics2D.Raycast(rayCastPoint.transform.position, Vector2.up * (float)0.3, LayerMask.NameToLayer("Player"));
            if (hitFront)
            {
                if (hitFront.collider.gameObject.tag == "Player")
                {
                    hitFront.collider.gameObject.GetComponent<PlayerController>().changeHealth(healthEffect);
                    
                    affectedPlayer = true;
                    Debug.Log("damaged");
                    Destroy(gameObject, (float)1);
                }

            }
            else if(hitUp)
            {
                if (hitUp.collider.gameObject.tag == "Player")
                {
                    hitUp.collider.gameObject.GetComponent<PlayerController>().changeHealth(healthEffect);

                    affectedPlayer = true;
                    Debug.Log("damaged");
                    Destroy(gameObject, (float)1);
                }

            }


        }
      
        
        
    }

}
