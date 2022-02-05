using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player;
    
    [Header("Pause")]
    public GameObject pausePanel;
    public bool needUpdatePath;

    [Header("Reload")]
    public Animator transition;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #region ReloadScene

    public void ReloadCurrentScene()
    {
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    #endregion

    #region Pause

    public void PauseGame()
    {
        StartCoroutine(Pause());
    }

    private IEnumerator Pause()
    {
        needUpdatePath = false;
        pausePanel.SetActive(!pausePanel.activeSelf);
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
    }
    
    public void ContinueGame()
    {
        needUpdatePath = true;
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = 1f;
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    #endregion
    
    public void SpawnNewPlayer(Vector3 spawnPosition)
    {
        StartCoroutine(SpawnPlayer(spawnPosition));
    }

    private IEnumerator SpawnPlayer(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(1.5f);
        player.transform.position = spawnPosition;
        needUpdatePath = true;
        player.gameObject.SetActive(true);
    }
}
