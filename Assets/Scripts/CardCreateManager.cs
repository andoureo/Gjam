using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using DG.Tweening;
using Photon.Pun;

public class CardCreateManager : MonoBehaviour
{

    // 生成するCardオブジェクト
    public Card CardPrefab;


    // 「カード」を生成する親オブジェクト
    public RectTransform CardCreateParent;

    // 生成したカードオブジェクトを保存する
    public List<Card> CardList = new List<Card>();
    // カード情報の順位をランダムに変更したリスト
    private List<CardData> mRandomCardDataList = new List<CardData>();

    // GridLayoutGroup
    public GridLayoutGroup GridLayout;

    // カードの生成アニメーションが終わった時
    public Action OnCardAnimeComp;

    // カード配列のインデックス
    private int mIndex;

    // カードを生成する時の高さインデックス
    private int mHelgthIdx;
    // カードを生成する時の幅インデックス
    private int mWidthIdx;

    // カードの生成アニメーションのアニメーション時間
    private readonly float DEAL_CAED_TIME = 0.2f;

    /*void Start()
    {

        //Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
        // カード情報リスト
        List<CardData> cardDataList = new List<CardData>();

        // 表示するカード画像情報のリスト
        List<Sprite> imgList = new List<Sprite>();

        // Resources/Imageフォルダ内にある画像を取得する
        imgList.Add(Resources.Load<Sprite>("Image/card_image_000"));
        imgList.Add(Resources.Load<Sprite>("Image/card_image_001"));
        imgList.Add(Resources.Load<Sprite>("Image/card_image_002"));
        // forを回す回数を取得する
        int loopCnt = imgList.Count;

        for (int i = 0; i < loopCnt; i++)
        {

            // カード情報を生成する
            CardData cardata = new CardData(i, imgList[i]);
            cardDataList.Add(cardata);
        }

        // 生成したカードリスト２つ分のリストを生成する
        List<CardData> SumCardDataList = new List<CardData>();
        SumCardDataList.AddRange(cardDataList);
        SumCardDataList.AddRange(cardDataList);

        // リストの中身をランダムに再配置する
        List<CardData> randomCardDataList = SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList();

        // カードオブジェクトを生成する
        foreach (var _cardData in randomCardDataList)
        {

            // Instantiate で Cardオブジェクトを生成
            Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
            // データを設定する
            card.Set(_cardData);

            // 生成したカードオブジェクトを保存する
            this.CardList.Add(card);
        }
    }*/
    /// <summary>
    /// カードを生成する
    /// </summary>
    public void CreateCard()
    {
        foreach (Transform child in this.CardCreateParent)
        {
            Destroy(child.gameObject);
        }
        //Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
        // カード情報リスト
        List<CardData> cardDataList = new List<CardData>();
        List<CardData> cardDataList2 = new List<CardData>();
        // 表示するカード画像情報のリスト
        List<Sprite> imgList = new List<Sprite>();
        List<Sprite> imgList2 = new List<Sprite>();
        // Resources/Imageフォルダ内にある画像を取得する
        /*imgList.Add(Resources.Load<Sprite>("Image/card_image_000"));
        imgList.Add(Resources.Load<Sprite>("Image/card_image_001"));
        imgList.Add(Resources.Load<Sprite>("Image/card_image_002"));
        imgList.Add(Resources.Load<Sprite>("Image/card_image_003"));
        imgList.Add(Resources.Load<Sprite>("Image/card_image_004"));
        imgList.Add(Resources.Load<Sprite>("Image/card_image_005"));
        imgList.Add(Resources.Load<Sprite>("Image/card_image_006"));
        imgList.Add(Resources.Load<Sprite>("Image/card_image_007"));
        imgList.Add(Resources.Load<Sprite>("Image/card_image_008"));
        /* imgList.Add(Resources.Load<Sprite>("Image/card_image_009"));
         imgList.Add(Resources.Load<Sprite>("Image/card_image_010"));
         imgList.Add(Resources.Load<Sprite>("Image/card_image_011"));*/
        // forを回す回数を取得する
        //int loopCnt = imgList.Count;

        /*for (int i = 0; i < loopCnt; i++)
        {

            // カード情報を生成する
            CardData cardata = new CardData(i, imgList[i]);
            cardDataList.Add(cardata);
        }*/
        imgList.Add(Resources.Load<Sprite>("Image/Chinese/002"));
        imgList.Add(Resources.Load<Sprite>("Image/Chinese/003"));
        imgList.Add(Resources.Load<Sprite>("Image/Chinese/004"));
        imgList.Add(Resources.Load<Sprite>("Image/Chinese/006"));
        imgList.Add(Resources.Load<Sprite>("Image/Chinese/009"));
        imgList.Add(Resources.Load<Sprite>("Image/Chinese/012"));
        imgList.Add(Resources.Load<Sprite>("Image/Chinese/015"));
        imgList.Add(Resources.Load<Sprite>("Image/Chinese/016"));
        imgList.Add(Resources.Load<Sprite>("Image/Chinese/020"));
        int loopCnt = imgList.Count;
        for (int i = 0; i < loopCnt; i++)
        {
            // カード情報を生成する
            CardData cardata = new CardData(i, imgList[i]);
            cardDataList.Add(cardata);
        }
        //imgList2.Add(Resources.Load<Sprite>());
        imgList2.Add(Resources.Load<Sprite>("Image/English/2.SITE"));
        imgList2.Add(Resources.Load<Sprite>("Image/English/3.LACK"));
        imgList2.Add(Resources.Load<Sprite>("Image/English/4.CLIENT"));
        imgList2.Add(Resources.Load<Sprite>("Image/English/6.TRAFIC"));
        imgList2.Add(Resources.Load<Sprite>("Image/English/9.FUEL"));
        imgList2.Add(Resources.Load<Sprite>("Image/English/12.DELAY"));
        imgList2.Add(Resources.Load<Sprite>("Image/English/15.BLANK"));
        imgList2.Add(Resources.Load<Sprite>("Image/English/16.DUE"));
        imgList2.Add(Resources.Load<Sprite>("Image/English/20.THUS"));

        int loopCnt2 = imgList2.Count;
        for (int i = 0; i < loopCnt2; i++)
        {

            // カード情報を生成する
            CardData cardata = new CardData(i, imgList2[i]);
            cardDataList2.Add(cardata);
        }

        this.mIndex = 0;
        this.mHelgthIdx = 0;
        this.mWidthIdx = 0;
        // 生成したカードリスト２つ分のリストを生成する
        List<CardData> SumCardDataList = new List<CardData>();
        SumCardDataList.AddRange(cardDataList);
        //Debug.Log("SumCardDataList:" + SumCardDataList.Count);
        SumCardDataList.AddRange(cardDataList2);

        // ランダムリストの初期化
        this.mRandomCardDataList.Clear();

        // リストの中身をランダムに再配置する
        this.mRandomCardDataList = SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList();
        //this.mRandomCardDataList.AddRange(SumCardDataList.OrderBy(a => Guid.NewGuid()).ToList());

        Debug.Log("mRandomCardDataList:" + mRandomCardDataList.Count);
        // GridLayoutを無効
        this.GridLayout.enabled = false;

        // カードを配るアニメーション処理
        this.mSetDealCardAnime();




        // カードオブジェクトを生成する
        /*foreach (var _cardData in mRandomCardDataList)
        {

            // Instantiate で Cardオブジェクトを生成
            Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
            // データを設定する
            card.Set(_cardData);

            // 生成したカードオブジェクトを保存する
            this.CardList.Add(card);
        }*/
    }

