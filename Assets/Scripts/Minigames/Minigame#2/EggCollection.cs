using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollection : MonoBehaviour
{
    public void EggCollected()
    {
        Destroy(gameObject);//Tempt simple solution, destroy the egg on press.
    }

    private void OnDestroy()
    {
        //Add to score? 
    }

}
