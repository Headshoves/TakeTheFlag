using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    private Movement move;
    private Skills skills;
    private Rigidbody2D rb2d;


    void Start()
    {
        move = GetComponent<Movement>();
        skills = GetComponent<Skills>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Creep());
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            skills.Freeze();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            skills.Unfreeze();
        }

    }

    private void FixedUpdate()
    {
        move.Move(speed);
    }

    private IEnumerator Creep()
    {
        move.Move(0);
        move.enabled = false;
        yield return new WaitForSeconds(0.1f);
        skills.Creep(speed);
        yield return new WaitForSeconds(0.3f);
        rb2d.velocity = Vector2.zero;
        move.enabled = true;
    }
}
