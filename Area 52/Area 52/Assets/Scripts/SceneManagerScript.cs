using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script manages game scenes
public class SceneManagerScript : MonoBehaviour
{
    public static void die()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void respawn()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
