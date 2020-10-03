using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private int state = 0;
    /*
    0= down
    1= left
    2= right
    3= up
    */
    private float speed = 3f;
    private int mapsizeX = 30;
    private int mapsizeY = 20;
    private float maxVelocity = 10f;

    // Update is called once per frame
    void Update()
    {
        print(state);
        if( state == 0 || state == 3)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal")*speed, GetComponent<Rigidbody2D>().velocity.y); 
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Input.GetAxisRaw("Vertical")*speed);
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
        if(col.gameObject.name == "Left" && state == 0)
        {
            print("LEFT");
            state += 1;
            Physics2D.gravity = new Vector3(-9.81f,0,0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); 
        }
        else if(col.gameObject.name == "Right" && state == 1)
        {
            print("RIGHT");
            state += 1;
            Physics2D.gravity = new Vector3(9.81f,0,0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); 
        }
        else if(col.gameObject.name == "Up" && state == 2)
        {
            print("UP");
            state += 1;
            Physics2D.gravity = new Vector3(0,9.81f,0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); 
        }
        else if(col.gameObject.name == "Down" && state == 3)
        {
            print("DOWN");
            state = 0;
            Physics2D.gravity = new Vector3(0,-9.81f,0);
            GetComponent<Rigidbody>().velocity = new Vector2(0, 0); //Funkar inte
        }
        else if(col.gameObject.name == "Exit" && state == 3)
        {
            //Win
            gameObject.GetComponent<Renderer2D>().enabled = false;
        }
    }
}
