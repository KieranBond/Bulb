using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField]
    private float m_scoreIncrement = 0.2f;

    [SerializeField]
    private Text m_scoreText;

    private string m_scorePrefix;

    private float m_currentScore = 0;

	// Use this for initialization
	void Start ()
    {
        if (m_scorePrefix == null)
            m_scorePrefix = m_scoreText.text + "";
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_currentScore += (Time.deltaTime * m_scoreIncrement);

        m_scoreText.text = m_scorePrefix + (int)m_currentScore;
	}
}
