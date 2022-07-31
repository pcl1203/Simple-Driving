using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
   [SerializeField] private float speed = 10f;
   [SerializeField] private float speedGainPerSecond = 0.20f;
   [SerializeField] private float turnSpeed = 200f;

   private int steerValue = 0;

    // Update is called once per frame
    void Update()
    {
      // Time.deltaTime to have the same movement with slower phones

      speed += speedGainPerSecond * Time.deltaTime;

      transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

      transform.Translate(Vector3.forward * speed * Time.deltaTime);


    }
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Obstacle")) {
         SceneManager.LoadScene("Scene_MainMenu"); // can be index depending on the arangement on the scenes
      }
   }
   public void Steer(int value) {
      steerValue = value;
   }
}
