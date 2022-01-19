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
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(1, 3)];
                    break;
                case 2:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(2, 4)];
                    break;
                case 3:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(3, 6)];
                    break;
                case 4:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(6, 7)];
                    break;
                case 5:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[7];
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
