using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFunction_Library : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void TravelPathVelocity(GameObject Object, Vector3 EndPosition, ref Vector3[] path, ref int pathLength, ref bool markerComplete, float goalAdvanceSpeed, Rigidbody2D RB)
    {
        float PNX = EndPosition.x - Object.transform.position.x; bool XDone = true;
        float PNY = EndPosition.y - Object.transform.position.y; bool YDone = true;

        //Debug.Log("Calc val: " + PNX * goalAdvanceSpeed / PNX + PNY);
        //Debug.Log("Y: " + PNY);
        if (EndPosition != new Vector3())
        {
            float TempX = PNX; float TempY = PNY;
            PNX = TempX * (goalAdvanceSpeed / (Mathf.Abs(TempX) + Mathf.Abs(TempY)));
            PNY = TempY * (goalAdvanceSpeed / (Mathf.Abs(TempX) + Mathf.Abs(TempY)));
            //Debug.Log("X Vellocity: " + PNX);
            //Debug.Log("Y Vellocity: " + PNY);
        }
        if (EndPosition.x - (goalAdvanceSpeed / 100) > Object.transform.position.x || EndPosition.x + (goalAdvanceSpeed / 100) < Object.transform.position.x && EndPosition != new Vector3())
        {
            RB.velocity = new Vector2(PNX, RB.velocity.y);
            XDone = false;
        }
        else { RB.velocity = new Vector2(0f, RB.velocity.y); XDone = true; }

        if (EndPosition.y - (goalAdvanceSpeed / 100) > Object.transform.position.y || EndPosition.y + (goalAdvanceSpeed / 100) < Object.transform.position.y && EndPosition != new Vector3())
        {
            RB.velocity = new Vector2(RB.velocity.x, PNY);
            YDone = false;
        }
        else { RB.velocity = new Vector2(RB.velocity.x, 0f); YDone = true; }

        if (XDone == true && YDone == true)
        {
            Object.transform.position = new Vector3(EndPosition.x, EndPosition.y, Object.transform.position.z);
            RB.velocity = new Vector2(0f, 0f);
            path = VectArraySubtract(path, 0);
            pathLength = path.Length; markerComplete = true;
            //Debug.Log(GoalPosPath[0]);
        }
    }

    public static void TravelPathTransform(GameObject Object, Vector3 EndPosition, ref Vector3[] path, ref int pathLength, ref bool markerComplete, float goalAdvanceSpeed)
    {
        float PNX = EndPosition.x - Object.transform.position.x; bool XDone = true;
        float PNY = EndPosition.y - Object.transform.position.y; bool YDone = true;

        //Debug.Log("Calc val: " + PNX * goalAdvanceSpeed / PNX + PNY);
        //Debug.Log("Y: " + PNY);
        if (EndPosition != new Vector3())
        {
            float TempX = PNX; float TempY = PNY;
            PNX = TempX * (goalAdvanceSpeed / (Mathf.Abs(TempX) + Mathf.Abs(TempX)));
            PNY = TempY * (goalAdvanceSpeed / (Mathf.Abs(TempX) + Mathf.Abs(TempY)));
            //Debug.Log("X Vellocity: " + PNX);
            //Debug.Log("Y Vellocity: " + PNY);
        }
        if (EndPosition.x - goalAdvanceSpeed > Object.transform.position.x || EndPosition.x + goalAdvanceSpeed < Object.transform.position.x && EndPosition != new Vector3())
        {
            Object.transform.position = new Vector3(Object.transform.position.x + PNX, Object.transform.position.y, Object.transform.position.z);
            XDone = false;
        }
        //else if (EndPosition.x - (goalAdvanceSpeed / 10) > Object.transform.position.x || EndPosition.x + (goalAdvanceSpeed / 10) < Object.transform.position.x && EndPosition != new Vector3())
        //{
           // Object.transform.position = new Vector3(Object.transform.position.x + (PNY / 100), Object.transform.position.y, Object.transform.position.z);
            //XDone = false;
        //}
        else { Object.transform.position = new Vector3(EndPosition.x, Object.transform.position.y, Object.transform.position.z); XDone = true; }

        if (EndPosition.y - goalAdvanceSpeed * 2 > Object.transform.position.y || EndPosition.y + goalAdvanceSpeed *2 < Object.transform.position.y && EndPosition != new Vector3())
        {
            Object.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y + PNY, Object.transform.position.z);
            YDone = false;
        }
        //else if (EndPosition.y - (goalAdvanceSpeed / 10) > Object.transform.position.y || EndPosition.y + (goalAdvanceSpeed / 10) < Object.transform.position.y && EndPosition != new Vector3())
        //{
            //Object.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y + (PNY / 100), Object.transform.position.z);
            //YDone = false;
        //}
        else { Object.transform.position = new Vector3(Object.transform.position.x, EndPosition.y, Object.transform.position.z); YDone = true; }

        if (XDone == true && YDone == true)
        {
            Object.transform.position = new Vector3(EndPosition.x, EndPosition.y, Object.transform.position.z);
            path = VectArraySubtract(path, 0);
            Debug.Log(path.Length);
            pathLength = path.Length; markerComplete = true;
            //Debug.Log(GoalPosPath[0]);
        }
    }

    public static Vector3[] VectArrayAdd(Vector3[] InputArray, Vector3 AddValue)//Vector3[] InputArray
    {
        Vector3[] TempVectArray = InputArray; var x = 0;
        InputArray = new Vector3[InputArray.Length + 1];
        foreach (Vector3 Positions in TempVectArray)
        {
            InputArray[x++] = Positions;
            //Debug.Log(Positions + " : " + transform.position + " : " + x);
        }
        InputArray[InputArray.Length - 1] = AddValue;
        return InputArray;
    }

    public static Vector3[] VectArraySubtract(Vector3[] InputArray, int SubtractValue)
    {
        Vector3[] TempVectArray = InputArray; var x = 0;
        InputArray = new Vector3[InputArray.Length - 1];
        foreach (Vector3 Positions in TempVectArray)
        {
            if (x != SubtractValue) { InputArray[x - 1] = Positions; x++; }
            else { x++; }
            //Debug.Log(Positions + " : " + transform.position + " : " + x);
        }
        return InputArray;
    }

}
