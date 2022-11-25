using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class GameSceneManager : MonoBehaviourPunCallbacks
{

    // 一致したカードリストID
    private List<int> mContainCardIdList = new List<int>();

    // カード生成マネージャクラス
    public CardCreateManager CardCreate;

    // 時間管理クラス
    public TimerManager timerManager;

    // スタートステートクラス
    public StartStateManager startStateManager;

    public ResultStateManager resultStateManagerwin;

    public ResultStateManager resultStateManagerlose;

    public ResultStateManager resultStateManagerdraw;
    // 経過時間
    public float mElapsedTime;

    public int HostScore = 0;

    public int GuestScore = 0;

    public GameObject panel;

    // ゲームステート管理
    private EGameState mEGameState;

    DogCastle dogCastle;

    CatCastle catCastle;

    float currentdogcastleHp;

    float currentcatcastleHp;

    public GameObject game;

    public GameObject Master;

    private int x = 0;
    void Start()
    {

        /* // 一致したカードIDリストを初期化
         this.mContainCardIdList.Clear();

         // カードリストを生成する
         this.CardCreate.CreateCard();

         // 時間を初期化
         this.mElapsedTime = 0f;*/
        Master.gameObject.SetActive(true);
        // ゲームステートを初期化
        panel.SetActive(false);
        // ゲームステートを初期化
        this.mEGameState = EGameState.START;

        // スタートエリアを表示
        this.startStateManager.gameObject.SetActive(false);

        // ゲームのステート管理
        this.mSetGameState();


    }
    /// <summary>
    /// ゲームステートで処理を変更する
    /// </summary>
    private void mSetGameState()
    {

        switch (this.mEGameState)
        {
            // スタート画面
            case EGameState.START:
                // スタートエリアを表示
                this.startStateManager.gameObject.SetActive(true);
                // ゲームスタートの開始
                this.mSetStartState();
                break;
            // ゲーム準備期間
            case EGameState.READY:
                this.startStateManager.gameObject.SetActive(false);
                this.mSetGameReady();
                break;
            // ゲーム中
            case EGameState.GAME:
                break;
            // 結果画面
            case EGameState.RESULT:
                this.resultStateManagerdraw.gameObject.SetActive(true);
                //this.mSetResultState();
                //resultJudge();
                break;
        }
    }

    private void mSetStartState()
    {
        // テキストの拡大縮小アニメーション
        this.startStateManager.EnlarAnimation();

    }

    /// <summary>
    /// Readyステートに遷移する
    /// </summary>
    public void OnGameStart()
    {
        // StartCoroutine(Example());
        // ゲームステートを初期化
        this.mEGameState = EGameState.READY;

        // ゲームのステート管理
        this.mSetGameState();
        panel.SetActive(true);
    }
    /// <summary>
    /// リザルトステートの設定処理
    /// </summary>
    private void mSetResultState()
    {

        //this.resultStateManager.SetTimerText((int)this.mElapsedTime);
    }

    /// <summary>
    /// スタート画面に遷移する
    /// </summary>
    public void OnBackStartStatewin()
    {

        // ResultAreaを非表示にする
        this.resultStateManagerwin.gameObject.SetActive(false);

        // ゲームステートをStartに変更
        this.mEGameState = EGameState.START;

        // ゲームのステート管理
        this.mSetGameState();
    }
    public void OnBackStartStatelose()
    {

        // ResultAreaを非表示にする
        this.resultStateManagerlose.gameObject.SetActive(false);

        // ゲームステートをStartに変更
        this.mEGameState = EGameState.START;

        // ゲームのステート管理
        this.mSetGameState();
    }
    public void OnBackStartStatedraw()
    {

        // ResultAreaを非表示にする
        this.resultStateManagerdraw.gameObject.SetActive(false);

        // ゲームステートをStartに変更
        this.mEGameState = EGameState.START;

        // ゲームのステート管理
        this.mSetGameState();
    }

    /// <summary>
    /// ゲームの準備ステートを開始する
    /// </summary>
    private void mSetGameReady()
    {

        // カード配布アニメーションが終了した後のコールバック処理を実装する
        this.CardCreate.OnCardAnimeComp = null;
        this.CardCreate.OnCardAnimeComp = () =>
        {

            // ゲームステートをGAME状態に変更する
            this.mEGameState = EGameState.GAME;
            this.mSetGameState();
        };
        // 一致したカードIDリストを初期化
        this.mContainCardIdList.Clear();

        // カードリストを生成する
        this.CardCreate.CreateCard();

        // 時間を初期化
        this.mElapsedTime = 0f;
        if (PhotonNetwork.IsMasterClient)
        {

            PhotonNetwork.Instantiate("NewCatCastle", new Vector3(7, -2, 0), Quaternion.identity);
            PhotonNetwork.Instantiate("DogCastle1", new Vector3(-7, -2, 0), Quaternion.identity);
            // PhotonNetwork.Instantiate("cat1", new Vector3(5, -2, -1), Quaternion.identity);
            //PhotonNetwork.Instantiate("dag1", new Vector3(-5, -2, -1), Quaternion.identity);
            x++;
            /*dogCastle = GameObject.Find("DogCastle(Clone)").GetComponent<DogCastle>();
            currentdogcastleHp = dogCastle.currentdogcastleHp;
            catCastle = GameObject.Find("newCatCastle(Clone)").GetComponent<CatCastle>();
            currentcatcastleHp = catCastle.currentcatcastleHp;*/

        }
        dogCastle = GameObject.Find("DogCastle1(Clone)").GetComponent<DogCastle>();
        currentdogcastleHp = dogCastle.currentdogcastleHp;
        catCastle = GameObject.Find("NewCatCastle(Clone)").GetComponent<CatCastle>();
        currentcatcastleHp = catCastle.currentcatcastleHp;
    }
    /*IEnumerator Example()
    {
        Debug.Log("wait");
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount >= 2);
        Debug.Log("FULL");
    }*/

    void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            ///panel.SetActive(false);
            if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
            {
                panel.SetActive(false);
            }

            // GameState が GAME状態なら
            if (this.mEGameState == EGameState.GAME)
            {
                if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
                {


                    this.mElapsedTime += Time.deltaTime;

                    this.timerManager.SetText((int)this.mElapsedTime);
                }

                // 選択したカードが２枚以上になったら
                if (GameStateController.Instance.SelectedCardIdList.Count >= 2)
                {

                    // 最初に選択したCardIDを取得する
                    int selectedId = GameStateController.Instance.SelectedCardIdList[0];

                    // 2枚目にあったカードと一緒だったら
                    if (selectedId == GameStateController.Instance.SelectedCardIdList[1])
                    {

                        if (PhotonNetwork.IsMasterClient)
                        {
                            HostScore++;
                            //Debug.Log(HostScore);
                            RamdomcreateDog();
                            //PhotonNetwork.Instantiate("dag1", new Vector3(-5, -2, -1), Quaternion.identity);
                            // PhotonNetwork.Instantiate("cat1", new Vector3(5, -2, -1), Quaternion.identity);
                        }
                        else if (!PhotonNetwork.IsMasterClient)
                        {
                            GuestScore++;
                            //Debug.Log(GuestScore);
                            RamdomcreateCat();
                            //photonView.RPC(nameof(spona), RpcTarget.All);
                            //photonView.RPC(nameof(Score), RpcTarget.All, GuestScore);
                            //PhotonNetwork.Instantiate("cat1", new Vector3(5, -2, -1), Quaternion.identity);

                        }
                        Debug.Log($"Contains! {selectedId}");
                        // 一致したカードIDを保存する
                        this.mContainCardIdList.Add(selectedId);
                    }

                    // カードの表示切り替えを行う
                    this.CardCreate.HideCardList(this.mContainCardIdList);

                    // 選択したカードリストを初期化する
                    GameStateController.Instance.SelectedCardIdList.Clear();
                }
                // 配置した全種類のカードを獲得したら
                /*if (this.mContainCardIdList.Count >= 18)
                {
                    OnGameStart();
                    //mSetGameReady();
                    // ゲームをリザルトステートに遷移する
                    //this.mEGameState = EGameState.RESULT;
                    //this.mSetGameState();
                }*/
                /*if (currentcatcastleHp <= 0)
                {
                    Debug.Log("Down!!");
                    this.mEGameState = EGameState.RESULT;
                    this.mSetGameState();
                }
                if (currentdogcastleHp <= 0)
                {
                    Debug.Log("Down!!");
                    this.mEGameState = EGameState.RESULT;
                    this.mSetGameState();
                }*/
                if (mElapsedTime >= 150)
                {
                    Debug.Log("Draw!!");
                    this.mEGameState = EGameState.RESULT;
                    this.mSetGameState();
                }

            }
        }
    }

    public void resultJudge()
    {
        if (PhotonNetwork.IsMasterClient && currentcatcastleHp <= 0)
        {
            this.resultStateManagerwin.gameObject.SetActive(true);
            this.resultStateManagerwin.SetTimerText((int)this.mElapsedTime);
            this.mSetResultState();
        }
        else if (PhotonNetwork.IsMasterClient && currentdogcastleHp <= 0)
        {
            this.resultStateManagerlose.gameObject.SetActive(true);
            this.resultStateManagerlose.SetTimerText((int)this.mElapsedTime);
            this.mSetResultState();
        }
        else if (!PhotonNetwork.IsMasterClient && currentcatcastleHp <= 0)
        {
            this.resultStateManagerwin.gameObject.SetActive(true);
            this.resultStateManagerwin.SetTimerText((int)this.mElapsedTime);
            this.mSetResultState();
        }
        else if (!PhotonNetwork.IsMasterClient && currentdogcastleHp <= 0)
        {
            this.resultStateManagerlose.gameObject.SetActive(true);
            this.resultStateManagerlose.SetTimerText((int)this.mElapsedTime);
            this.mSetResultState();
        }
        else if (mElapsedTime >= 120)
        {
            this.resultStateManagerdraw.gameObject.SetActive(true);
            this.resultStateManagerdraw.SetTimerText((int)this.mElapsedTime);
            this.mSetResultState();
        }
    }
    private void RamdomcreateDog()
    {
        // １から１００までの中からランダムに数字を選択する。
        int number = UnityEngine.Random.Range(1, 10);

        if (number <= 7)
        {
            PhotonNetwork.Instantiate("dag1", new Vector3(-5, -2, -1), Quaternion.identity);
        }
        else if (number == 9 || number == 8)
        {
            PhotonNetwork.Instantiate("dag2", new Vector3(-5, -2, -1), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("dag3", new Vector3(-5, -2, -1), Quaternion.identity);
        }
    }
    public void TurnResult()
    {
        this.mEGameState = EGameState.RESULT;
        panel.SetActive(true);
        Debug.Log("Turn");
    }
    private void RamdomcreateCat()
    {
        // １から１００までの中からランダムに数字を選択する。
        int number = UnityEngine.Random.Range(1, 10);

        if (number <= 7)
        {
            PhotonNetwork.Instantiate("cat1", new Vector3(5, -2, -1), Quaternion.identity);
        }
        else if (number == 9 || number == 8)
        {
            PhotonNetwork.Instantiate("cat2", new Vector3(5, -2, -1), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("cat3", new Vector3(5, -2, -1), Quaternion.identity);
        }
    }
    public void onclickcontinue()
    {
        //this.gameObject.SetActive(false);
        resultStateManagerdraw.gameObject.SetActive(false);
        resultStateManagerlose.gameObject.SetActive(false);
        resultStateManagerwin.gameObject.SetActive(false);
        Instantiate(game);
        GameObject obj = this.transform.root.gameObject;
        Destroy(obj);

    }

    [PunRPC]
    private void Score(int score)
    {
        GuestScore = score;
    }
    /* [PunRPC]
     private void spona(int score)
     {
         PhotonNetwork.Instantiate("cat1", new Vector3(5, -2, -1), Quaternion.identity);
     }*/
}
