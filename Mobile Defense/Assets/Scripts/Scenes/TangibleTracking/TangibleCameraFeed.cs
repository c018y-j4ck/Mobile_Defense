using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using TiltFive;

public class TangibleCameraFeed : MonoBehaviour
{
    // Start is called before the first frame update

    public T5_CameraStreamConfig config = new T5_CameraStreamConfig { };
    public GCHandle imageBuffer1Handle;
    public GCHandle imageBuffer2Handle;
    public GCHandle imageBuffer3Handle;
    public GameObject canvasRawImage;
    public TiltFive.PlayerIndex playerNumber;
    public string triggerKey;

    bool imageOn = false;
    bool firstTime = true;
    bool debounce = false;

    public void activateStream()
    {
        if (firstTime)
        {
            byte[] buff1 = new byte[768 * 600];
            imageBuffer1Handle = GCHandle.Alloc(buff1, GCHandleType.Pinned);
            byte[] buff2 = new byte[768 * 600];
            imageBuffer2Handle = GCHandle.Alloc(buff2, GCHandleType.Pinned);
            byte[] buff3 = new byte[768 * 600];
            imageBuffer3Handle = GCHandle.Alloc(buff3, GCHandleType.Pinned);
            if (!CameraImage.TrySubmitEmptyCameraImageBuffer(playerNumber, imageBuffer1Handle.AddrOfPinnedObject(), 768 * 600))
            {
                Debug.Log("Submission error");
                return;
            }

            if (!CameraImage.TrySubmitEmptyCameraImageBuffer(playerNumber, imageBuffer2Handle.AddrOfPinnedObject(), 768 * 600))
            {
                Debug.Log("Submission error");
                return;
            }

            if (!CameraImage.TrySubmitEmptyCameraImageBuffer(playerNumber, imageBuffer3Handle.AddrOfPinnedObject(), 768 * 600))
            {
                Debug.Log("Submission error");
                return;
            }

            config.cameraIndex = 0;
            config.enabled = true;
            Debug.Log("Created Stream config");
            if (CameraImage.TryConfigureCameraImageStream(playerNumber, config))
            {
                Debug.Log("Stream configured");
            }
            else
            {
                Debug.Log("Stream not configured");
                return;
            }
            firstTime = false;
        }
        imageOn = !imageOn;
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(triggerKey))
        {
            if (!debounce)
            {
                activateStream();
                debounce = true;
            }
        }
        else if (UnityEngine.Input.GetKeyUp(triggerKey))
        {
            debounce = false;
        }
        if (imageOn)
        {
            T5_CamImage imageBuffer = new T5_CamImage(0, 0, 0, 768 * 600);
            if (CameraImage.TryGetFilledCameraImageBuffer(playerNumber, ref imageBuffer))
            {
                byte[] imageArray = new byte[768 * 600];
                Marshal.Copy(imageBuffer.ImageBuffer, imageArray, 0, 768 * 600);

                // Create Unity output texture with detected markers
                Texture2D outputTexture = new Texture2D(768, 600, TextureFormat.R8, false);
                outputTexture.LoadRawTextureData(imageArray);
                outputTexture.Apply();

                // Set texture to see the result
                RawImage rawImage = canvasRawImage.GetComponent<RawImage>();
                rawImage.texture = outputTexture;

                if (!CameraImage.TrySubmitEmptyCameraImageBuffer(playerNumber, imageBuffer.ImageBuffer, 768 * 600))
                {
                    Debug.Log("Submission error");
                }
            }
        }
    }
}
