using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlappyController : MonoBehaviour
{
    public enum EnemyType
    {
        Cardeal,
        BemTeVi,
        RaboDePalha,
        JoaoDeBarro
    }
    public EnemyType birdType;
    public bool triggered;

    public float velocityX; //see background velocity in BGRunner script for an idea!
    [Tooltip("The max a Cardeal can go on Y axis (Cardeal only)")]
    public float maxY;
    [Tooltip("The min a Cardeal can go on Y axis (Cardeal only)")]
    public float minY;

    public float velocityY;
    [Tooltip("Should Cardeal go up or down? (Cardeal only)")]
    public bool up;

    private void Awake()
    {
        /*-- Organiza a velocidade do bicho dependendo do momento da fase. --*/
        if(birdType != EnemyType.JoaoDeBarro)
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
        
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (birdType)
        {
            case EnemyType.BemTeVi:
                if (this.transform.position.y > 3.5f)
                {
                    velocityY = velocityY * -1;
                }
                break;
        }

        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (birdType)
        {
            case EnemyType.Cardeal:
                //move toward quero-quero
                transform.position = new Vector2(transform.position.x - velocityX, transform.position.y);

                if (up && transform.position.y < maxY)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + velocityY);
                    return;
                }
                if (up && transform.position.y >= maxY)
                {
                    up = false;
                    transform.position = new Vector2(transform.position.x, transform.position.y - velocityY);
                    return;
                }
                if (!up && transform.position.y > minY)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y - velocityY);
                    return;
                }
                if (!up && transform.position.y <= minY)
                {
                    up = true;
                    transform.position = new Vector2(transform.position.x, transform.position.y + velocityY);
                    return;
                }
                break;
            case EnemyType.BemTeVi:
                //move toward quero-quero
                transform.position = new Vector2(transform.position.x - velocityX, transform.position.y + velocityY);
                break;
            case EnemyType.RaboDePalha:
                //move toward quero-quero
                transform.position = new Vector2(transform.position.x - velocityX, transform.position.y);
                break;
            case EnemyType.JoaoDeBarro:
                //move toward quero-quero
                transform.position = new Vector2(transform.position.x - velocityX, transform.position.y);
                break;
        }
    }
}
