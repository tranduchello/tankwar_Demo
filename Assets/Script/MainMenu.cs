using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // được sử dụng để chuyển đổi về cảnh ban đầu của trò chơi
    public void StartMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //  được sử dụng để chuyển đến cảnh tiếp theo
    public void GuiDe()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //sử dụng để chuyển đến cảnh chơi game
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    //sử dụng để chuyển đến cảnh kết thúc 
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }
    //được sử dụng để chuyển đến cảnh kết thúc game khi người chơi chiến thắng
    public void GameClear()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
    }
    // được sử dụng để đóng trò chơi khi người chơi muốn thoát
    public void Quit()
    {
        Debug.Log("da thoat game");
        Application.Quit();
    }
}
