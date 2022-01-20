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
    public bool alive;

    public Text healthCountText;
    public Camera mainCamera;
    private Animator camAnimator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        camAnimator = mainCamera.GetComponent<Animator>();

        alive = true;
        healthCountText.text = life.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")) && alive)
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
            //necessário para evitar duas colisões com o mesmo pássaro
            collision.GetComponent<Rigidbody2D>().Sleep();

            TakeDamage(1);
        }
    }

    private IEnumerator QueroQueroFall()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1.5f;
        yield return new WaitForSeconds(2f);
        Faint();
    }

    private void TakeDamage(int damage)
    {
        life -= damage;
        healthCountText.text = life.ToString();
        camAnimator.Play("TakeDamage", -1);
        animator.Play("TakeDamage", -1);
    }

    public void Faint()
    {
        life = 0;
        healthCountText.text = life.ToString();
        alive = false;
        Time.timeScale = 0;
    }
}
