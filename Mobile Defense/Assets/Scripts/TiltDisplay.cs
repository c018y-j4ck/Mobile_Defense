using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltDisplay : MonoBehaviour
{
    public Transform ship;
    private Quaternion tiltRotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiltRotation = Quaternion.Lerp(
            tiltRotation,
            Quaternion.Euler(Input.acceleration.y * 90f, 0f, -Input.acceleration.x * 90f),
            0.05f);

        ship.rotation = tiltRotation;
    }
}
