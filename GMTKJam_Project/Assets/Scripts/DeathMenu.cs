using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class DeathMenu : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public GameObject deathMenuUI;
    public float deathDelay = 1f;
    private float startTime;
    private float elapsedTime;

    public UnityEngine.UI.Text text;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.dead == true)
        {
            //wait 3 seconds before showing death menu
            StartCoroutine(ShowDeathMenuAfterDelay(deathDelay));
            UnityEngine.Debug.Log("Player has lived for " + elapsedTime + " seconds");
         
        }else{
            elapsedTime = Time.time - startTime;
        }

    }

    public void Restart()
    {
        //SceneManager.LoadScene("TestScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//reloads the current scene regardless of its name
    }

    public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }

    IEnumerator ShowDeathMenuAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        string t = elapsedTime.ToString("0.00"); 
        text.text = "Time Survived: " + t;
        // Now show the death menu
        deathMenuUI.SetActive(true);
    }




    //record how long player lived
}
