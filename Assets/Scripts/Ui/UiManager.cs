using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countEnemy;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI cointText;
    [SerializeField] private TextMeshProUGUI heartText;
    [SerializeField] private GameObject uiWin;
    [SerializeField] private GameObject uiLose;
    [SerializeField] private Button btnReloadWin;
    [SerializeField] private Button btnReloadLose;
    private Wave wave;

    private float previousEnemy;
    private int previousWave;
    private int previousCoint;
    private int previousHeart;

    private void Start()
    {
        UpdateUI();
        wave = LevelData.Ins.lsLevel.Find(r => r.level == GameManager.Ins.currentWave);
        btnReloadWin.onClick.AddListener(ReloadLevel);
        btnReloadLose.onClick.AddListener(ReloadLevel);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene("Home");
    }

    private void Update()
    {
        if (GameManager.Ins.enemy != previousEnemy || GameManager.Ins.currentWave != previousWave ||
            GameManager.Ins.coint != previousCoint ||
            GameManager.Ins.heart != previousHeart)
        {
            UpdateUI();
            UpdateStateGame();
            // Cập nhật giá trị trước đó
            previousEnemy = GameManager.Ins.enemy;
            previousWave = GameManager.Ins.currentWave;
            previousCoint = GameManager.Ins.coint;
            previousHeart = GameManager.Ins.heart;
        }
    }

    private void UpdateStateGame()
    {
        if (GameManager.Ins.enemy <= 0)
        {
            if (GameManager.Ins.currentWave == wave.lsWave.Count && GameManager.Ins.enemy == 0)
            {
                GameManager.Ins.currentWave = wave.lsWave.Count;
                uiWin.SetActive(true);
                return;
            }
            GameManager.Ins.currentWave++;
        }

        if (GameManager.Ins.heart <= 0)
        {
            GameManager.Ins.heart = 0;
            uiLose.SetActive(true);
        }
    }

    private void UpdateUI()
    {
        countEnemy.text = GameManager.Ins.enemy.ToString();
        currentWaveText.text = GameManager.Ins.currentWave.ToString();
        cointText.text = GameManager.Ins.coint.ToString();
        heartText.text = GameManager.Ins.heart.ToString();
    }
}