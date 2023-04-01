using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeReference] public GameObject pauseMenu;
    [SerializeField] public GameObject scoreText;
    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "SCORE " + Player.score;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePaused();
        }
    }
    public void TogglePaused() {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void ToMainMenu() {
        TogglePaused();
        SceneManager.LoadScene(0);
    }
}
