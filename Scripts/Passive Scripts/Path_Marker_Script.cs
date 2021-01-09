using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_Marker_Script : MonoBehaviour
{
    public string ParentScript;
    public int DelNum;

    // Start is called before the first frame update
    void Start()
    {
        if (ParentScript == "Unit_Script")
        {
            DelNum = Unit_Script.DelUpdate;
        }
        if (ParentScript == "Unit_Controller_Script")
        {
            DelNum = Unit_Controller_Script.DelUpdate;
        }
    }

    void FixedUpdate()
    {
        if (ParentScript == "Unit_Script")
        {
            if (Unit_Script.DelSub == true)
            { DelNum--; }

            if (DelNum == 0)
            { Destroy(gameObject); }
        }
        if (ParentScript == "Unit_Controller_Script")
        {
            if (Unit_Controller_Script.DelSub == true)
            { DelNum--; }

            if (DelNum == 0)
            { Destroy(gameObject); }
        }
    }
}
