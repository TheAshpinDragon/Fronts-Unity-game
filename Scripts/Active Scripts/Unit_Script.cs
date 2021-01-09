using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Script : MonoBehaviour
{
    private Rigidbody2D UnitRB;

    //public GameObject[] UnitArray; // All individual units attatched to this culmative unit
    //public string[] Formation;
    //private bool InCombat = false;
    public bool Selected = false;
    public string UnitName = "";

    public Vector3 GlobalMousePos;
    private GameObject CameraObj;
    private Camera Camera;
    //private int layerMask = 1 << 8;
    private RaycastHit2D SelectRay;

    /*private float HealthLVL;
        private float LifeSpan;
        private float DietGrade;
        private float SleepGrade;
        private float IllnessResist;
        private List<string> Illnessess = new List<string>(); // C = chroninc, I = inactive, A = Alergy, P = Persistant
        private List<string> CurrentInjuries = new List<string>(); // U = untreated, T = Treated, H = healing
        public float[] HealthSeed1;
        public float[] HealthSeed2;
        public float[] HealthSeed3;
            private float Afactor;
            private float Bfactor;
            private float Cfactor;
            private float Dfactor;
            private float Efactor;

    public float MentalLVL;
        public float EngineeringLVL;
        public float MedicalLVL;
        public float ScienceLVL;
        public float StratagyLVL;

    private float StrengthLVL;
        private float SwordsmanLVL;
        private float BowmanLVL;
        private float CarryCapacity; // Lbs - must be realistic in all curcumstances; 10 - 15 = 0, 100+ = 10

    private float PerseptionLVL;
        private float SightDistance;
        private List<float> EyeHealth = new List<float>(); // 0 = Right (0-20) / 1 = Left (0 - 20)
    
    private float StaminaLVL;
        private List<float> SpeedRange = new List<float>(); // 0 = slowest / 1 = fastest
        private List<float> EnduranceDistances = new List<float>(); // 0 = 100m / 1 = 1mi / 2 = 10 mi

    private float MoralLVL;
        private float MoralCap;
        private float TraumaResist;
        private float StressResist;

    private float LearningLVL;
        private float LearnSpeed;
        private float KnowladgeRetainLVL;*/

    public float Health = 100;
    public float Armor = 1;
    public float Damage = 5;
    public float Moral = 100;
    public int Perception = 10;


    public Vector3 GoalPos;
    public Vector3[] GoalPosPath;
    private GameObject[] PrefabList;
    public CircleCollider2D HitBox;
    public CircleCollider2D InteractionBounds;
    public static int DelUpdate;
    public static bool DelSub;
    private bool NoObstacle = true;
    private float AdvanceSpeed;
    public float GoalAdvanceSpeed = 1;

    int LoopNum = 0;

    void Start()
    {
        Camera = Camera.main;
        UnitRB = transform.GetComponent("Rigidbody2D") as Rigidbody2D;
        PrefabList = Resources.LoadAll<GameObject>("Presets/Marker Presets/Path_Marker");
    }

    void Update()
    {
        GlobalMousePos = Camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        if (Input.GetMouseButton(0) && Selected == false)
        {
            //SelectRay = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));//, layerMask

            //if (SelectRay == gameObject)
            //{
            //    if (SelectRay.transform.name == transform.name)
            //        Selected = true;
            //}

        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.white);

        if (Selected == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Selected = false;
            }

            if (Input.GetButton("Left Shift") && Input.GetMouseButtonDown(0))
            {
                //GoalPos = GlobalMousePos;
            }
            //if (Input.GetButton("Left Shift") && Input.GetButton("Left Control") && Input.GetMouseButtonDown(0))
            //{
                //GoalPosPath = VectArrayAdd(GoalPosPath, GlobalMousePos);

                //DelUpdate = GoalPosPath.Length;
                //GameObject PathMarker = Instantiate(PrefabList[0]);
                //PathMarker.transform.position = new Vector3(GlobalMousePos.x, GlobalMousePos.y, 0);
            //}
        }
    }

    void FixedUpdate()
    {
        DelSub = false;
        if (GoalPosPath.Length >= 1 && NoObstacle == true)//PathFind(GoalPosPath[0], true, GoalAdvanceSpeed)
        { GFunction_Library.TravelPathVelocity(gameObject, GoalPosPath[0], ref GoalPosPath, ref DelUpdate, ref DelSub, GoalAdvanceSpeed, UnitRB); }

        //PathFind(Perception);

        LoopNum++;
    }

    private void OnTriggerStay2D(Collider2D col)//InteractionBounds HitBox
    {
        //Debug.Log(LoopNum);
        //CircleCollider2D[] Colliders = col.GetComponents<CircleCollider2D>();
        //if (col.GetComponent<CircleCollider2D>() == InteractionBounds) { Debug.Log("Interact Bounds"); }
        //if (col.GetComponent<CircleCollider2D>() == HitBox) { Debug.Log("Hit Box"); }

        if (col.gameObject.tag == "Obstacle" && GoalPosPath.Length != 0)//col.GetComponent<CircleCollider2D>() == InteractionBounds && 
        {
            //GoalPosPath = VectArraySubtract(GoalPosPath, 0);
            //DelUpdate = GoalPosPath.Length; DelSub = true;
            GFunction_Library.TravelPathVelocity(gameObject, transform.position, ref GoalPosPath, ref DelUpdate, ref DelSub, 1, UnitRB);
            //TravelPathVelocity(GameObject Object, Vector3 EndPosition, Vector3[] path, int pathLength, bool markerComplete, float goalAdvanceSpeed, Rigidbody2D RB)
            GameObject[] ToDelete;
            ToDelete = GameObject.FindGameObjectsWithTag("Path-Marker");
            foreach (GameObject delete in ToDelete)
            {
                Destroy(delete);
            }
        }
    }

    
    private void PathFind(int Perception)
    {
        int TotalRays = 130 + (Perception * 2);

        int AccurateViss = Mathf.RoundToInt(TotalRays * 0.525f);
        float AccurateIncrament = 30f / AccurateViss;
        RaycastHit2D[] AccurateSees = new RaycastHit2D[0]; int A = AccurateViss;

        int InaccurateViss = Mathf.RoundToInt(TotalRays * 0.45f);
        float InaccurateIncrament = 90 / InaccurateViss;
        RaycastHit2D[] InaccurateSees = new RaycastHit2D[0]; int I = InaccurateViss;

        int PerriferalViss = Mathf.RoundToInt(TotalRays * 0.025f);
        float PerriferalIncrament = 10 / PerriferalViss;
        RaycastHit2D[] PerriferalSees = new RaycastHit2D[0]; int P = PerriferalViss;

        while (A >= 0)
        {
            LayerMask mask = LayerMask.GetMask("Obstacle");
            float AngleGoal = (AccurateViss - A) * AccurateIncrament + 75;
            float RayY = 0.01f;
            float RayX;
            if (Mathf.Tan(AngleGoal * (Mathf.PI / 180)) * RayY != 0)
            { RayX = Mathf.Tan(AngleGoal * (Mathf.PI / 180)) * RayY; }
            else { RayX = 0; RayY = 1; }
            RaycastHit2D TempRay = Physics2D.Raycast(transform.position, new Vector2(RayX, RayY), 2000);//100 * (Perception * 2)
            //Debug.DrawRay(transform.position, new Vector3(RayX * 100000, RayY * 100000, 0), Color.white);

            //Debug.Log(TempRay.transform.name);
            RaycastHit2D[] TempRayArray = AccurateSees; var x = 0;
            AccurateSees = new RaycastHit2D[AccurateSees.Length + 1];
            foreach (RaycastHit2D Ray in TempRayArray)
            {
                AccurateSees[x++] = Ray;
                //Debug.Log(Positions + " : " + transform.position + " : " + x);
            }
            AccurateSees[AccurateSees.Length - 1] = TempRay;

            //Debug.Log(A + " Angle: " + AngleGoal + " X: " + RayX + " Y: " + RayY + " # of rays: " + (AccurateViss - A) + " Angle incrament: " + AccurateIncrament);

            A--;
        }
    }


    public void CheckUnitStats(ref string FuncTarget, ref float FuncHealth, ref float FuncArmour, ref float FuncDamage, ref float FuncMoral)
    {
        FuncTarget = UnitName;
        FuncHealth = Health;
        FuncArmour = Armor;
        FuncDamage = Damage;
        FuncMoral = Moral;
    }
}
