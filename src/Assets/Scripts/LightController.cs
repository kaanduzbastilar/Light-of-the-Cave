using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public GameObject lightElement;
    public float radius = 5;
    public float scalespeed = 5f;

    private GameController gm;
    //public List<string> stones = new List<string>();

    public List<AudioClip> audioClips = new List<AudioClip>();

    private string currentColor = "yellow";
    private float fixedDeltaTime;
    private Rigidbody2D rb;

    Vector2 newscale = new Vector2(0.4f, 0.4f);
    Vector2 originalscale = new Vector2(1f, 1f);

    private List<Animator> animators = new List<Animator>();

    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GM").GetComponent<GameController>();
    }

    private void Start()
    {
        lightElement.GetComponent<Light2D>().color = Color.yellow;
    }

    public void resetPowers()
    {
        // Reset Animation Speeds
        foreach (Animator animator in animators)
        {
            animator.speed = 1;
        }
        animators.Clear();

        //Reset Scale
        if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(-originalscale.x, originalscale.y);
        }
        else
        {
            transform.localScale = new Vector2(originalscale.x, originalscale.y);
        }

        // Reset Gravity
        rb.gravityScale = 3;
        transform.localScale = new Vector2(transform.localScale.x, 1);
    }

    public void activatePowers()
    {
        switch (currentColor)
        {
            case "yellow":
                break;
            case "red":
                transform.localScale = transform.localScale / 2.5f;
                break;
            case "green":
                break;
            case "blue":
                rb.gravityScale = -3f;
                transform.localScale = new Vector2(transform.localScale.x, -1);
                break;
        }
    }

    public void changeColor()
    {
        //switch (currentColor)
        //{
        //    case "yellow":

        //        currentColor = "red";
        //        lightElement.GetComponent<Light2D>().color = Color.red;
        //        break;
        //    case "red":
        //        currentColor = "green";
        //        lightElement.GetComponent<Light2D>().color = Color.green;
        //        break;
        //    case "green":
        //        currentColor = "blue";
        //        lightElement.GetComponent<Light2D>().color = Color.cyan;
        //        break;
        //    case "blue":
        //        currentColor = "yellow";
        //        lightElement.GetComponent<Light2D>().color = Color.yellow;
        //        break;
        //}

        if (gm.stones.Count <= 1)
        {
            return;
        }

        int stoneSize = gm.stones.Count;

        int currentIndex = gm.stones.IndexOf(currentColor);

        if (currentIndex < stoneSize - 1)
        {
            string colorToChange = gm.stones[currentIndex + 1];
            currentColor = colorToChange;

        }
        else
        {
            string colorToChange = gm.stones[0];
            currentColor = colorToChange;
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();

        switch (currentColor)
        {
            case "yellow":
                lightElement.GetComponent<Light2D>().color = Color.yellow;
                audioSource.clip = audioClips[0];
                audioSource.Play();
                break;
            case "red":
                lightElement.GetComponent<Light2D>().color = Color.red;
                audioSource.clip = audioClips[1];
                audioSource.Play();
                break;
            case "green":
                lightElement.GetComponent<Light2D>().color = Color.green;
                audioSource.clip = audioClips[2];
                audioSource.Play();
                break;
            case "blue":
                lightElement.GetComponent<Light2D>().color = Color.blue;
                audioSource.clip = audioClips[3];
                audioSource.Play();
                break;
        }

        resetPowers();
        activatePowers();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "red")
        {
            gm.stones.Add("red");

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "green")
        {

            gm.stones.Add("green");

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "blue")
        {

            gm.stones.Add("blue");

            Destroy(collision.gameObject);
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (currentColor == "green")
        {
            if (collision.tag == "Player")
            {
                return;
            }

            Debug.Log(collision.name);

            Animator anim = collision.gameObject.GetComponent<Animator>();
            if (anim != null)
            {
                anim.speed = 0.2f;
                animators.Add(anim);
            }
        }
        else if (currentColor == "yellow")
        {
            if (collision.tag == "repairable")
            {
                Animator anim = collision.gameObject.GetComponent<Animator>();
                if (anim != null)
                {
                    if (!anim.GetBool("fixed"))
                    {
                        anim.SetBool("fixed", true);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentColor == "green")
        {
            foreach (Animator animator in animators)
            {
                animator.speed = 1;
            }
            animators.Clear();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            changeColor();
        }
    }
}
