using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartMenu : MonoBehaviour
{
    [SerializeField] 
    private Button _restartInGame;
    [SerializeField] 
    private Button _restart;
    [SerializeField]
    private Button _exet;

    private string _scenename = "SampleScene";

    void Start()
    {
        Time.timeScale = 1;
        _restartInGame.onClick.AddListener(Restart);
        _restart.onClick.AddListener(Restart);
        _exet.onClick.AddListener(Exet);
    }
    
    private void Restart()
    {
        SceneManager.LoadScene(_scenename);
        Time.timeScale = 1;
    }

    private void Exet() => 
        Application.Quit();
}
