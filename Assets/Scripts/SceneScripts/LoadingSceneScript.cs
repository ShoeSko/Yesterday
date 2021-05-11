using UnityEngine;

public class LoadingSceneScript : MonoBehaviour
{
    [SerializeField] private LevelTransitionSystem transitionSystem;

    public void LoadTheNextScene()
    {
        transitionSystem.LoadNextLevelFromCoreGame();
    }
}
