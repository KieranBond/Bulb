using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup m_scoreCanvasGroup;

    [SerializeField]
    private float m_scoreIncrement = 0.2f;

    [SerializeField]
    private Text m_scoreText;

    private string m_scorePrefix;

    private float m_currentScore = 0;

    private bool m_addToScore = false;

    private static string m_scoreKey = "PlayerScore";

	// Use this for initialization
	void Start ()
    {
        if (m_scorePrefix != null)
            m_scorePrefix = m_scoreText.text + "";

        if (m_scoreCanvasGroup != null)
        {
            m_addToScore = true;
            ShowScore(2f);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_addToScore)
        {
            m_currentScore += (Time.deltaTime * m_scoreIncrement);

            m_scoreText.text = m_scorePrefix + (int) m_currentScore;
        }
	}

    public void UpdateHighScore()
    {
        m_addToScore = false;

        int highscore = GetHighScore();
        int thisScore = GetCurrentScore();

        if(thisScore > highscore)
        {
            SaveNewScore(thisScore);
        }
    }

    public void HideScore(float fadeTime = 0f)
    {
        m_scoreCanvasGroup.DOFade(0f, fadeTime);
    }

    public void ShowScore(float fadeTime = 0f)
    {
        m_scoreCanvasGroup.DOFade(1f, fadeTime);
    }

    private void SaveNewScore(int thisScore)
    {
        PlayerPrefs.SetInt(m_scoreKey, thisScore);
        PlayerPrefs.Save();
    }

    public int GetCurrentScore()
    {
        return (int)m_currentScore;
    }

    public int GetHighScore()
    {
        int highscore = 0;
        if (!PlayerPrefs.HasKey(m_scoreKey))
        {
            //Doesn't have key, we'll just return 0.
        }
        else
        {
            return PlayerPrefs.GetInt(m_scoreKey);
        }

        return highscore;
    }
}
