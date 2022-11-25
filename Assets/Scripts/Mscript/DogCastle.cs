using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;


public class DogCastle : MonoBehaviourPunCallbacks, IPunObservable
{
    private Rigidbody2D rb;

    public float dogcastleHP = 50;

    [SerializeField]
    private Image dogcastleHpBar;
    [SerializeField]
    public float currentdogcastleHp = 50;

    GameObject panel;

    GameSceneManager gameSceneManager;

    public TextMeshProUGUI you;


    //GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            you.text = "you";
        }
        gameSceneManager = GameObject.Find("GameManager").GetComponent<GameSceneManager>();
        panel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;
        rb = GetComponent<Rigidbody2D>();
        /*canvas = GameObject.Find("Canvas");
        this.transform.SetParent(canvas.transform, false);
        RectTransform rect = GetComponent<RectTransform>();
        rect.localPosition += new Vector3(-730, -300, 0);*/

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentdogcastleHp);
        // if (PhotonNetwork.IsMasterClient)
        //{
        // スタミナをゲージに反映する
        dogcastleHpBar.fillAmount = currentdogcastleHp / dogcastleHP;
        if (currentdogcastleHp == 0)
        {
            gameSceneManager.TurnResult();
            //panel.SetActive(true);
            GetComponent<Animator>().SetBool("Attack", true);
            //Destroy(this.gameObject);
            if (PhotonNetwork.IsMasterClient)
            {
                gameSceneManager.resultStateManagerlose.gameObject.SetActive(true);
                gameSceneManager.resultStateManagerlose.SetTimerText((int)gameSceneManager.mElapsedTime);
                gameSceneManager.mElapsedTime = 0;
                gameSceneManager.resultStateManagerlose.setscoretext((int)gameSceneManager.HostScore);
            }
            else
            {
                gameSceneManager.resultStateManagerwin.gameObject.SetActive(true);
                gameSceneManager.resultStateManagerwin.SetTimerText((int)gameSceneManager.mElapsedTime);
                gameSceneManager.mElapsedTime = 0;
                gameSceneManager.resultStateManagerwin.setscoretext((int)gameSceneManager.GuestScore);
            }

        }
        /// }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身のアバターのスタミナを送信する
            stream.SendNext(currentdogcastleHp);
        }
        else
        {
            // 他プレイヤーのアバターのスタミナを受信する
            currentdogcastleHp = (float)stream.ReceiveNext();
        }
    }
}
