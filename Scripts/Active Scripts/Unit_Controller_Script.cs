using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Controller_Script : MonoBehaviour
{
    private Rigidbody2D UnitContainerRB;

    //public GameObject[] UnitArray; // All individual units attatched to this culmative unit
    public string[] Formation;
    private bool InCombat = false;
    public bool Selected = false;
    public bool Selectable = true;

    public Vector3 GlobalMousePos;
    private GameObject CameraObj;
    private Camera Camera;
    //private int layerMask = 1 << 8;
    private RaycastHit2D SelectRay;
    public string Name = "";

    public Vector3[] GoalPosPath;
    private GameObject[] PrefabList;
    //public CircleCollider2D HitBox;
    //public CircleCollider2D InteractionBounds;
    public static int DelUpdate;
    public static bool DelSub;
    private bool NoObstacle = true;
    private float AdvanceSpeed;
    public float GoalAdvanceSpeed = 1;

    //public List<Unit_Script> UnitScript = new List<Unit_Script>();
    //public List<GameObject> Units = new List<GameObject>();
    //public List<string> CName = new List<string>(); private string TempName;
    //public List<float> CHealth = new List<float>(); private float TempHealth;
    //public List<float> CArmor = new List<float>(); private float TempArmor;
    //public List<float> CDamage = new List<float>(); private float TempDamage;
    //public List<float> CMoral = new List<float>(); private float TempMoral;

    public Unit_Script[] UnitScript;
    public GameObject[] Units;
    public string[] CName; private string TempName;
    public float[] CHealth; private float TempHealth;
    public float[] CArmor; private float TempArmor;
    public float[] CDamage; private float TempDamage;
    public float[] CMoral; private float TempMoral;

    public List<GameObject> Combatants = new List<GameObject>();
    public List<Unit_Controller_Script> UnitController = new List<Unit_Controller_Script>();

    //public GameObject[] Combatants;

    int LoopNum = 0;

    void Start()
    {
        Camera = Camera.main;
        UnitContainerRB = transform.GetComponent("Rigidbody2D") as Rigidbody2D;
        PrefabList = Resources.LoadAll<GameObject>("Presets/Marker Presets/Container_Path_Marker");
    }

    void Update()
    {
        GlobalMousePos = Camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        //Transform.childCount Transform.GetChild

        UnitStatsUpdate();

        if (Input.GetMouseButton(0) && Selected == false && Selectable == true)
        {
            SelectRay = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));//, layerMask

            if (SelectRay == gameObject && SelectRay.transform.name == transform.name)
            {
                Selected = true;
            }
        }
        else if (Selectable == false) { Selected = false; }

        if (Selected == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Selected = false;
            }

            if (Input.GetButton("Left Shift") && Input.GetMouseButtonDown(0))
            {
                GoalPosPath = GFunction_Library.VectArrayAdd(GoalPosPath, GlobalMousePos);

                DelUpdate = GoalPosPath.Length;
                GameObject PathMarker = Instantiate(PrefabList[0]);
                PathMarker.transform.position = new Vector3(GlobalMousePos.x, GlobalMousePos.y, 0);
            }
            if (Input.GetButton("Left Shift") && Input.GetButton("Left Control") && Input.GetMouseButtonDown(0))
            {
                
            }
        }
    }

    void FixedUpdate()
    {
        DelSub = false;
        if (GoalPosPath.Length >= 1 && NoObstacle == true)
        { GFunction_Library.TravelPathTransform(gameObject, GoalPosPath[0], ref GoalPosPath, ref DelUpdate, ref DelSub, GoalAdvanceSpeed); }
        LoopNum++;
        //Debug.Log(LoopNum + " " + DelSub + " " + DelUpdate);
    }

    private void OnTriggerStay2D(Collider2D col)//InteractionBounds HitBox
    {
        //Debug.Log(LoopNum);
        //CircleCollider2D[] Colliders = col.GetComponents<CircleCollider2D>();
        //if (col.GetComponent<CircleCollider2D>() == InteractionBounds) { Debug.Log("Interact Bounds"); }
        //if (col.GetComponent<CircleCollider2D>() == HitBox) { Debug.Log("Hit Box"); }

        if (col.gameObject.tag == "Obstacle" && GoalPosPath.Length != 0)//col.GetComponent<CircleCollider2D>() == InteractionBounds && 
        {
            GFunction_Library.TravelPathTransform(gameObject, transform.position, ref GoalPosPath, ref DelUpdate, ref DelSub, 1);
            GameObject[] ToDelete;
            ToDelete = GameObject.FindGameObjectsWithTag("Path-Marker");
            foreach (GameObject delete in ToDelete)
            {
                Destroy(delete);
            }
        }
        if (col.gameObject.tag == "Enemy")//col.GetComponent<CircleCollider2D>() == InteractionBounds && 
        {
            Debug.Log("Enemy Encountered");
            InCombat = true;
            if (!Combatants.Contains(col.gameObject)) { Combatants.Add(col.gameObject); }
            if (!UnitController.Contains(col.gameObject.GetComponent<Unit_Controller_Script>())) { UnitController.Add(col.gameObject.GetComponent<Unit_Controller_Script>()); }
        }
    }

    private void UnitStatsUpdate()
    {

        UnitScript = new Unit_Script[gameObject.transform.childCount];
        Units = new GameObject[gameObject.transform.childCount];
        CName = new string[gameObject.transform.childCount];
        CHealth = new float[gameObject.transform.childCount];
        CArmor = new float[gameObject.transform.childCount];
        CDamage = new float[gameObject.transform.childCount];
        CMoral = new float[gameObject.transform.childCount];

        for (int a = 0; a < gameObject.transform.childCount; a++)
        {
            Transform TransTemp = gameObject.transform.GetChild(a);
            Units[a] = TransTemp.gameObject;
            UnitScript[a] = Units[a].GetComponent<Unit_Script>();
            UnitScript[a].CheckUnitStats(ref TempName, ref TempHealth, ref TempArmor, ref TempDamage, ref TempMoral);
            CName[a] = TempName;
            CHealth[a] = TempHealth;
            CArmor[a] = TempArmor;
            CDamage[a] = TempDamage;
            CMoral[a] = TempMoral;
        }
    }

    private void Battle() 
    {

        if (Combatants.Count != 0) 
        {
            foreach (Unit_Controller_Script UCS in UnitController) 
            {
                
            }
            
        }

    }

}
