using UnityEngine;

public class CreditsSecret : MonoBehaviour
{
    [SerializeField] private GameObject creditButton;
    private int timesButtonIsPressed;

    public void ReleaseThatSecret()
    {
        if(timesButtonIsPressed == 9)
        {
            creditButton.SetActive(true);
        }
        else { timesButtonIsPressed++; }

        print("Secret button was pressed");
    }
}
