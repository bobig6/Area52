using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  This script allows us to make breakable blocks. The following parameters should be set:
    - sprites is the list of sprites that will indicate the breaking of the wall and the first one should be the default sprite
    - max_health is the health of the block
*/ 
public class BreakableWall : MonoBehaviour
{
    public List<Sprite> sprites;
    public float max_health = 100f;
    private float current_health;
    private float steps;
    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        steps = max_health / (sprites.Count);
    }
    
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            //when a collision is detected the health is redused and the sprite
            //changes depending on the health level
            current_health -= collision.gameObject.GetComponent<BulletScript>().damage;
            if (current_health <= 0.0f)
            {
                gameObject.SetActive(false);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = sprites[(int)(current_health / steps)];
            }
        }
    }
}
