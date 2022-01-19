using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRunner : MonoBehaviour
{
    public float timer = 0;
    public float velocity;

    public Sprite[] BGStarter;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            int randomBG = Random.Range(0, 2);
            print(child.name + " BG should be = " + randomBG);
            child.transform.GetComponent<SpriteRenderer>().sprite = BGStarter[randomBG];
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        foreach(Transform child in transform)
        {
            child.transform.position = new Vector2(child.transform.position.x - velocity, child.transform.position.y);
        }

        //transform.position = new Vector2(transform.position.x - velocity, transform.position.y);
    }

}
