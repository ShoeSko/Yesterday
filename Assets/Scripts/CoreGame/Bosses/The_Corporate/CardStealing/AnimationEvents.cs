using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private GreedyOpportunityDeckStealing greedOrigin;

    [SerializeField] private Animator animationOfHand;
    [SerializeField] private Animator animationOfCardBeingStolen;

    public void AnimatedCardSteal()
    {
        animationOfCardBeingStolen.SetTrigger("Stolen");
        animationOfHand.SetTrigger("Grabbed");
    }

    public void CardIsNowStolen()
    {
        greedOrigin.isCardStolen = true;
    }
}
