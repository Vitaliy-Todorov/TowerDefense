using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _restartMenu;

    private void OnDestroy()
    {
        Time.timeScale = 0;
        if(_restartMenu != null)
            _restartMenu.SetActive(true);
    }
}