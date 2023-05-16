using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public float waitTime;
    public string sceneName;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            if (sceneName == "MainMenu") 
            {
                PlayerPrefs.SetInt("IsScoreBoardActive", 1);
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
            
        }
    }
}
