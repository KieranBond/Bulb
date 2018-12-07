using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup m_menuCanvasGroup;
    [SerializeField]
    private Text m_highscoreText;
    [SerializeField]
    private ScoreTracker m_scoreTracker;

    [SerializeField]
    private Button m_startButton;

	// Use this for initialization
	void Start ()
    {
        //Saving this from being destroyed on Scene load.
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(FindObjectOfType<Camera>());
        //DontDestroyOnLoad(FindObjectOfType<Light>());

        m_menuCanvasGroup.DOFade(1f, 0f);//Activate the UI.

        m_startButton.onClick.AddListener(BeginGame);
        m_highscoreText.text = "" + m_scoreTracker.GetHighScore();
        SceneManager.sceneLoaded += ( e, o ) => { ActivateSpawners(); };

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        //ActivateSpawners();
    }

    private void BeginGame()
    {
        m_menuCanvasGroup.interactable = false;
        m_menuCanvasGroup.DOFade(0f, 1f).OnComplete(()=>
        {
            //SceneManager.LoadScene(1);
            ActivateSpawners();
        });
    }

    private void ActivateSpawners()
    {
        GameObject[] activators = GameObject.FindGameObjectsWithTag("Activator");
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");

        foreach (GameObject obj in activators)
        {
            if (obj.GetComponent<Activator>())
                obj.GetComponent<Activator>().Activate();
        }
        foreach (GameObject obj in spawners)
        {
            if (obj.GetComponent<Activator>())
                obj.GetComponent<Activator>().Activate();
        }
    }
}
