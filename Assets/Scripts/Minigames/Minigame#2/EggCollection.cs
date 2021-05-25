using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollection : MonoBehaviour
{
    private bool GetPoints = false;
    private float timer = 0;
    public GameObject eggAudio;
    public AudioSource getEgg;

    private EggCollectionSpawning coreSystem;

    public void Start()
    {
        coreSystem = FindObjectOfType<EggCollectionSpawning>();
        eggAudio = GameObject.Find("getEggSound");
        getEgg = eggAudio.GetComponent<AudioSource>();
    }

    public void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > 1.5f)//destroy after 2 second
            Destroy(gameObject);
    }
    public void EggCollected()
    {
        getEgg.Play();
        GetPoints = true;   //counts the eggs towards the total goal
        coreSystem.EggCollected();
        Destroy(gameObject);//Tempt simple solution, destroy the egg on press.
    }

    private void OnDestroy()
    {
        if (GetPoints == true)//the egg won't give points when disappearing
            EggCollectionSpawning.score++;
        else { coreSystem.EggLost(); }

        EggCollectionSpawning.eggsSpawned++;
    }
}
