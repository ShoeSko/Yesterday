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
        
        if(timer > 3)
            Destroy(gameObject);
    }
    public void EggCollected()
    {
        GetPoints = true;
        Destroy(gameObject);//Tempt simple solution, destroy the egg on press.
    }

    private void OnDestroy()
    {
        if (GetPoints == true)
            EggCollectionSpawning.score++;
    }
}
