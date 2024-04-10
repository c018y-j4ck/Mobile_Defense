using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    Vector3 touchStart;
    public Camera theCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            Debug.Log(currentMagnitude);

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
            Debug.Log("Zoom");
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        //theCam.orthographicSize = Mathf.Clamp(theCam.orthographicSize - increment, zoomOutMin, zoomOutMax);
        theCam.transform.position = new Vector3(theCam.transform.position.x, theCam.transform.position.y - (increment*3), theCam.transform.position.z);
    }
}
