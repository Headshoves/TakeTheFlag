using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    float movex;
    float movey;

    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movex = Input.GetAxisRaw("Horizontal");
        movey = Input.GetAxisRaw("Vertical");
    }

    public void Move( float speed)
    {
        //rb2d.velocity = new Vector2(movex * speed, (movey / 2) * speed);
        this.transform.transform.Translate((movex * speed) * Time.deltaTime, ((movey/2) * speed) * Time.deltaTime, 0);
    }
}
