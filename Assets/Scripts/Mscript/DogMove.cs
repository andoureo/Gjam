using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class DogMove : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;

    public bool isMove = true;
    private bool isAttack;

    private bool isAttackCastle = false;

    public float dogHP;

    private CatMove catMove;

    public float power;

    private CatCastle catCastle;

    //GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        /*canvas = GameObject.Find("Canvas");
        this.transform.SetParent(canvas.transform, false);
        RectTransform rect = GetComponent<RectTransform>();
        rect.localPosition += new Vector3(-500, -400, 0);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Animator>().SetBool("Attack", false);
        }
        else
        {
            if (isAttack)
            {
                isAttack = false;
                StartCoroutine(Attack());
            }
            else if (isAttackCastle)
            {
                isAttackCastle = false;
                StartCoroutine(Attack2());
            }
        }

        if (dogHP == 0)
        {

            // PhotonNetwork.Destroy(gameObject);
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMove = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isAttack = true;
            catMove = collision.gameObject.GetComponent<CatMove>();
        }
        else if (collision.gameObject.tag == "CatCastle")
        {
            if (!isAttack)
            {
                isMove = false;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                isAttackCastle = true;
                catCastle = collision.gameObject.GetComponent<CatCastle>();
            }
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMove = true;
            isAttack = false;
        }
        else if (collision.gameObject.tag == "DogCastle")
        {
            isMove = true;
            isAttackCastle = false;
        }
    }
    private IEnumerator Attack()
    {
        GetComponent<Animator>().SetBool("Attack", true);
        yield return new WaitForSeconds(1.4f);
        //enemy.GetComponent<EnemyMove>().dogHP--;
        //enemyMove=collision.gameObject.GetComponent<enemyMove>();
        catMove.Hp = catMove.Hp - power;
        Debug.Log(catMove.Hp);
        isAttack = true;
    }
    private IEnumerator Attack2()
    {
        GetComponent<Animator>().SetBool("Attack", true);
        yield return new WaitForSeconds(1.4f);
        //enemy.GetComponent<DogMove>().dogHP--;
        //dogMove=collision.gameObject.GetComponent<dogMove>();
        catCastle.currentcatcastleHp = catCastle.currentcatcastleHp - power;
        Debug.Log(catCastle.currentcatcastleHp);
        isAttackCastle = true;
    }
}