using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Transform player;

    private Vector2 targetPosition;

    public float smoothing = 3;

    private Animator anim;

    private BoxCollider2D collider2D;

    public int Count;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = transform.position;
        anim = GetComponent<Animator>();
        collider2D = GetComponent<BoxCollider2D>();
        GameManger._instance.enemyList.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.Lerp(transform.position, targetPosition,smoothing *Time .deltaTime );
       
	}
    public void Move()
    {
        Vector2 offset = player.position - transform.position;
        if (offset .magnitude <1.1f)
        {
            anim.SetTrigger("Attack");
            GameManger._instance.ReduceEnergy(Count);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("Danger");
        }
        else
        {
            float x = 0, y = 0;
            if (Mathf .Abs (offset .y)>Mathf .Abs (offset .x))
            {
                if (offset .y < 0)
                {
                    y = -1;
                }
                else
                {
                    y = 1;
                }
            }
            else
            {
                if (offset .x >0)
                {
                    x = 1;
                }
                else
                {
                    x =-1;
                }
            }
            collider2D.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(transform.position, targetPosition + new Vector2(x, y));
            collider2D.enabled = true;
            if (hit.transform == null)
            {
              // if ( targetPosition +new Vector2(x, y)!=GameObject .FindGameObjectWithTag ("Player").GetComponent <Player >().targetPos )
                    targetPosition +=new Vector2 (x,y);
            }
            else
            {
                if (hit.collider.tag == "Energy" || hit.collider.tag == "Energy1")
                    targetPosition += new Vector2(x, y);

            }
        }
    }
    //public void Attack()
    //{
    //    anim.SetTrigger("Attack");
    //}
}
