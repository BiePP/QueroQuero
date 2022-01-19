using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaboDePalhaFlappyController : MonoBehaviour
{
    public float velocityX;

    private void Awake()
    {
        /*-- Organiza a velocidade do bicho --*/
        switch (this.transform.root.GetComponent<FlappyQueroStageController>().stageMoment)
        {
            case 2:
                velocityX = Random.Range(velocityX, velocityX * 1.3f);
                break;
            case 3:
                velocityX = Random.Range(velocityX, velocityX * 1.7f);
                break;
            case 4:
                velocityX = Random.Range(velocityX, velocityX * 2f);
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
    }
}
