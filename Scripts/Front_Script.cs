using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Front_Script : MonoBehaviour
{
    //==========Front Components==========

    //public bool FrontBroken = false;
    //private bool FrontRetreating = false;
    //private int Defence = 0;
    //public static int FrontNum;
    public int FrontType;
    private float FrontOffset;
    public float AdvanceSpeed = 5f;
    public bool CanMove = true;
    public bool DoObjCheck = false;
    private bool DoTranslate = true;


    //==========Up Components==========

    public bool UpTouchingFronts = false;
    public bool UpEnd = false;

    public GameObject UpFrontObj = null;
    private Transform UpFrontTransform;
    public int UpFrontType;

    public GameObject UpHorzBorderObj;
    private SpriteRenderer UpHorzRenderer;
    private Transform UpHorzTransform;

    public GameObject UpVertBorderObj;
    private SpriteRenderer UpVertBorderRenderer;
    private Transform UpVertBorderTransform;

    //==========Down Components==========

    public bool DownTouchingFronts = false;
    public bool DownEnd = false;

    public GameObject DownFrontObj = null;
    private Transform DownFrontTransform;
    public int DownFrontType;

    public GameObject DownHorzBorderObj = null;
    private SpriteRenderer DownHorzRenderer;
    private Transform DownHorzTransform;

    public GameObject DownVertBorderObj = null;
    private SpriteRenderer DownVertBorderRenderer;
    private Transform DownVertBorderTransform;

    //==========Find Objects Vars==========

    private int ArrayCounter1 = 0;
    private int ArrayCounter2 = 0;
    public GameObject[] Fronts;
    public GameObject[] VertSides;
    private Vector3 AltUpV3;
    private Vector3 AltDownV3;
    private Vector3 AltSideUpV3;
    private Vector3 AltSideDownV3;

    //==========Vectors/Other Vars==========

    private Vector3 NewUpPosition = Vector3.zero;
    private Vector3 NewDownPosition = Vector3.zero;
    private int RandomNumber;
    private float[] OffsetArray;
    //private bool Errors = false;
    private int Flip = 1;
    private int EdgeX;
    private Vector3 GlobalMousePos;
    public Camera Camera;

    // Start is called before the first frame update
    void Start()
    {
        OffsetArray = new float[9];

        OffsetArray[0] = 0f;        //0 - Debug
        OffsetArray[1] = 0f;        //1 - SlowAdvance
        OffsetArray[2] = 78.125f;   //2 - NormalAdvance
        OffsetArray[3] = 125f;      //3 - FastAdvance
        OffsetArray[4] = 125f;      //4 - Charge/Ambush
        OffsetArray[5] = 31.25f;    //5 - Defend/Entrench
        OffsetArray[6] = 0f;        //6 - Stationary
        OffsetArray[7] = 0f;        //7 - Reorganize/Rest
        OffsetArray[8] = 0f;        //8 - Sneak/PrepAmbush

        if (DoObjCheck == true)
        {
            if (gameObject.tag == "1-Front")
            {
                Fronts = GameObject.FindGameObjectsWithTag("1-Front");
                VertSides = GameObject.FindGameObjectsWithTag("1-VertBoundDown");
            }
            else if (gameObject.tag == "2-Front")
            {
                Fronts = GameObject.FindGameObjectsWithTag("2-Front");
                VertSides = GameObject.FindGameObjectsWithTag("2-VertBoundDown");
            }

            foreach (GameObject Front in Fronts) // Fronts
            {
                ArrayCounter2++;
                Vector3 Obj = transform.position;
                Vector3 CheckObj = Front.transform.position - Obj;
                if (UpFrontObj != null) { AltUpV3 = UpFrontObj.transform.position - Obj; }
                if (DownFrontObj != null) { AltDownV3 = DownFrontObj.transform.position - Obj; }

                if ((CheckObj.y > 0) && Front != gameObject) //Above
                {
                    UpFrontObj = Front;
                    UpVertBorderObj = VertSides[ArrayCounter2 - 1];
                }
                else { UpEnd = true; }

                if ((CheckObj.y < 0) && Front != gameObject) //Below
                {
                    DownFrontObj = Front;
                }
                else { DownEnd = true; }
                if (ArrayCounter2 == Fronts.Length) { ArrayCounter2 = 0; }
            }
        }

        if (UpFrontObj != null) { UpEnd = false; } else { UpEnd = true; }
        if (DownFrontObj != null) { DownEnd = false; } else { DownEnd = true; }

        UpHorzRenderer = UpHorzBorderObj.GetComponent("SpriteRenderer") as SpriteRenderer;
        UpHorzTransform = UpHorzBorderObj.GetComponent("Transform") as Transform;
        DownHorzRenderer = DownHorzBorderObj.GetComponent("SpriteRenderer") as SpriteRenderer;
        DownHorzTransform = DownHorzBorderObj.GetComponent("Transform") as Transform;
        DownVertBorderRenderer = DownVertBorderObj.GetComponent("SpriteRenderer") as SpriteRenderer;
        DownVertBorderTransform = DownVertBorderObj.GetComponent("Transform") as Transform;
        Camera = Camera.main;

        if (UpEnd == false)
        {
            UpFrontTransform = UpFrontObj.GetComponent("Transform") as Transform;
            UpVertBorderRenderer = UpVertBorderObj.GetComponent("SpriteRenderer") as SpriteRenderer;
            UpVertBorderTransform = UpVertBorderObj.GetComponent("Transform") as Transform;
        }

        if (DownEnd == false)
        {
            DownFrontTransform = DownFrontObj.GetComponent("Transform") as Transform;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("[1]"))
        {
            //Debug.Log(transform.position.x - ((transform.position.x - UpFrontTransform.position.x) / 2));
            Debug.Log(Input.mousePosition.x);
        }

        if (Input.GetKeyDown("[2]"))
        {
            //Debug.Log((Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(UpFrontTransform.position.x)) * 0.04f) / 2);
            Debug.Log(((12 - 6) * 0.04) / 2);
        }

        if (Input.GetKeyDown("space"))
        {
            DoTranslate = true;
        }

    }

    void FixedUpdate()
    {
        GlobalMousePos = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        if (DoObjCheck == true)
        {
            if (gameObject.tag == "1-Front")
            {
                Fronts = GameObject.FindGameObjectsWithTag("1-Front");
                VertSides = GameObject.FindGameObjectsWithTag("1-VertBoundDown");
            }
            else if (gameObject.tag == "2-Front")
            {
                Fronts = GameObject.FindGameObjectsWithTag("2-Front");
                VertSides = GameObject.FindGameObjectsWithTag("2-VertBoundDown");
            }

            foreach (GameObject Front in Fronts) // Fronts
            {
                ArrayCounter1++;
                Vector3 Obj = transform.position;
                Vector3 CheckObj = Front.transform.position - Obj;
                if (UpFrontObj != null) { AltUpV3 = UpFrontObj.transform.position - Obj; }
                if (DownFrontObj != null) { AltDownV3 = DownFrontObj.transform.position - Obj; }

                if ((CheckObj.y > 0) && UpFrontObj == null && Front != gameObject) //Above
                {
                    UpFrontObj = Front;
                    UpVertBorderObj = VertSides[ArrayCounter1 - 1];
                }
                else { UpEnd = true; }

                if ((CheckObj.y < 0) && DownFrontObj == null && Front != gameObject) //Below
                {
                    DownFrontObj = Front;
                }
                else { DownEnd = true; }

                if ((UpFrontObj != null) && Front != gameObject && (CheckObj.y > 0) && (AltUpV3.y > CheckObj.y)) //Above
                {
                    UpFrontObj = Front;
                    UpVertBorderObj = VertSides[ArrayCounter1 - 1];
                }

                if ((DownFrontObj != null) && Front != gameObject && (CheckObj.y < 0) && (AltDownV3.y < CheckObj.y)) //Below
                {
                    DownFrontObj = Front;
                }
                if (ArrayCounter1 == Fronts.Length) { ArrayCounter1 = 0; }
            }
        }

        if (UpFrontObj != null) { UpEnd = false; } else { UpEnd = true; }
        if (DownFrontObj != null) { DownEnd = false; } else { DownEnd = true; }

        if (UpEnd == false)
        {
            UpFrontTransform = UpFrontObj.GetComponent("Transform") as Transform;
            UpVertBorderRenderer = UpVertBorderObj.GetComponent("SpriteRenderer") as SpriteRenderer;
            UpVertBorderTransform = UpVertBorderObj.GetComponent("Transform") as Transform;
        }

        if (DownEnd == false)
        {
            DownFrontTransform = DownFrontObj.GetComponent("Transform") as Transform;
            DownVertBorderRenderer = DownVertBorderObj.GetComponent("SpriteRenderer") as SpriteRenderer;
            DownVertBorderTransform = DownVertBorderObj.GetComponent("Transform") as Transform;
        }

        var ObjAngZ = transform.eulerAngles.z;
        if ((ObjAngZ < 75 && ObjAngZ >= 0) || (ObjAngZ >= 285 && ObjAngZ <= 360)) { Flip = 1; } // Roght facing
        if (ObjAngZ > 105 && ObjAngZ <= 255) { Flip = -1; } // Left facing

        if (Flip == 1) { EdgeX = 815; } else { EdgeX = 3185; }

        //#################################################################################################### CONNECTOR LOCATION/SIZE #####################################################################################################

        if (DoTranslate == true)
        {
            transform.DetachChildren();

            if (UpEnd == false)//----------Up Connector----------
            {
                if (UpTouchingFronts == true) // IGNORE
                {
                    if ((transform.position.x - OffsetArray[FrontType]) * Flip > (UpFrontTransform.position.x - OffsetArray[UpFrontType]) * Flip) // IGNORE
                    {
                        UpHorzTransform.position = new Vector3(transform.position.x - (((transform.position.x + OffsetArray[FrontType]) - (UpFrontTransform.position.x - OffsetArray[UpFrontType])) / 2), UpHorzTransform.position.y, UpHorzTransform.position.z);
                        UpHorzRenderer.size = new Vector2((((transform.position.x - OffsetArray[FrontType]) - (UpFrontTransform.position.x - OffsetArray[UpFrontType])) * 0.02f), 5.12f);
                        UpHorzRenderer.flipY = true;
                    }
                    else if ((transform.position.x - OffsetArray[FrontType]) * Flip <= (UpFrontTransform.position.x - OffsetArray[UpFrontType]) * Flip) // IGNORE
                    {
                        UpHorzTransform.position = new Vector3(transform.position.x, UpHorzTransform.position.y, UpHorzTransform.position.z);
                        UpHorzRenderer.size = new Vector2(0, 5.12f);
                        UpHorzRenderer.flipY = false;
                        UpVertBorderRenderer.size = new Vector2(0, 0);
                    }
                }
                else
                {

                    if ((transform.position.x - OffsetArray[FrontType]) * Flip > (UpFrontTransform.position.x - OffsetArray[UpFrontType]) * Flip) // Current front is to the right of the above front
                    {
                        UpHorzTransform.position = new Vector3(transform.position.x - (((transform.position.x + OffsetArray[FrontType]) - (UpFrontTransform.position.x - OffsetArray[UpFrontType])) / 2), UpHorzTransform.position.y, UpHorzTransform.position.z);
                        UpHorzRenderer.size = new Vector2((((transform.position.x - OffsetArray[FrontType]) - (UpFrontTransform.position.x) + OffsetArray[UpFrontType]) * 0.02f), 5.12f);
                        UpHorzRenderer.flipY = true;
                    }
                    else if ((transform.position.x - OffsetArray[FrontType]) * Flip <= (UpFrontTransform.position.x - OffsetArray[UpFrontType]) * Flip) // Current front is to the left of the above front #PROBLEM#
                    {
                        UpHorzTransform.position = new Vector3(transform.position.x, UpHorzTransform.position.y, UpHorzTransform.position.z);
                        UpHorzRenderer.size = new Vector2(0, 5.12f);
                        UpHorzRenderer.flipY = false;
                        UpVertBorderTransform.position = new Vector3(gameObject.transform.position.x - OffsetArray[FrontType], transform.position.y - ((transform.position.y - UpFrontTransform.position.y) / 2), UpVertBorderTransform.position.z);
                        UpVertBorderRenderer.size = new Vector2((((transform.position.y + 125) - (UpFrontTransform.position.y - 125)) * 0.02f), 5.12f);
                    }
                }
            }
            else//835 3050
            {
                //UpVertBorderRenderer.size = new Vector2(0, 0);
                UpHorzTransform.position = new Vector3(((transform.position.x - OffsetArray[FrontType]) + EdgeX) / 2, UpHorzTransform.position.y, UpHorzTransform.position.z);
                UpHorzRenderer.size = new Vector2(((transform.position.x - OffsetArray[FrontType]) - EdgeX) * 0.02f, 5.12f);
            }

            if ((DownEnd == false))//----------Down Connector----------
            {
                if (DownTouchingFronts == true) // IGNORE
                {
                    if ((transform.position.x - OffsetArray[FrontType]) * Flip > (DownFrontTransform.position.x - OffsetArray[DownFrontType]) * Flip) // IGNORE
                    {
                        DownHorzTransform.position = new Vector3(transform.position.x - (((transform.position.x + OffsetArray[FrontType]) - (DownFrontTransform.position.x - OffsetArray[DownFrontType])) / 2), DownHorzTransform.position.y, DownHorzTransform.position.z);
                        DownHorzRenderer.size = new Vector2((((transform.position.x - OffsetArray[FrontType]) - (DownFrontTransform.position.x - OffsetArray[DownFrontType])) * 0.02f), 5.12f);
                        DownHorzRenderer.flipY = false;
                    }
                    else if ((transform.position.x - OffsetArray[FrontType]) * Flip <= (DownFrontTransform.position.x - OffsetArray[DownFrontType]) * Flip) // IGNORE
                    {
                        DownHorzTransform.position = new Vector3(transform.position.x, DownHorzTransform.position.y, DownHorzTransform.position.z);
                        DownHorzRenderer.size = new Vector2(0, 5.12f);
                        DownHorzRenderer.flipY = true;
                        DownVertBorderRenderer.size = new Vector2(0, 0);
                    }
                }
                else
                {
                    if ((transform.position.x - OffsetArray[FrontType]) * Flip > (DownFrontTransform.position.x - OffsetArray[DownFrontType]) * Flip) // Current front is to the right of the lower front
                    {
                        DownHorzTransform.position = new Vector3(transform.position.x - (((transform.position.x + OffsetArray[FrontType]) - (DownFrontTransform.position.x - OffsetArray[DownFrontType])) / 2), DownHorzTransform.position.y, DownHorzTransform.position.z);
                        DownHorzRenderer.size = new Vector2((((transform.position.x - OffsetArray[FrontType]) - (DownFrontTransform.position.x - OffsetArray[DownFrontType])) * 0.02f), 5.12f);
                        DownHorzRenderer.flipY = false;
                    }
                    else if ((transform.position.x - OffsetArray[FrontType]) * Flip <= (DownFrontTransform.position.x - OffsetArray[DownFrontType]) * Flip) // Current front is to the left of the lower front
                    {
                        DownHorzTransform.position = new Vector3(transform.position.x, DownHorzTransform.position.y, DownHorzTransform.position.z);
                        DownHorzRenderer.size = new Vector2(0, 5.12f);
                        DownHorzRenderer.flipY = true;
                        DownVertBorderTransform.position = new Vector3(transform.position.x - OffsetArray[FrontType], transform.position.y - ((transform.position.y - DownFrontTransform.position.y) / 2), DownVertBorderTransform.position.z);
                        DownVertBorderRenderer.size = new Vector2((((transform.position.y - 125) - (DownFrontTransform.position.y + 125)) * 0.02f), 5.12f);
                    }
                }
            }
            else//835 3050
            {
                DownVertBorderRenderer.size = new Vector2(0, 0);
                DownHorzTransform.position = new Vector3(((transform.position.x - OffsetArray[FrontType]) + EdgeX) / 2, DownHorzTransform.position.y, DownHorzTransform.position.z);
                DownHorzRenderer.size = new Vector2(((transform.position.x - OffsetArray[FrontType]) - EdgeX) * 0.02f, 5);
            }
        }
        else
        {
            UpHorzBorderObj.transform.SetParent(transform);
            DownHorzBorderObj.transform.SetParent(transform);
            DownVertBorderObj.transform.SetParent(transform);
        }

        if (Input.GetMouseButton(0))
        {
            if (CanMove == true && GlobalMousePos.x >= 835 && GlobalMousePos.x <= 3050)
            {
                transform.position = new Vector3(GlobalMousePos.x, transform.position.y, transform.position.z);
            }
            DoTranslate = true;
        }
        else { DoTranslate = false; }
    }
}
