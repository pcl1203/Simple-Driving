using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private TMP_Text highScoreText;
   [SerializeField] private TMP_Text previousScoreText;
   [SerializeField] private TMP_Text energyText;
   [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
   [SerializeField] private IOSNotificationHandler iosNotificationHandler;
   [SerializeField] private Button playButton;



   [SerializeField] private int maxEnergy;
   [SerializeField] private int energyRechargeDuration;

   private int energy;

   private const string EnergyKey = "Energy";
   private const string EnergyReadyKey = "EnergyReady";

   private void Start()
   {
      OnApplicationFocus(true);
   }
   private void OnApplicationFocus(bool hasFocus)
   {
      if(!hasFocus) return;
      CancelInvoke();

      int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
      highScoreText.text = $"High Score: {highScore}";

      int prevScore = PlayerPrefs.GetInt(ScoreSystem.PreviousScoreKey, 0);
      if (prevScore == 0) previousScoreText.text = "";
      else previousScoreText.text = $"Last Score: {prevScore}";

      energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
      if (energy == 0) { 
         string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
         if (energyReadyString == string.Empty) return;

         DateTime energyReady = DateTime.Parse(energyReadyString);

         if (DateTime.Now > energyReady) {
            energy = maxEnergy;
            PlayerPrefs.SetInt(EnergyKey, energy);
         }
         else {
            Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
         }
      }

      energyText.text = $"Play({energy})";
   }
   private void EnergyRecharged()
   {
      playButton.interactable = true;
      energy = maxEnergy;
      PlayerPrefs.SetInt(EnergyKey, energy);
      energyText.text = $"Play({energy})";

   }
   public void Play()
   {
      if (energy < 1) return;

      energy--;
      PlayerPrefs.SetInt(EnergyKey, energy);
      if (energy == 0) { 
         DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
         PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
         #if UNITY_ANDROID
         androidNotificationHandler.ScheduleNotification(energyReady);
         #elif UNITY_IOS
         iosNotificationHandler.ScheduleNotification(energyRechargeDuration);
         #endif
      }
      SceneManager.LoadScene("Scene_Game");
   }
}
