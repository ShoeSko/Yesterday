using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    static public bool s_youWon; //Use the s_ to make static variable easier to find
    [SerializeField] private GameObject youWin;
    public GameObject emote;
    public GameObject TutorialHand;
    private bool victoryIsSHown;
    [SerializeField] private Animator hamsterAnimator;

    private void Start()
    {
        s_youWon = false; //Makes sure that if this loosing does not cause you to win automatically when returning to core game. ("Bugfix")
        youWin.SetActive(false); //In case it was left on.
    }
    private void Update()
    {
        if(MinigameSceneScript.Tutorial == false)
            SearchForWinCondition();
        else//Tutorial stuff
        {
            if(TutorialHand.GetComponent<NewCardHandScript>().LookForEnemies == true)
            {
                if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
                {
                    TutorialHand.GetComponent<NewCardHandScript>().TutorialWin();
                }
            }
        }
    }
    private void SearchForWinCondition()
    {
        if (s_youWon)
        {
            print("Trying to win");
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) //As everything is in one scene, we are forced to have the boss hands in the equation.
            {
                if (!victoryIsSHown)
                {
                    Win();
                    victoryIsSHown = true;
                }
            }
        }
    }

    [ContextMenu("Actiave Win")]
    public void Win()
    {
        emote.GetComponent<Emotes>().WonGame();
        print("Victory");
        s_youWon = false;
        youWin.SetActive(true);
        hamsterAnimator.SetTrigger("Full");
        Time.timeScale = 0;
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
