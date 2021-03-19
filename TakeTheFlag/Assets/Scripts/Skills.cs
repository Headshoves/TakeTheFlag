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

    /*Mapeamento de Todos os elementos do jogo*/
    GameObject[] bots;

    /*LADO ESQUERDO*/
    GameObject leftFlag;
    GameObject leftArena;
    GameObject leftArea;

    /*LADO DIREITO*/
    GameObject rightFlag;
    GameObject rightArena;
    GameObject rightArea;

    GameObject division;

    private bool takeFlag;

    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if(bots == null)
        {
            bots = GameObject.FindGameObjectsWithTag("Bot");
        }

        /*LADO ESQUERDO*/
        leftFlag = GameObject.FindGameObjectWithTag("LeftFlag");
        leftArena = GameObject.FindGameObjectWithTag("LeftArena");
        leftArea = GameObject.FindGameObjectWithTag("LeftArea");

        /*LADO DIREITO*/
        rightFlag = GameObject.FindGameObjectWithTag("RightFlag");
        rightArena = GameObject.FindGameObjectWithTag("RightArena");
        rightArea = GameObject.FindGameObjectWithTag("RightArea");

        division = GameObject.FindGameObjectWithTag("Division");
    }

    // Update is called once per frame
    void Update()
    {

        TakeFlag();

        Direction();
        movex = Input.GetAxisRaw("Horizontal");
        movey = Input.GetAxisRaw("Vertical");

    }

    public void Creep(float speed)
    {
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

    public void TakeFlag()
    {
        if(transform.parent.tag == "LeftPlayer" && takeFlag)
        {
            rightFlag.transform.parent = transform;
            rightFlag.transform.localPosition = new Vector2(0.2f, 0.3f);
        }

        if (transform.parent.tag == "RightPlayer" && takeFlag)
        {
            leftFlag.transform.parent = transform;
            leftFlag.transform.localPosition = new Vector2(0.2f, 0.3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //CHECAGEM PARA PEGAR A BANDEIRA

        if(transform.parent.tag == "LeftPlayer" && collision.gameObject.CompareTag("RightFlag"))
        {
            takeFlag = true;
        }

        if(transform.parent.tag == "RightPlayer" && collision.gameObject.CompareTag("LeftFlag"))
        {
            takeFlag = true;
        }


        //CHECAGEM PARA MARCAÇÃO DE PONTOS QUANDO PASSA DO MEIO DO CAMPO

        if(leftFlag.transform.IsChildOf(transform) && collision.gameObject.CompareTag("Division"))
        {
            leftFlag.transform.parent = leftArea.transform;
            leftFlag.transform.localPosition = new Vector2(-5, 2.5f);
            takeFlag = false;
        }

        if (rightFlag.transform.IsChildOf(transform) && collision.gameObject.CompareTag("Division"))
        {
            rightFlag.transform.parent = rightArea.transform;
            rightFlag.transform.localPosition = new Vector2(20, 2.5f);
            takeFlag = false;
        }

        //RECUPERAÇÃO DE BANDEIRA

        if(leftFlag.transform.parent == null && transform.parent.tag == "LeftPlayer")
        {
            leftFlag.transform.parent = leftArea.transform;
            leftFlag.transform.localPosition = new Vector2(-5, 2.5f);
        }
    }
}
