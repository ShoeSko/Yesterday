using UnityEngine;

public class AnimalPens : MonoBehaviour
{
    //public LayerMask penLayer;
    private GameObject currentAnimal;
    public string penLayerName;

    public AudioSource PigSFX;
    public AudioSource ChickenSFX;
    public AudioSource CowSFX;
    public AudioSource SheepSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentAnimal = collision.gameObject; //Grabs the gameobject for easier refrence.
        if (currentAnimal.layer == LayerMask.NameToLayer(penLayerName)) //If the layer collided with is the same as the animal layer set.
        {
            currentAnimal.layer = 2; //Sets the object to not be interactable by mouse/touch
            currentAnimal.transform.position = transform.position; //Can be set later to make it look good when placed correctly
            AnimalVictory._animalPensFilled++; //Increases the finished Animal Pens list.
            currentAnimal.GetComponent<Collider2D>().enabled = false;
            currentAnimal.GetComponent<SpriteRenderer>().sortingOrder = 2;

            if (currentAnimal.name == "Pig")
                PigSFX.Play();
            else if (currentAnimal.name == "Chicken")
                ChickenSFX.Play();
            else if (currentAnimal.name == "Cow")
                CowSFX.Play();
            else if (currentAnimal.name == "Sheep")
                SheepSFX.Play();
            gameObject.SetActive(false);
        }
    }
}