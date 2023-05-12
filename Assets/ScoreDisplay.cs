using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;
    public Text highScore;// điểm cao nhất trong các lần chơi đưa ra text
    public Text Highbonus;
    [HideInInspector] public int score;// điểm trong trận 
    public static int scoreHigh;//điểm cao nhất trong các lần chơi
    public static int bonusHigh;// tiền thưởng cao 
    private int bonus;// điểm số hiện tại 

    private void Awake()
    {
        scoreText = GameObject.FindGameObjectWithTag("scoredisplay").GetComponent<Text>();
    }
    void OnEnable()

    {
        // biến lưu điểm 
        if (PlayerPrefs.HasKey("HighScore"))
        {
            scoreHigh = PlayerPrefs.GetInt("HighScore");
        }
        if (PlayerPrefs.HasKey("bonuss"))
        {
            bonusHigh = PlayerPrefs.GetInt("bonuss");
        }

    }
    private void Update()
    {
        // nếu diểm trong trận lớn hơn điểm cao nhát thì lưu ddierm cao nhát 
        if (score > scoreHigh)
        {
            scoreHigh = score;
            PlayerPrefs.SetInt("HighScore", scoreHigh);
        }


        UpdateHandle();
    }

    public void bonusHi()
    {
        bonus += Random.Range(1, 5);
        bonusHigh += bonus; // Cộng giá trị mới vào giá trị hiện tại của bonusHigh
        PlayerPrefs.SetInt("bonuss", bonusHigh);
        
    }
    void UpdateHandle()
    {
        scoreText.text = score.ToString();
        highScore.text = scoreHigh.ToString();
        Highbonus.text = bonusHigh.ToString();
    }
}
