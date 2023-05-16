using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject savePlayerNameCanvas;
    public static bool isGameFinished = false; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //pause game
            Time.timeScale = 0f;
            isGameFinished = true;
            savePlayerNameCanvas.SetActive(true);

        }
    }

    private void Awake()
    {
        isGameFinished= false;
    }
}
