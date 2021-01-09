using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_Script : MonoBehaviour
{
    public bool KeyHeldDown = false;
    public int KeyHeldFor = 0;
    private int HeldGrace = 0;
    private int MoveSpeed = 10;
    private Vector3 MoveVector;
    public Camera MainCamera;
    public static Vector3[] MapBounds = new Vector3[4]; // The map load script will update this value
    public RectTransform ReferenceCanvas;

    void Start()
    {

        MapBounds[0] = new Vector3(0, 0, 0);
        MapBounds[1] = new Vector3(0, 1000, 0);
        MapBounds[2] = new Vector3(1000, 1000, 0);
        MapBounds[3] = new Vector3(1000, 0, 0);

    }

    void Update()
    {
        //Debug.Log(ReferenceCanvas.localScale);
        //if (transform.position == new Vector3(0,0,0))
        //{
        KeyHeldDown = false;

        if (Input.mouseScrollDelta.y > 0 && MainCamera.orthographicSize > 50)
        { MainCamera.orthographicSize = MainCamera.orthographicSize - 25; }
        else if (Input.mouseScrollDelta.y < 0 && MainCamera.orthographicSize < 1500) // CHANGE LATER
        { MainCamera.orthographicSize = MainCamera.orthographicSize + 25; }
        else if (MainCamera.orthographicSize < 50) { MainCamera.orthographicSize = 50; }

        MoveVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("right") || Input.GetKey("left")) { KeyHeldDown = true; }

        if (Input.GetKeyDown("up")) { transform.position = new Vector3(MoveVector.x, MoveVector.y + MoveSpeed, MoveVector.z); }
        if (Input.GetKey("up") && KeyHeldFor >= 20) { transform.position = new Vector3(MoveVector.x, MoveVector.y + MoveSpeed, MoveVector.z); }
        if (Input.GetKeyDown("down")) { transform.position = new Vector3(MoveVector.x, MoveVector.y - MoveSpeed, MoveVector.z); }
        if (Input.GetKey("down") && KeyHeldFor >= 20) { transform.position = new Vector3(MoveVector.x, MoveVector.y - MoveSpeed, MoveVector.z); }
        if (Input.GetKeyDown("right")) { transform.position = new Vector3(MoveVector.x + MoveSpeed, MoveVector.y, MoveVector.z); }
        if (Input.GetKey("right") && KeyHeldFor >= 20) { transform.position = new Vector3(MoveVector.x + MoveSpeed, MoveVector.y, MoveVector.z); }
        if (Input.GetKeyDown("left")) { transform.position = new Vector3(MoveVector.x - MoveSpeed, MoveVector.y, MoveVector.z); }
        if (Input.GetKey("left") && KeyHeldFor >= 20) { transform.position = new Vector3(MoveVector.x - MoveSpeed, MoveVector.y, MoveVector.z); }

        if (KeyHeldDown == true)
        {
            KeyHeldFor++;
            HeldGrace = 0;
            if (KeyHeldFor >= 200) { MoveSpeed = 50; }
            else if (KeyHeldFor >= 400) { MoveSpeed = 200; }
            else { MoveSpeed = 10; }
        }
        else if (HeldGrace >= 15) { KeyHeldFor = 0; HeldGrace = 0; }
        else if (KeyHeldFor != 0) { HeldGrace++; }
        //}
    }
}
