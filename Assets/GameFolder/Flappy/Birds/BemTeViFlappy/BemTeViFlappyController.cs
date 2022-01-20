using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BemTeViFlappyController : MonoBehaviour
{
    public float velocityX;
    public float velocityY;
    // Start is called before the first frame update

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

    void Start()
    {
        if(this.transform.position.y > 3.5f)
        {
            velocityY = velocityY * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move toward quero-quero
        transform.position = new Vector2(transform.position.x - velocityX, transform.position.y + velocityY);

    }
}
