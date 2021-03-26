using UnityEngine;

public class AnimalPens : MonoBehaviour
{
    //public LayerMask penLayer;
    private GameObject currentAnimal;
    public string penLayerName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentAnimal = collision.gameObject; //Grabs the gameobject for easier refrence.
        if (currentAnimal.layer == LayerMask.NameToLayer(penLayerName)) //If the layer collided with is the same as the animal layer set.
        {
            currentAnimal.layer = 2; //Sets the object to not be interactable by mouse/touch
            currentAnimal.transform.position = transform.position; //Can be set later to make it look good when placed correctly
            AnimalVictory._animalPensFilled++; //Increases the finished Animal Pens list.
        }
    }
}