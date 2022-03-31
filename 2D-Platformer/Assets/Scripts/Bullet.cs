using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

     void Start()
    {
        Invoke("DestroyBullet", 2.5f);
    }
    void Update()
    {
        gameObject.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }


     void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
