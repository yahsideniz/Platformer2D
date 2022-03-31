using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumScript : MonoBehaviour
{
    public int speed;
    bool FaceRight = false;

    public int turnDelay;

    private void Start()
    {
        StartCoroutine(SwiitchDirections());
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    IEnumerator SwiitchDirections()
    {
        yield return new WaitForSeconds(turnDelay);
        Switch();   
    }

    private void Switch()
    {
        FaceRight = !FaceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        speed *= -1;


        StartCoroutine(SwiitchDirections());

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Hit();
        }
    }
}
