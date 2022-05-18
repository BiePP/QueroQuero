using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRunner : MonoBehaviour
{
    public float timer = 0;
    public float velocity;

    public Sprite[] BGStarter;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        //foreach child in Skin GameObject (foreach GO background)
        foreach(Transform child in transform)
        {
            int randomBG = Random.Range(0, 2);
            child.transform.GetComponent<SpriteRenderer>().sprite = BGStarter[randomBG];
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (active)
        {
            foreach (Transform child in transform)
            {
                child.transform.position = new Vector2(child.transform.position.x - velocity, child.transform.position.y);
            }
        }
        
    }

}
