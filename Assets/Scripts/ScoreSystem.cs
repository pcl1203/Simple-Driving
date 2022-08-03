using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
   [SerializeField] private TMP_Text scoreText;
   [SerializeField] private float scoreMultiplier;
   public const string HighScoreKey = "HighScore";
   public const string PreviousScoreKey = "PrevScore";
   private float score;
    // Update is called once per frame
    void Update()
    {
      score += Time.deltaTime * scoreMultiplier;
      scoreText.text = Mathf.FloorToInt(score).ToString();
    }

   private void OnDestroy()
   {
      int currentHighscore = PlayerPrefs.GetInt(HighScoreKey, 0); //
      if (score > currentHighscore) PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
      PlayerPrefs.SetInt(PreviousScoreKey, Mathf.FloorToInt(score));
   }
}
