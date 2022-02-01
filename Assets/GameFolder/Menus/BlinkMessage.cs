using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkMessage : MonoBehaviour
{
    bool blink;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        blink = false;
        text = GetComponent<Text>();

        StartCoroutine(Blink());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Blink()
    {
        while (this.enabled)
        {
            if (blink)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                blink = false;
            }
            else
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                blink = true;
            }
            yield return new WaitForSeconds(0.8f);
        }
        yield return false;
         
    }
}
