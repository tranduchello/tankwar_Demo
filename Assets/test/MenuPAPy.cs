using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuPAPy : MonoBehaviour
{
    [SerializeField] public GameObject Text;
    [SerializeField] public GameObject ButtonPlay;
    [SerializeField] public GameObject ButtonPause;
    [SerializeField] public GameObject Youwin;
    [SerializeField] public GameObject Exit;
    [SerializeField] public GameObject ScoreDiem;
    public int  maxyouwin;
    ScoreDisplay scoreDisplay;
    Enemyabc enemyabc;
    public void Start()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
        enemyabc = FindObjectOfType<Enemyabc>();
        maxyouwin = enemyabc.spawnCount;

    }
    public void Update()
    {
        if (scoreDisplay != null && enemyabc !=null)
        {
            if (scoreDisplay.score >= enemyabc.spawnCount)
            {
                YouWinn();
                StartCoroutine(Win());
            }

        }
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        Text.SetActive(false);
        Debug.Log("play");
        ButtonPlay.SetActive(false);
        ButtonPause.SetActive(true);
        Exit.SetActive(true);
        Youwin.SetActive(false);
        ScoreDiem.SetActive(true);
    }
    public void PauseGame()
    {
        Debug.Log("pause");
        Time.timeScale = 0;
        Text.SetActive(true);
        ButtonPlay.SetActive(true);
        ButtonPause.SetActive(false);
        Youwin.SetActive(false);
        Exit.SetActive(true);
        ScoreDiem.SetActive(true);
    }
    public void YouWinn()
    {
        Text.SetActive(false);
        ButtonPause.SetActive(false) ;
        ButtonPlay.SetActive(false);
        Youwin.SetActive(true);
        Exit.SetActive(false);
        ScoreDiem.SetActive(false);

    }
    IEnumerator Win()
    {
        yield return  new WaitForSeconds(7f);
        SceneManager.LoadScene("Start_Menu");
    }
  
  
}
