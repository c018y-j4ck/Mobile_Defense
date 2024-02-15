using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource normalSource;
    public AudioSource dangerSource;
    private bool inDanger = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inDanger)
        {
            if (normalSource.volume > 0)
            {
                normalSource.volume -= 0.01f;
            }
            if (dangerSource.volume < AudioListener.volume)
            {
                dangerSource.volume += 0.01f;
            }
        }
        else
        {
            if (dangerSource.volume > 0)
            {
                dangerSource.volume -= 0.01f;
            }
            if (normalSource.volume < AudioListener.volume)
            {
                normalSource.volume += 0.01f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemies in Danger Zone");
            inDanger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemies in Danger Zone");
            inDanger = true;
        }
    }
}
