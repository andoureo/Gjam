using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CatMove : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;

    public bool isMove = true;

    private GameObject enemy;

    private bool isAttack;

    private bool isAttackCastle = false;


    private GameObject DogCastle;

    public float Hp;

    public float power;

    //DogMove dogMove;

    private DogMove dogMove;

    private DogCastle dogCastle;

    //GameObject canvas;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //enemy = GameObject.FindWithTag("Enemy");

        /*canvas = GameObject.Find("Canvas");
        this.transform.SetParent(canvas.transform, false);
        RectTransform rect = GetComponent<RectTransform>();
        rect.localPosition += new Vector3(500, -400, 0);*/
        this.transform.Rotate(0f, 180f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
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
        if (Hp == 0)
        {

            Destroy(this.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isMove = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isAttack = true;
            dogMove = collision.gameObject.GetComponent<DogMove>();

        }
        else if (collision.gameObject.tag == "DogCastle")
        {

            if (!isAttack)
            {

                isMove = false;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;

                isAttackCastle = true;
                dogCastle = collision.gameObject.GetComponent<DogCastle>();
            }
            else { return; }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
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
        //enemy.GetComponent<DogMove>().dogHP--;
        //dogMove=collision.gameObject.GetComponent<dogMove>();
        dogMove.dogHP = dogMove.dogHP - power;
        Debug.Log(dogMove.dogHP);
        isAttack = true;
    }
    private IEnumerator Attack2()
    {
        GetComponent<Animator>().SetBool("Attack", true);
        yield return new WaitForSeconds(1.4f);
        //enemy.GetComponent<DogMove>().dogHP--;
        //dogMove=collision.gameObject.GetComponent<dogMove>();
        dogCastle.currentdogcastleHp = dogCastle.currentdogcastleHp - power;
        Debug.Log(dogCastle.currentdogcastleHp);
        isAttackCastle = true;
    }
}
