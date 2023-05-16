using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NameManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public static string playerName;

    public void save()
    {
        if (playerNameInput != null)
        {
            playerName = playerNameInput.text;
            gameObject.SetActive(false);
            SceneManager.LoadScene("Ending");
        }
    }

}
