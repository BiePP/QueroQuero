using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGTeleporter : MonoBehaviour
{
    [Tooltip("The original amount in Y which every BG will be teleported \"ahead\" when it's time.")]
    public float sentPosition;

    [Tooltip("TRUE if the teleporter is active and should send BG's to form the next part of the path.")]
    public bool active;
    
    public int stageMoment;
    private int currentStgMom;
    private Transform BGBefore;

    public Sprite[] BG;

    public Transform finishLine;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.CompareTag("BG") && active)
        {
            collision.transform.position = new Vector2(collision.transform.position.x + sentPosition, collision.transform.position.y);
            switch (stageMoment)
            {
                case 1:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(0, 2)];
                    currentStgMom = stageMoment;
                    break;
                case 2:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(1, 4)];
                    currentStgMom = stageMoment;
                    break;
                case 3:
                    if(stageMoment != currentStgMom)
                    {
                        collision.GetComponent<SpriteRenderer>().sprite = BG[4];
                    }
                    else
                    {
                        collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(5, 7)];
                    }
                    currentStgMom = stageMoment;
                    break;
                case 4:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(6, 8)];
                    BGBefore = collision.transform;
                    currentStgMom = stageMoment;
                    break;
                case 5:
                    BGBefore.GetComponent<SpriteRenderer>().sprite = BG[7];
                    collision.GetComponent<SpriteRenderer>().sprite = BG[8];
                    currentStgMom = stageMoment;
                    Instantiate(
                        finishLine,
                        new Vector3(collision.transform.position.x, collision.transform.position.y, 0),
                        Quaternion.identity,
                        collision.transform
                    );
                    active = false;
                    break;
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }

    void ChangeStageMoment(int stageMoment)
    {
        stageMoment = this.stageMoment;
    }
}
