using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAllGame : MonoBehaviour {

    public static MusicAllGame Instance;//Khai báo biến trung gian kiểu static
    public static bool isMusic = true;//kiểm tra xem người chơi đã bật hay tắt âm nhạc trong game.
    public AudioSource aS;
    public AudioClip[] aClip;// Mảng chứa tất cả các AudioClip (âm thanh) cần sử dụng trong game. 

    void Awake()
    {
        // Kiểm tra xem đã có thể truy xuất được đến MusicAllGame Instance chưa
        if (Instance == null)
        {
            // Nếu chưa thì sử dụng DontDestroyOnLoad để giữ game object này trong suốt game
            DontDestroyOnLoad(gameObject);
            Instance = this;//gán lớp cho biến instance làm trung gian để truy xuất
        }
        else if (Instance != this)
        {
            // Nếu đã có Instance rồi thì destroy game object này đi
            Destroy(gameObject);
        }
        // Kiểm tra xem người chơi đã bật/tắt âm nhạc trong game chưa
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            isMusic = true;
        }
        else { isMusic = false; }
    }
    // Hàm này để phát âm nhạc trong game
    public void PlayMusicGame(int music)
    {
        if (isMusic)// Nếu âm nhạc được bật thì mới phát
        {
            aS.Stop();// Dừng âm nhạc đang phát nếu có
            aS.PlayOneShot(aClip[music]);// Phát âm nhạc tại index music
        }
    }
}
