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
        if (!isPaused) {
            Image image = deathMenu.GetComponent<Image>();
            float a = image.color.a;
            if (player.lightCount == 0)
            {
                a += Time.deltaTime * 0.25f;
            }
            else {
                a -= Time.deltaTime * 0.25f;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Clamp01(a));
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
