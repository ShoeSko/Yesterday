using UnityEngine.SceneManagement;
using UnityEngine;

public class SkipToNextScene : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("MainMenu"); //This script does one thing. Loads the Menu after the very first scene
    }
}
