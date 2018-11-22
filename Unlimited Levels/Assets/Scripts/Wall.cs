using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    public int hp = 2;

    public Sprite DamgeSprite;
    public  void TakeDamge()
    {
        hp -= 1;
        GetComponent<SpriteRenderer>().sprite =DamgeSprite ;
        if (hp <= 0)
            Destroy(this.gameObject);
    }
}
