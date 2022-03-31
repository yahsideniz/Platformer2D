using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rigidbody;
    private float hor;
    public float jumpSpeed;

    public Animator anim;

    public int life, bulletCount;
    public float damagedTime;

    public Color normalColor;
    public Color damagedColor;

    public Text lifeText, bulletText;

    public ItemSlot[] inventory;

    public Transform bulletSpawnPos;
    public GameObject leftBullet, rightBullet;
    public bool isLookingRight;
   

    void Start()
    {
        normalColor = gameObject.GetComponent<SpriteRenderer>().color;
        bulletCount = 10;
        bulletText.text = bulletCount.ToString();
    }

     void Update()
    {
        // Ateþ etme
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Shoot();
        }

        // Envanterdeki eþyalarý kullanmak için

       if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //1. slottaki eþyayý kullan
            inventory[0].UseItem();
        }

       else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //2. slottaki eþyayý kullan
            inventory[1].UseItem();
        }

       else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //3. slottaki eþyayý kullan
            inventory[2].UseItem();
        }

       else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //4. slottaki eþyayý kullan
            inventory[3].UseItem();
        }

       else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //5. slottaki eþyayý kullan
            inventory[4].UseItem();
        }




        //Ziplama

        if (Input.GetKeyDown(KeyCode.Space) && gameObject.transform.Find("Foot").GetComponent<GroundCheck>().isGrounded)
        {
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
        }

        if(damagedTime>0)
        {
            damagedTime -= Time.deltaTime;

           if(gameObject.GetComponent<SpriteRenderer>().color== normalColor)
            {
                gameObject.GetComponent<SpriteRenderer>().color = damagedColor;
            }
        }
        else
        {
            if(gameObject.GetComponent<SpriteRenderer>().color == damagedColor)
            {
                gameObject.GetComponent<SpriteRenderer>().color = normalColor;
            }
        }
    }

    private void FixedUpdate()
    {

        // Sag sol kosma

        hor = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(hor));

        rigidbody.velocity = new Vector2(hor*speed,rigidbody.velocity.y);

       

        //Kafasýný cevirme 
        if(hor > 0.1f)
        {
            gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, 0);
            isLookingRight = true;
        }

        if (hor < -0.1f)
        {
            gameObject.transform.localScale = new Vector3(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, 0);
            isLookingRight = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="enemy" && damagedTime <= 0)
        {
            Hit();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" && damagedTime <= 0)
        {
            Hit();
        }
    }

    public void Hit()
    {
        life--;
        lifeText.text = life.ToString();
        damagedTime = 0.7f;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.6f, gameObject.transform.position.z);

        if (life <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void AddLife()
    {
        life++;
        lifeText.text = life.ToString();
    }

    public void AddItem(Item item)
    {
        for(int i= 0; i < inventory.Length; i++)
        {
            if(inventory[i].isEmpty)
            {
                inventory[i].SetItem(item);
                break;
            }
        }
    }

    private void Shoot()
    {

        if(bulletCount > 0)
        {
            if (isLookingRight)
            {
                Instantiate(rightBullet, bulletSpawnPos.position, Quaternion.identity); // gameobject olsuturmak için
            }
            else
            {
                Instantiate(leftBullet, bulletSpawnPos.position, Quaternion.identity);
            }
            bulletCount--;

            bulletText.text = bulletCount.ToString();
        }
       
    }


}
