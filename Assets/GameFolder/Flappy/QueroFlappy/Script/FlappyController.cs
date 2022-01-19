using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlappyController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float jumpForce;

    public int life;
    bool alive;

    public Text healthCountText;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        alive = true;
        healthCountText.text = life.ToString();
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

        if(life == 0 && alive)
        {
            alive = false;
            StartCoroutine(QueroQueroFall());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            life--;
            healthCountText.text = life.ToString();
        }
    }

    private IEnumerator QueroQueroFall()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1.5f;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }
}
