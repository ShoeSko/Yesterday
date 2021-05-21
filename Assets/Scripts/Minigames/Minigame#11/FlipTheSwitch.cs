using UnityEngine;

public class FlipTheSwitch : MonoBehaviour
{
    private bool isFlipped;
    private float flipStrenght = -90;
    private Animator animator;

    private void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
    }

    private void OnMouseDown() //Is called with a mouseclick or touch on an object with collider. (Ignore raycast layer prevents this)
    {
        FlippingSwitch();
    }

    private void FlippingSwitch()
    {
        if (!isFlipped)
        {
            transform.localRotation = Quaternion.Euler(0, 0,flipStrenght); //Flips the object 90 degrees
            isFlipped = true;
            animator.SetTrigger("Wrong");
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0); //Returns the object to it's original rotation
            isFlipped = false;
        }
    }
}
