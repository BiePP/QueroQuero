using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGTeleporter : MonoBehaviour
{

    public float startingPosition;

    public Transform[] BG;

    // Start is called before the first frame update
    void Start()
    {
        //BG[1].transform.position = transform.position + Vector3.right * BG[0].GetComponent<SpriteRenderer>().bounds.size.x;
        //BG[2].transform.position = transform.position + Vector3.right * BG[1].GetComponent<SpriteRenderer>().bounds.size.x;
        //BG[3].transform.position = transform.position + Vector3.right * BG[2].GetComponent<SpriteRenderer>().bounds.size.x;
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
        }
    }
}
