using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;
[System.Serializable]
public class SaveData {
    public Dictionary<string, int> scores = new();
}

public class IngameMenu : MonoBehaviour
{
    [SerializeReference] public GameObject pauseMenu;
    [SerializeReference] public GameObject deathMenu;
    [SerializeField] public GameObject scoreText;
    [SerializeReference] public Player player;
    [SerializeReference] public TMPro.TextMeshProUGUI myScore;
    [SerializeReference] public TMPro.TextMeshProUGUI otherScores;
    [SerializeReference] public TMPro.TMP_InputField textBox;
    
    public static bool isPaused = false;
    bool reloading = false;
    SaveData saveData;
    public string playerName;

    string LoadFile(string name) {
        string fullPath = Path.Combine(Application.persistentDataPath, name);
        try
        {
            return File.ReadAllText(fullPath);
        }
        catch (Exception)
        {
            return "{}";
        }
    }
    void SaveFile(string name, string contents) {
        var fullPath = Path.Combine(Application.persistentDataPath, name);

        try
        {
            File.WriteAllText(fullPath, contents);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
        }
    }
    void Start()
    {
        pauseMenu.SetActive(false);
        deathMenu.transform.GetChild(0).gameObject.SetActive(false);
        try
        {
            saveData = JsonConvert.DeserializeObject<SaveData>(LoadFile("SaveData.dat"));
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read with exception {e}");
            saveData = new SaveData();
        }
    }

    public void SetName(string s) {
        playerName = s;
    }

    public void SaveHighScore() {
        if (playerName == null) return;
        if (playerName.Length == 0) return;
        textBox.interactable = false;
        if (saveData.scores.ContainsKey(playerName))
        {
            int oldScore = saveData.scores[playerName];
            if (Player.score <= oldScore) return;
        }
        saveData.scores[playerName] = Player.score;
        SaveFile("SaveData.dat", JsonConvert.SerializeObject(saveData));
        RefreshLeaderBoard();
    }

    void RefreshLeaderBoard() {
        var list = saveData.scores.OrderByDescending(pair => pair.Value).ToList();
        string thing = "";
        for (int i = 0; i < Mathf.Min(7, list.Count()); i++) {
            thing += list[i].Key + " " + list[i].Value + "\n";
        }
        otherScores.text = thing;
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
            RefreshLeaderBoard();
            myScore.text = "YOU SCORED " + Player.score;
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
