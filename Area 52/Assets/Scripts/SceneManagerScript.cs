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
        SaveClass saveClass = (SaveClass)SerializationManager.Load(Application.persistentDataPath + "/saves/PlayerData.save");
        if (saveClass != null)
        {
            Debug.Log(saveClass.profile.ProgressStageNumber);
            SceneManager.LoadScene(saveClass.profile.ProgressStageNumber);
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
