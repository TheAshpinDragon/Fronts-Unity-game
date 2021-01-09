using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Front : MonoBehaviour
{

    //0 - Debug
    //1 - SlowAdvance
    //2 - NormalAdvance
    //3 - FastAdvance
    //4 - Charge/Ambush
    //5 - Defend/Entrench
    //6 - Stationary
    //7 - Reorganize/Rest
    //8 - Sneak/PrepAmbush

    public GameObject Camera;
    private Camera CameraCompon;
    private int NewFrontType = 0;
    private int Facing = 0;
    private Sprite[] SpriteList;
    private GameObject[] PrefabList;

    public static Vector3 GlobalMousePos;
    private float x;
    private float y;
    private float z;

    // Start is called before the first frame update
    void Start()
    {
        SpriteList = Resources.LoadAll<Sprite>("Sprites/Board Pieces/RTS_Strategy_Board_Pieces_High_def");
        PrefabList = Resources.LoadAll<GameObject>("Presets/Front Presets");
        CameraCompon = Camera.GetComponent("Camera") as Camera;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1")) { NewFrontType = 3; }
        if (Input.GetKeyDown("2")) { NewFrontType = 5; }
        if (Input.GetKeyDown("3")) { NewFrontType = 7; }
        if (Input.GetKeyDown("4")) { NewFrontType = 9; }
        if (Input.GetKeyDown("5")) { NewFrontType = 11; }

        x = Input.mousePosition.x;
        y = Input.mousePosition.y;
        z = 0;
        GlobalMousePos = CameraCompon.ScreenToWorldPoint(new Vector3(x, y, z));

        if (Input.GetKeyDown("."))
        {
            Facing = 1;
        }
        if (Input.GetKeyDown(","))
        {
            Facing = 0;
        }

        if (Input.GetKeyDown("space") && NewFrontType != 0)//Debug.Log(NewFrontType);
        {
            GameObject NewFront = Instantiate(PrefabList[Facing]);

            SpriteRenderer FrontRenderer = NewFront.GetComponent("SpriteRenderer") as SpriteRenderer;

            FrontRenderer.sprite = SpriteList[NewFrontType - 1];

            NewFront.transform.position = new Vector3(GlobalMousePos.x, GlobalMousePos.y, -3);
        }
    }
}
