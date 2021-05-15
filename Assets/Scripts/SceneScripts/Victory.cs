using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    static public bool s_youWon; //Use the s_ to make static variable easier to find
    [SerializeField] private GameObject youWin;
    public GameObject emote;

    private void Start()
    {
        s_youWon = false; //Makes sure that if this loosing does not cause you to win automatically when returning to core game. ("Bugfix")
        youWin.SetActive(false); //In case it was left on.
    }
    private void Update()
    {
        if(MinigameSceneScript.Tutorial == false)
            SearchForWinCondition();
    }
    private void SearchForWinCondition()
    {
        if (s_youWon)
        {
            print("Trying to win");
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) //As everything is in one scene, we are forced to have the boss hands in the equation.
            {
                Win();
            }
        }
    }

    public void Win()
    {
        print("Victory");
        s_youWon = false;
        youWin.SetActive(true);
        Time.timeScale = 0;
        emote.GetComponent<Emotes>().WonGame();
    }

    //public void NextRound()
    //{
    //    Time.timeScale = 1;
    //    NewCardHandScript.Stage++;
    //    Debug.Log(NewCardHandScript.Stage);

    //    if (NewCardHandScript.Stage == 4)
    //        SceneManager.LoadScene("MainMenu");
    //}
}
