using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
