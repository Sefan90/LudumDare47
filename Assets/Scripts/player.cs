using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player : MonoBehaviour
{
    public GameObject text;
    private int score = 0;
    private bool onGround = true;
    private int state = 0;
    /*
    0= down
    1= left
    2= right
    3= up
    */
    private float rotation = 0f;
    private float speed = 3f;
    private int mapsizeX = 30;
    private int mapsizeY = 20;
    private float maxVelocity = 10f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal")*speed;
        float newVelocityX = GetComponent<Rigidbody2D>().velocity.x;
        float newVelocityY = GetComponent<Rigidbody2D>().velocity.y;
        if( state == 0)
        {
            newVelocityX = input;
        }
        else if(state == 1)
        {
            newVelocityY = input;
        }
        else if(state == 2)
        {
            newVelocityY = -input;
        }
        else if(state == 3)
        {
            newVelocityX = -input;
        }

        if(newVelocityY<-maxVelocity)
        {
            newVelocityY = -maxVelocity;
        }
        else if(newVelocityY>maxVelocity)
        {
            newVelocityY = maxVelocity;
        }
        
        if(newVelocityX<-maxVelocity)
        {
            newVelocityX = -maxVelocity;
        }
        else if(newVelocityX>maxVelocity)
        {
            newVelocityX = maxVelocity;
        }
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(newVelocityX,newVelocityY);

        if(input != 0f)
        {
            text.transform.localScale = new Vector3(0f,0f,0f); //Removes starttext
            if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "running" && input > 0)
            {
                anim.Play("Base Layer.running", 0, 1);
            }
            else if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "running2" && input < 0)
            {
                anim.Play("Base Layer.running2", 0, 1);
            }
        }   
        else if(Physics2D.Raycast(transform.position, -Vector2.up).collider != null)
        {
            if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "landing" && onGround == false)
            {
                //anim.Play("Base Layer.landing", 0, 1);
                //onGround = true;
            }
            if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Idel" && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "landing")
            {
                anim.Play("Base Layer.Idel", 0, 1);
            }
        }
        //else if(Physics2D.Raycast(transform.position, -Vector2.up).collider == null)
        //{
        //    onGround = false;
        //}

        if(transform.rotation.eulerAngles.z != rotation)
        {
            GetComponent<Rigidbody2D>().freezeRotation = false;
            if(transform.rotation.eulerAngles.z < rotation+1 && transform.rotation.eulerAngles.z > rotation-1)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y,rotation);
            }
            else if (transform.rotation.eulerAngles.z < rotation)
            {
                transform.Rotate(new Vector3(0,0,1f));
            }
            else if(transform.rotation.eulerAngles.z > rotation)
            {
                transform.Rotate(new Vector3(0,0,-1f));
            }
            GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        if(transform.position.x < -mapsizeX)
        {
            transform.position = new Vector3(transform.position.x+mapsizeX*2,transform.position.y,transform.position.z);
        }
        else if(transform.position.x > mapsizeX)
        {
            transform.position = new Vector3(transform.position.x-mapsizeX*2,transform.position.y,transform.position.z);
        }
        else if(transform.position.y < -mapsizeY)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y+mapsizeY*2,transform.position.z);
        }
        else if(transform.position.y > mapsizeY)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y-mapsizeY*2,transform.position.z);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Right")
        {
            print("RIGHT");
            state = 1;
            Physics2D.gravity = new Vector3(9.81f,0,0);
            rotation = 90f;
        }
        else if(col.gameObject.name == "Left")
        {
            print("LEFT");
            state = 2;
            Physics2D.gravity = new Vector3(-9.81f,0,0);
            rotation = 270f;
        }
        else if(col.gameObject.name == "Up")
        {
            print("UP");
            state = 3;
            Physics2D.gravity = new Vector3(0,9.81f,0);
            rotation = 180f;
        }
        else if(col.gameObject.name == "Down")
        {
            print("DOWN");
            state = 0;
            Physics2D.gravity = new Vector3(0,-9.81f,0);
            rotation = 0f;
        }
        else if(col.gameObject.name == "Pickup")
        {
            col.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            col.gameObject.transform.position = new Vector3(Random.Range(-1f, 1f),Random.Range(-0.75f,-1.25f),0f);
            score += 1;
        }
        else if(col.gameObject.name == "Exit" && state == 3)
        {
            text.GetComponent<TextMeshPro>().text = score+" orbs collected";
            text.transform.localScale = new Vector3(1f,1f,1f);
            text.transform.rotation = Quaternion.Euler(0,0,180f);
            text.transform.position = new Vector3(0,1.5f,0);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.name == "Exit" && state == 3)
        {
            text.transform.localScale = new Vector3(0f,0f,0f);
        }
    }
}
