using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRunner : MonoBehaviour
{
    public float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        transform.position = new Vector2(transform.position.x - 0.05f, transform.position.y);
    }

}
