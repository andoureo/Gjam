using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartStateManager : MonoBehaviour
{

    // ゲームの開始テキストの座標
    public RectTransform GameStartTextRt;

    public CanvasGroup canvasGroup;

    /// <summary>
    /// テキストの拡大アニメーション
    /// </summary>
    public void EnlarAnimation()
    {

        this.GameStartTextRt.DOScale(Vector3.one * 1.1f, 2.2f)
            .OnComplete(() =>
            {
                // テキストの縮小アニメーション
                this.mShrinkAnimation();
            });
    }

    /// <summary>
    /// テキストの縮小アニメーション
    /// </summary>
    private void mShrinkAnimation()
    {

        this.GameStartTextRt.DOScale(Vector3.one * 0.9f, 1.8f)
            .OnComplete(() =>
            {
                // テキストの縮小アニメーション
                this.EnlarAnimation();
            });
    }
}