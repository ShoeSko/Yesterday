using UnityEngine;

public class LoadingSceneScript : MonoBehaviour
{
    [SerializeField] private LevelTransitionSystem transitionSystem;
    [SerializeField] private int numberOfLoops;
    private Animator LoadingAnimator;

    private void Awake()
    {
        LoadingAnimator = GetComponent<Animator>();
    }

    public void LoadTheNextScene()
    {
        if (numberOfLoops == 1)
        {
            transitionSystem.LoadNextLevelFromCoreGame();
        }
        else
        {
            LoadingAnimator.SetTrigger("Loop");
            numberOfLoops--;
        }
    }
}
