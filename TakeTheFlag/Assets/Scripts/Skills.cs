using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] float multImpulse = 3;
    [SerializeField] float range = 1;
    float movex;
    float movey;

    float movex2;
    float movey2;

    [SerializeField] List<float> botsDist = new List<float>();
    GameObject[] bots;


    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if(bots == null)
        {
            bots = GameObject.FindGameObjectsWithTag("Bot");
        }
    }

    // Update is called once per frame
    void Update()
    {

        Direction();
        movex = Input.GetAxisRaw("Horizontal");
        movey = Input.GetAxisRaw("Vertical");

    }

    public void Creep(float speed)
    {

        if (movex != 0 && movey != 0)
            rb2d.AddForce(new Vector2(movex * (speed * multImpulse), movey * (speed * multImpulse)), ForceMode2D.Impulse);
        else
            rb2d.AddForce(new Vector2(movex2 * (speed * multImpulse), movey2 * (speed * multImpulse)), ForceMode2D.Impulse);

    }

    private void Direction()
    {
        if (movex > 0 || movex < 0)
        {
            movex2 = movex;
        }

        if (movey > 0 || movey < 0)
        {
            movey2 = movey;
        }

        if (movex == 0 && movey > 0 || movey < 0 && movex == 0)
        {
            movex2 = movex;
        }

        if (movey == 0 && movex > 0 || movex < 0 && movey == 0)
        {
            movey2 = movey;
        }
    }

    public void Freeze()
    {
        for (int i = 0; i < bots.Length; i++)
        {
            Vector2 thisPos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
            Vector2 otherPos = new Vector2(bots[i].transform.position.x, bots[i].transform.position.y);
            float tempDist = Vector2.Distance(thisPos, otherPos);
            if(tempDist <= range)
            {
                bots[i].GetComponent<BotMove>().enabled = false;
            }
        }
    }

    public void Unfreeze()
    {
        for (int i = 0; i < bots.Length; i++)
        {
            Vector2 thisPos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
            Vector2 otherPos = new Vector2(bots[i].transform.position.x, bots[i].transform.position.y);
            float tempDist = Vector2.Distance(thisPos, otherPos);
            if (tempDist <= range && bots[i].GetComponent<BotMove>().enabled == false)
            {
                bots[i].GetComponent<BotMove>().enabled = true;
            }
        }
    }
}
