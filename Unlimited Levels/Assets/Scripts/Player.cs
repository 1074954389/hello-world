using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public  Vector2 targetPos = new Vector2(1, 1);
   // private Rigidbody2D rigidbody1;

    public float smoothing = 1f;

    public float restTime = 1f;
    public float restTimer = 0;
    private Animator anim;
    private Collider2D collider2D;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        collider2D = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

        restTimer += Time.deltaTime;
        if (restTimer < restTime) return;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h !=0)
        {
            v = 0;
        }
        if (v != 0)
            h = 0;

       transform.position =Vector2.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
        if (h != 0 || v != 0)
        {
            GameManger._instance.ReduceEnergy(1);
            collider2D.enabled = false ;
            RaycastHit2D hit = Physics2D.Linecast(transform.position, targetPos +new Vector2(h, v));
            collider2D.enabled = true;
            if (hit.transform == null)
            { targetPos += new Vector2(h, v);

            }

            else
            {
                switch (hit.collider.tag)
                {
                    case "OutWall":

                        break;
                    case "Wall":
                        anim.SetTrigger("Attack");

                        hit.collider.SendMessage("TakeDamge");
                        break;
                    case "Energy":
                        GameManger._instance.AddEnergy(20);
                        targetPos += new Vector2(h, v);
                        Destroy(hit.collider.gameObject,0.7f);
                        break;
                    case "Energy1":
                        GameManger._instance.AddEnergy(10);
                        targetPos += new Vector2(h, v);
                        Destroy(hit .collider .gameObject ,0.7f);
                        break;
                    case "Monster":
                       // TakeDamge();
                        break;
                    case "ExitDoor":
                        GameManger._instance.level += 1;
                        transform.position = new Vector2(1, 1);
                        targetPos  = new Vector2(1, 1);

                       // DestoryWithTag();
                        MapManger ._instance .CreatMap ();
                        
                        break;
                }
            }
            GameManger._instance.OnPlayerMove();
            restTimer = 0;

        }
        

    }
    //void DestoryWithTag()
    //{
    //    GameObject[] wallLives;
    //    wallLives = GameObject.FindGameObjectsWithTag("Wall");
    //    foreach (GameObject wall in wallLives)
    //    {
    //        wall.GetComponent<Enemy>().enabled = false;
    //        Destroy(wall);
    //        Destroy(wall.transform);
    //    }
    //    GameObject[]monsterLives;
    //    monsterLives = GameObject.FindGameObjectsWithTag("Monster");
    //    foreach (GameObject monster in monsterLives)
    //    {
    //        monster .GetComponent<Enemy>().enabled = false;
    //        Destroy(monster);
    //        Destroy(monster .transform);
    //    }
  
   // }
    //void TakeDamge()
    //{
       
    //    anim.SetTrigger("Danger");
    //}
}
