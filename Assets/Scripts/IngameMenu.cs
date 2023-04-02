using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class IngameMenu : MonoBehaviour
{
    [SerializeReference] public GameObject pauseMenu;
    [SerializeReference] public GameObject deathMenu;
    [SerializeField] public GameObject scoreText;
    [SerializeReference] public Player player;
    public static bool isPaused = false;
    bool reloading = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        deathMenu.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading) return;
        player.GetComponent<PlayerMovement>().enabled = !isPaused;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "SCORE " + Player.score;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePaused();
        }
        if (!isPaused && player.isAlive) {
            Image image = deathMenu.GetComponent<Image>();
            float a = image.color.a;
            deathMenu.SetActive(a != 0);
            if (player.lightCount == 0)
            {
                a += Time.deltaTime * 0.25f;
            }
            else {
                a -= Time.deltaTime * 0.25f;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Clamp01(a));
        }
        if (!player.isAlive)
        {
            deathMenu.transform.GetChild(0).gameObject.SetActive(true);
            TogglePaused();
        }
    }
    public void TogglePaused() {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        if(!player.isAlive) pauseMenu.SetActive(false);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void ToMainMenu() {
        Time.timeScale = 1;
        isPaused = false;
        reloading = true;
        SceneManager.LoadScene(0);
    }

    public void PlayAgain() {
        Time.timeScale = 1;
        isPaused = false;
        reloading = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
