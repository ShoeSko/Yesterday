using UnityEngine;

public class ManaAnimationInteraction : MonoBehaviour
{
    [SerializeField] private NewCardHandScript hand;
    private int nrCard;

    public void CardUsed(int cardNr) //Initiate when card animation starts
    {
        nrCard = cardNr;
        if (nrCard == 1)
        {
            hand.isManaCardGlow1 = false;
        }
        else if(nrCard == 2)
        {
            hand.isManaCardGlow2 = false;
        }
        else if(nrCard == 3)
        {
            hand.isManaCardGlow3 = false;
        }
        else if(nrCard == 4)
        {
            hand.isManaCardGlow4 = false;
        }
        else if(nrCard == 5)
        {
            hand.isManaCardGlow5 = false;
        }
    }

    public void CardDrawn(int cardNr) //Initiate when card animation ends
    {
        nrCard = cardNr;
        if (nrCard == 1)
        {
            hand.isManaCardGlow1 = true;
        }
        else if (nrCard == 2)
        {
            hand.isManaCardGlow2 = true;
        }
        else if (nrCard == 3)
        {
            hand.isManaCardGlow3 = true;
        }
        else if (nrCard == 4)
        {
            hand.isManaCardGlow4 = true;
        }
        else if (nrCard == 5)
        {
            hand.isManaCardGlow5 = true;
        }
    }
}
