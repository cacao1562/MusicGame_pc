using UnityEngine;
using System.Collections;

public class CameraResolutionManager : MonoBehaviour {


    public Camera pixelCam;

    public float heightV;
    public float widthV;

    public float x, y; // 카메라의 x,y만큼 떨어진 좌표이다.
    public float xOffsetPx;

    public float Size; // 카메라의 사이즈이다.

    // Use this for initialization
    void Start () {

        Screen.SetResolution(1024, 768, true);
        changTempCamVal();

        //Cursor.visible = false;

    }
	
    private void changTempCamVal()
    {

        pixelCam.rect = new Rect(xOffsetPx/1024, ((768f - heightV) / 768f), widthV / 1024f, heightV / 768f);
        float rate = (1024f / widthV) * (heightV / 768f);

        pixelCam.orthographicSize = 70f * rate;
        pixelCam.transform.position = new Vector3(x, y, pixelCam.transform.position.z);

        if(Size != 0)
        {

            pixelCam.orthographicSize = Size;

        }

    }

}
