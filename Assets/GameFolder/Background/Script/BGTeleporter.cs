using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGTeleporter : MonoBehaviour
{
    [Tooltip("The original position in Y which every BG will be teleported \"ahead\" when it's time.")]
    public float startingPosition;

    
    public int stageMoment;

    public Sprite[] BG;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.CompareTag("BG"))
        {
            collision.transform.position = new Vector2(startingPosition, collision.transform.position.y);
            switch (stageMoment)
            {
                case 1:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(1, 2)];
                    break;
                case 2:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(2, 3)];
                    break;
                case 3:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(4, 5)];
                    break;
                case 4:
                    collision.GetComponent<SpriteRenderer>().sprite = BG[Random.Range(5, 7)];
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
