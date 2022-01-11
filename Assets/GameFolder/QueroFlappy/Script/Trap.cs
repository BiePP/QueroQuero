using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
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
        if (collision.CompareTag("Player"))
        {
            print("Colidiu com o player, fim do jogo!");
            /*--GAME OVER*--*/
            //TODO não será game over, mas sim uma tela de restart
            Time.timeScale = 0f;
        }
    }
}
