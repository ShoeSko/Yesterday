using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    static public bool s_youWon; //Use the s_ to make static variable easier to find
    [SerializeField] private GameObject youWin;

    private void Start()
    {
        youWin.SetActive(false); //In case it was left on.
    }
    private void Update()
    {
        SearchForWinCondition();
    }
    private void SearchForWinCondition()
    {
        if (s_youWon)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                Win();
            }
        }
    }

    private void Win()
    {
        print("Victory");
        s_youWon = false;
        youWin.SetActive(true);
        Time.timeScale = 0;
    }

    public void NextRound()
    {
        Time.timeScale = 1;
        NewCardHandScript.Stage++;
        Debug.Log(NewCardHandScript.Stage);

        if (NewCardHandScript.Stage == 4)
            SceneManager.LoadScene("MainMenu");
    }
}