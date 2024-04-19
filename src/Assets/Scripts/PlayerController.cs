using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 50f;
    public bool jump = false;
    public Animator animator;
    public CharacterController2D controller;
    public Vector2 lastpos;
    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("jump", true);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("jump", false);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("speed", Mathf.Abs(moveHorizontal));
        controller.Move(moveHorizontal * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "moving")
        {
            transform.SetParent(collision.gameObject.transform);
        }
        if (collision.gameObject.tag == "transport")
        {
            transform.position = lastpos;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "moving")
        {
            transform.SetParent(null);
        }
    }

    

}
