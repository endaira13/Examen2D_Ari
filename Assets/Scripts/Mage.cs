using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    private Rigidbody2D rBody;
    private float horizontal;
    public float speed = 3;
    private Animator anim;
    public Transform groundSensor;
    public float sensorRadius;
    public LayerMask sensorLayer;
    public bool isGrounded;
    public float jumpForce = 10;
    // Start is called before the first frame update
    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if(horizontal > 0)
        {
            anim.SetBool("Run",true);
        }

        else if(horizontal == 0)
        {
            anim.SetBool("Run",false);
        }

        isGrounded = Physics2D.OverlapCircle(groundSensor.position,sensorRadius,sensorLayer);
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rBody.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            anim.SetBool("Jump",true);
        }

    }

    void FixedUpdate() 
    {
        rBody.velocity = new Vector2(horizontal * speed,rBody.velocity.y);
    }

   void OnCollisionEnter2D(Collision2D coll) 
   {
      if(coll.gameObject.layer == 3)
      {
        anim.SetBool("Jump",false);
      }
   }
   
}
