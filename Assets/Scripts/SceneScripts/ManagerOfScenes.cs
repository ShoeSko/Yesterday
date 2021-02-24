using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "SceneManager", menuName = "ButtonWizard")]
public class ManagerOfScenes : ScriptableObject
{
    public void ChangeScene(string sceneName) //Use to change scenes
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single); //Refrences scene by name with string.
    }    
    public void Activate(GameObject gameObject) //Can turn game objects on.
    {
        gameObject.SetActive(true); //Sets gameobject to active
    }
    public void UnActivate(GameObject gameObject) //Can turn game objects off.
    {
        gameObject.SetActive(false); //Sets gameobject to UnActive
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            UnityEditor.EditorApplication.isPlaying = false; //Quits game, both in build and editor.
        }
#else
        Application.Quit();
#endif
    }
}