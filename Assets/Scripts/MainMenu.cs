using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private TMP_Text highScoreText;
   [SerializeField] private TMP_Text previousScoreText;
   private void Start()
   {
      int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
      highScoreText.text = $"High Score: {highScore}";

      int prevScore = PlayerPrefs.GetInt(ScoreSystem.PreviusScoreKey, 0);
      if (prevScore == 0) previousScoreText.text = "";
      else previousScoreText.text = $"Last Score: {prevScore}";
   }
   public void Play()
   {
      SceneManager.LoadScene("Scene_Game");
   }
}
