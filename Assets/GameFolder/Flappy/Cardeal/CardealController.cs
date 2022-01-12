using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardealController : MonoBehaviour
{
    public float velocityX; //see background velocity in BGRunner script for an idea!
    public float maxY;
    public float minY;

    public float velocityY;
    public bool up;

    private void Awake()
    {
        switch (this.transform.root.GetComponent<FlappyQueroStageController>().stageMoment)
        {
            case 2:
                velocityY = Random.Range(velocityY, velocityY * 1.3f);
                break;
            case 3:
                velocityY = Random.Range(velocityY, velocityY * 1.7f);
                break;
            case 4:
                velocityY = Random.Range(velocityY, velocityY * 2f);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //move toward quero-quero
        transform.position = new Vector2(transform.position.x - velocityX, transform.position.y);

        if(up && transform.position.y < maxY)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + velocityY);
            return;
        }
        if(up && transform.position.y >= maxY)
        {
            up = false;
            transform.position = new Vector2(transform.position.x, transform.position.y - velocityY);
            return;
        }
        if(!up && transform.position.y > minY)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - velocityY);
            return;
        }
        if(!up && transform.position.y <= minY)
        {
            up = true;
            transform.position = new Vector2(transform.position.x, transform.position.y + velocityY);
            return;
        }
        
    }
}
