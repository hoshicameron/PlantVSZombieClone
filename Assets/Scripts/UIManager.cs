using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject lastSelectedObject=null;
    [SerializeField] private TextMeshProUGUI starText;
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject pauseMenuUI;

    private void OnEnable()
    {
        EventHandler.UpdateStarsEvent+=OnUpdateStarsEvent;
        EventHandler.EndGameEvent+=OnEndGameEvent;

        endGameUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }
    private void OnDisable()
    {
        EventHandler.UpdateStarsEvent-=OnUpdateStarsEvent;
        EventHandler.EndGameEvent-=OnEndGameEvent;
    }

    private void OnEndGameEvent()
    {
        Time.timeScale = 0;
        endGameUI.SetActive(true);
        GameManager.Instance.IsGameEnded = true;
    }

    private void OnUpdateStarsEvent(int amount)
    {
        starText.SetText(amount.ToString());
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {

            if (lastSelectedObject != null)
            {
                if(lastSelectedObject.gameObject.activeSelf && lastSelectedObject.GetComponent<Button>()!=null
                                                            && lastSelectedObject.GetComponent<Button>().interactable)
                {
                    EventSystem.current.SetSelectedGameObject(lastSelectedObject);
                } else
                {
                    print("Set last selected ");
                    lastSelectedObject = EventSystem.current.currentSelectedGameObject;
                }
            }

        } else
        {
            lastSelectedObject=EventSystem.current.currentSelectedGameObject;
        }
    }


    public void ShowPauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        Time.timeScale = pauseMenuUI.activeSelf ? 0 : 1;
        GameManager.Instance.IsGamePaused = !GameManager.Instance.IsGamePaused;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        GameManager.Instance.IsGamePaused = false;
        GameManager.Instance.IsGameEnded = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);


    }

    public void LoadLevel(string sceneName)
    {
        Time.timeScale = 1;
        GameManager.Instance.IsGamePaused = false;
        GameManager.Instance.IsGameEnded = false;
        SceneManager.LoadSceneAsync(sceneName);
    }
}