    /// <summary>
    /// カードを配るアニメーション処理
    /// </summary>
    private void mSetDealCardAnime()
    {

        var _cardData = this.mRandomCardDataList[this.mIndex];

        // Instantiate で Cardオブジェクトを生成
        //Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);
        Card card = Instantiate<Card>(this.CardPrefab, this.CardCreateParent);

        /*sphereObject = GameObject.Find("Sphere");
    sphereScript = sphereObject.GetComponent<SphereScript>();*/
        // データを設定する
        card.Set(_cardData);
        // カードの初期値を設定 (画面外にする)
        //card.mRt.anchoredPosition = new Vector2(1900, 0f);
        card.mRt.anchoredPosition = new Vector2(1900, 0f);
        // サイズをGridLayoutのCellSizeに設定
        //card.mRt.sizeDelta = this.GridLayout.cellSize;
        card.mRt.sizeDelta = this.GridLayout.cellSize;

        // カードの移動先を設定
        float posX = (this.GridLayout.cellSize.x * this.mWidthIdx) + (this.GridLayout.spacing.x * (this.mWidthIdx + 1));
        float posY = ((this.GridLayout.cellSize.y * this.mHelgthIdx) + (this.GridLayout.spacing.y * this.mHelgthIdx)) * -1f;

        // DOAnchorPosでアニメーションを行う
        card.mRt.DOAnchorPos(new Vector2(posX + 70, posY + 260), this.DEAL_CAED_TIME)
            // アニメーションが終了したら
            .OnComplete(() =>
            {
                // 生成したカードオブジェクトを保存する
                this.CardList.Add(card);

                // 生成するカードデータリストのインデックスを更新
                this.mIndex++;
                this.mWidthIdx++;

                // 生成インデックスがリストの最大値を迎えたら
                if (this.mIndex >= this.mRandomCardDataList.Count)
                {
                    // GridLayoutを有効にし、生成処理を終了する
                    this.GridLayout.enabled = true;
                    // アニメーション終了時の関数を宣言する
                    if (this.OnCardAnimeComp != null)
                    {
                        this.OnCardAnimeComp();
                    }
                }
                else
                {
                    // GridLayoutの折り返し地点に来たら
                    if (this.mIndex % this.GridLayout.constraintCount == 0)
                    {
                        // 高さの生成箇所を更新
                        this.mHelgthIdx++;
                        this.mWidthIdx = 0;
                    }
                    // アニメーション処理を再帰処理する
                    this.mSetDealCardAnime();
                }
            });
    }

    /// <summary>
    /// 取得していないカードを背面にする
    /// </summary>
    public void HideCardList(List<int> containCardIdList)
    {

        foreach (var _card in this.CardList)
        {

            // 既に獲得したカードIDの場合、非表示にする
            if (containCardIdList.Contains(_card.Id))
            {

                // カードを非表示にする
                _card.SetInvisible();
            }
            // 獲得していないカードは裏面表示にする
            else if (_card.IsSelected)
            {

                // カードを裏面表示にする
                _card.SetHide();
            }
        }
    }

}