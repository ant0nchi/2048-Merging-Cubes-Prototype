using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Canvas gameOverCanvas;
    public Text scoreText;
    public static ColorPalette currentPalette { get; protected set; }
    public static List<int> availableValues;
    public InterAd interAd;

    string currentPaletteName = "StandartPalette";
    private int score;
    private int counter = 0;
    private int maxValue = 2;

    void Awake()
    {
        Time.timeScale = 1f;
        EventManager.OnCubeMerged += UpdateScore;
        EventManager.OnLineCrossed += FinishGame;
        EventManager.OnCubePushed += UpdateCounter;

        availableValues = new List<int>() { 2 };
        LoadPalette(currentPaletteName);
        gameOverCanvas.enabled = false;
    }

    void LoadPalette(string paletteName)
    {
        string path = Application.dataPath + "/" + paletteName + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            currentPalette = JsonUtility.FromJson<ColorPalette>(json);
        }
    }

    void UpdateScore(int value)
    {
        if (value > maxValue && value <= 64)
        {
            availableValues.Add(value);
            maxValue = value;
        }
        score += value;
        scoreText.text = score.ToString();
    }

    void UpdateCounter()
    {
        counter++;
        if (counter % 20 == 0)
        {
            interAd.ShowAd();
        }
    }

    void FinishGame()
    {
        Time.timeScale = 0f;
        gameOverCanvas.enabled = true;
    }

    public void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void OnDestroy()
    {
        EventManager.OnCubeMerged -= UpdateScore;
        EventManager.OnLineCrossed -= FinishGame;
        EventManager.OnCubePushed -= UpdateCounter;
    }
}