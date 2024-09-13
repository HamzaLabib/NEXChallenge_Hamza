using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Fields
    [SerializeField] TextMeshProUGUI result;
    [SerializeField] EnemyControl enemy;
    [SerializeField] GameObject player;
    [SerializeField] AudioSource backgroundAudio;
    [SerializeField] AudioSource loseAudio;
    [SerializeField] AudioSource winAudio;
    #endregion

    #region Methods
    private void Update()
    {
        if (!enemy)
        {
            PlayerWin();
            HandleAudioPlayback(winAudio);
        }
        if (!player)
        {
            PlayerLose();
            HandleAudioPlayback(loseAudio);
        }
    }

    #region Scene Management
    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    //Open Game Scene
    public void GameplayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(2);

    }

    // Close The Game
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Handle Text Message
    private void PlayerWin()
    {
        if (result != null)
        {
            result.text = "You Won!";
            FreezeGame();
        }
    }

    private void PlayerLose()
    {
        if (result != null)
            result.text = "Loser Haha!";
    }
    #endregion

    #region Audio Management

    void HandleAudioPlayback(AudioSource audio)
    {
        if (backgroundAudio)
            backgroundAudio.Stop();
        if (audio)
            audio.gameObject.SetActive(true);
    }
    #endregion

    //To Freeze Game when menu popup!
    void FreezeGame()
    {
        player.GetComponent<PlayerAttack>().enabled = false;
        player.GetComponent<PlayerControl>().enabled = false;
    }
    #endregion
}
