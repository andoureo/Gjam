using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;


public class CatCastle : MonoBehaviourPunCallbacks, IPunObservable
{
    private Rigidbody2D rb;

    public float CatCastleHP = 50;

    [SerializeField]
    private Image catcastleHpBar;
    [SerializeField]
    public float currentcatcastleHp = 50;

    GameObject panel;

    GameSceneManager gameSceneManager;

    public TextMeshProUGUI you;

    // GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            you.text = "you";
        }
        gameSceneManager = GameObject.Find("GameManager").GetComponent<GameSceneManager>();

        panel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;

        rb = GetComponent<Rigidbody2D>();
        /*canvas = GameObject.Find("Canvas");
        this.transform.SetParent(canvas.transform, false);
        RectTransform rect = GetComponent<RectTransform>();
        rect.localPosition += new Vector3(700, -300, 0);*/
    }

    // Update is called once per frame
    void Update()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        // スタミナをゲージに反映する
        //Debug.Log(currentcatcastleHp);
        catcastleHpBar.fillAmount = currentcatcastleHp / CatCastleHP;
        if (currentcatcastleHp == 0)
        {
            gameSceneManager.TurnResult();
            //panel.SetActive(true);
            GetComponent<Animator>().SetBool("Attack", true);
            //Destroy(this.gameObject);
            if (PhotonNetwork.IsMasterClient)
            {
                gameSceneManager.resultStateManagerwin.gameObject.SetActive(true);
                gameSceneManager.resultStateManagerwin.SetTimerText((int)gameSceneManager.mElapsedTime);
                gameSceneManager.resultStateManagerwin.setscoretext((int)gameSceneManager.HostScore);
            }
            else
            {
                gameSceneManager.resultStateManagerlose.gameObject.SetActive(true);
                gameSceneManager.resultStateManagerlose.SetTimerText((int)gameSceneManager.mElapsedTime);
                gameSceneManager.resultStateManagerlose.setscoretext((int)gameSceneManager.HostScore);
            }
        }
        // }
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身のアバターのスタミナを送信する
            stream.SendNext(currentcatcastleHp);
        }
        else
        {
            // 他プレイヤーのアバターのスタミナを受信する
            currentcatcastleHp = (float)stream.ReceiveNext();
        }
    }
}
