using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoaoDeBarroFlappyController : MonoBehaviour
{
    public float velocityX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move toward quero-quero
        transform.position = new Vector2(transform.position.x - velocityX, transform.position.y);
    }
}
