using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollection : MonoBehaviour
{
    private bool GetPoints = false;
    private float timer = 0;

    public void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > 1.5f)//destroy after 2 second
            Destroy(gameObject);
    }
    public void EggCollected()
    {
        GetPoints = true;   //counts the eggs towards the total goal
        Destroy(gameObject);//Tempt simple solution, destroy the egg on press.
    }

    private void OnDestroy()
    {
        if (GetPoints == true)//the egg won't give points when disappearing
            EggCollectionSpawning.score++;

        EggCollectionSpawning.eggsSpawned++;
    }
}
