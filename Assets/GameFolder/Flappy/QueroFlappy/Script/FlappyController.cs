using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            animator.Play("Flap", -1);

            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce));
        }


    }

}
