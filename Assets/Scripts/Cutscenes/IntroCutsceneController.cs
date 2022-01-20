using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneController : MonoBehaviour
{
    public GameObject Fadebox;
    public GameObject FadeboxEnd;

    private float startDelay = 2;//Delay before the fade at the start

    private float fadefloat = 1;//Don't touch
    private float fadespeed = 0.3f;//Speed at which the fade effect happens (1 / 0.3 = 3.33 seconds of fade)

    public List<GameObject> PageLocations = new List<GameObject>();
    private int WhichPage;
    private float TurnSpeed = 5f;//Camera speed when "flipping pages"

    private bool FadeOut;//Triggers final fade out


    private void Update()
    {
        startDelay -= Time.deltaTime;

        if (startDelay <= 0 && !FadeOut)
        {
            fadefloat -= Time.deltaTime * fadespeed;

            if (fadefloat >= 0)
            {
                Color fadecolor = Fadebox.GetComponent<Renderer>().material.color;

                fadecolor = new Color(fadecolor.r, fadecolor.g, fadecolor.b, fadefloat);
                Fadebox.GetComponent<Renderer>().material.color = fadecolor;
                FadeboxEnd.GetComponent<Renderer>().material.color = fadecolor;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (WhichPage != PageLocations.Count - 1)
                    {
                        if(transform.position == PageLocations[WhichPage].transform.position)//If "animation" is over, go to next panel
                        {
                            WhichPage++;
                        }
                        else//If "mid-animation", skip the animation, but don't go to the next panel (QoL improvement)
                        {
                            transform.position = PageLocations[WhichPage].transform.position;
                        }
                    }
                    else
                    {
                        fadefloat = 0;
                        FadeOut = true;
                    }
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, PageLocations[WhichPage].transform.position, TurnSpeed * Time.deltaTime);//Automatically move the camera towards the new panel when designated
        }

        if (FadeOut)
        {
            if (fadefloat <= 1)
            {
                fadefloat += Time.deltaTime * fadespeed;

                Color fadecolor = FadeboxEnd.GetComponent<Renderer>().material.color;

                fadecolor = new Color(fadecolor.r, fadecolor.g, fadecolor.b, fadefloat);
                FadeboxEnd.GetComponent<Renderer>().material.color = fadecolor;
            }
            else
                SceneManager.LoadScene("MainMenu");
        }
    }
}