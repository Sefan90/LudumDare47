using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
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
        if( state == 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(input, GetComponent<Rigidbody2D>().velocity.y); 
        }
        else if(state == 1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, input);
        }
        else if(state == 2)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -input);
        }
        else if(state == 3)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-input, GetComponent<Rigidbody2D>().velocity.y);
        }

        if(input != 0f)
        {
            
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
                onGround = true;
            }
            else if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Idel" && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "landing")
            {
                anim.Play("Base Layer.Idel", 0, 1);
            }
        }
        else if(Physics2D.Raycast(transform.position, -Vector2.up).collider == null)
        {
            onGround = false;
        }

        if(GetComponent<Rigidbody2D>().velocity.y<-maxVelocity)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -maxVelocity); 
        }
        else if(GetComponent<Rigidbody2D>().velocity.y>maxVelocity)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, maxVelocity); 
        }
        else if(GetComponent<Rigidbody2D>().velocity.x<-maxVelocity)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-maxVelocity, GetComponent<Rigidbody2D>().velocity.y); 
        }
        else if(GetComponent<Rigidbody2D>().velocity.x>maxVelocity)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(maxVelocity, GetComponent<Rigidbody2D>().velocity.y); 
        }

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
        else if(col.gameObject.name == "Exit" && state == 3)
        {
            //Win
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
