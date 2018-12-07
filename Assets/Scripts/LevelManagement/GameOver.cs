using DG.Tweening;
using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private string m_playerTag;

    [SerializeField]
    private ScoreTracker m_scoreTracker;

    [Header("Canvas objects")]
    [SerializeField]
    private CanvasGroup m_uiCanvasGroup;
    [SerializeField]
    private Button m_restartButton;
    [SerializeField]
    private Text m_scoreText;
    [SerializeField]
    private Text m_highscoreText;

    //Ads.
    private InterstitialAd m_interAd;


    private void Start()
    {
        //Makes sure it's invisible and untouchable.
        m_uiCanvasGroup.interactable = false;
        m_uiCanvasGroup.DOFade(0f, 0f);

        RequestInterstitialAd interAd = this.gameObject.AddComponent<RequestInterstitialAd>();
        m_interAd = interAd.LoadAd();

        //Add the button action.
        m_restartButton.onClick.AddListener(() =>
        {
            RestartGame();
        });
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        //When the player enters

        if(collision.tag == m_playerTag)
        {
            //Trigger game over

            //Destroy the GO.
            Destroy(collision.gameObject);

            //Do whatever normally.
            TriggerGameOver();

            //For now, we're just gunna restart!
            //SceneManager.LoadScene(0);

        }
    }

    private void TriggerGameOver()
    {
        //Trigger an ad.
        RequestInterstitialAd interAd = GetComponent<RequestInterstitialAd>();
        m_interAd = interAd.m_ad;//This returns the Ad

        if(!interAd.m_ad.IsLoaded())
        {
            //Not loaded. Request.
            m_interAd = interAd.RequestInterstitial();
        }

        m_interAd.Show();
        m_scoreTracker.HideScore();

        //Clean up. 
        m_interAd.OnAdClosed += (o, e)=> 
        {
            ActivateUI();//Game Over UI.
        };
    }

    private void ActivateUI()
    {
        //Save score.
        m_scoreTracker.UpdateHighScore();
        int ourScore = m_scoreTracker.GetCurrentScore();
        int highscore = m_scoreTracker.GetHighScore();

        m_scoreText.text = ""+ourScore;
        m_highscoreText.text = ""+highscore;

        m_uiCanvasGroup.DOFade(1f, 1f).OnComplete(() =>
        {
            //Once the UI is visible, we can do stuff.
            m_uiCanvasGroup.interactable = true;
        });
    }

    private void RestartGame()
    {
        m_interAd.Destroy();
        Destroy(gameObject.GetComponent<RequestInterstitialAd>());

        m_uiCanvasGroup.interactable = false;

        //Restart game
        m_uiCanvasGroup.DOFade(0f, 0.5f).OnComplete(()=> 
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<StartMenu>().RestartGame();
        });
    }
}
