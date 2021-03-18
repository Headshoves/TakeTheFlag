using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMove : MonoBehaviour
{
    float dir = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 22f)
            dir = -5;

        else if (transform.position.x <= -6f)
            dir = 5;

        gameObject.transform.Translate(new Vector3(dir * Time.deltaTime, 0, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Limits"))
        {
            dir = dir* -1;
        }
    }
}
