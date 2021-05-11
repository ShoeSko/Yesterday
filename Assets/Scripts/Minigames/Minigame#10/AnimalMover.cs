using UnityEngine;

public class AnimalMover : MonoBehaviour
{
    private Camera mainCamera; //Camera refrence
    private float CameraZDistance; //Variable to determine the distance in the z axis
    private bool grabbed; //Is the object grabber?
    private Collider2D colliderOfAnimal;

    //private Transform animalTransform;
    private Vector3 originalPos;
    private Vector3 fakePos;

    private void Start()
    {
        mainCamera = Camera.main;
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z; //Sets the Z distance to the position value of the camera world point.
    }

    private void OnMouseDown()
    {
        OnMouseDrag(); //On Mouse/Touch down, use the On Mouse Drag
        grabbed = true; //It is now grabbed
        MovingAnimal(); //Activate the Moving Animal
    }

    private void OnMouseUp()
    {
        grabbed = false; //No Longer grabbed
        MovingAnimal();//Use other half of Moving Animal
    }

    private void OnMouseDrag()
    {
#if Unity_Android
                Vector3 ScreenPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y,CameraZDistance); //Gets the x & y position of the touch
                Vector3 NewWorldPosition = mainCamera.ScreenToWorldPoint(ScreenPosition); //Sets the Newworld position to be the screenposition
                transform.position = NewWorldPosition; //Moves the transform of the object holding the script to the newWorldPosition
#else
        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance); //Gets the x & Y position of the mouse
        Vector3 NewWorldPosition = mainCamera.ScreenToWorldPoint(ScreenPosition); //Sets the Newworld position to be the screenposition
        transform.position = NewWorldPosition; //Moves the transform of the object holding the script to the newWorldPosition
#endif
    }

    private void MovingAnimal() //This makes sure that when moving the animals, they wont trigger placements
    {
        if (grabbed)
        {
            colliderOfAnimal = GetComponent<Collider2D>(); //Get the collider
            colliderOfAnimal.enabled = false; //Can't collide if there is not collider
        }
        else
        {
            originalPos = gameObject.transform.position; //Where are we
            colliderOfAnimal.enabled = true; //Collide once more
            gameObject.transform.position = fakePos; //Send to god knows where
            gameObject.transform.position = originalPos; //Return before anyone notices, but makes collider able to trigger.
        }
    }
}
