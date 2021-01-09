using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quick_Unit_Info_Script : MonoBehaviour
{
    public RectTransform ObjRT;
    private GameObject UnitPair;

    public Button CollapseBtn;
    public Button InventoryBtn;
    public Button MessageBtn;
    public Button LocateBtn;

    public GameObject StatusPannel;

    public TMP_Text UnitNameText;
    public TMP_Text RoleText;
    public TMP_Text StatusText;
    public TMP_Text LimbsText;
    public TMP_Text MoralText;

    public bool IsExpanded = true;
    public int PlaceNumber = 0;

    void Start()
    {
        


    }

    void Update()
    {
        


    }

    public void CollapseExpandBtnPressed() //Debug.Log("Click");
    {
        if (IsExpanded == false) 
        {
            IsExpanded = true;
            StatusPannel.SetActive(true);
            ObjRT.sizeDelta = new Vector2(600, 100);
            transform.position = new Vector3(transform.position.x, transform.position.y - 155, transform.position.z);
            UnitNameText.transform.position = new Vector3(UnitNameText.transform.position.x, UnitNameText.transform.position.y + 155, UnitNameText.transform.position.z);
            CollapseBtn.transform.position = new Vector3(CollapseBtn.transform.position.x, CollapseBtn.transform.position.y + 155, CollapseBtn.transform.position.z);
            InventoryBtn.transform.position = new Vector3(InventoryBtn.transform.position.x, InventoryBtn.transform.position.y + 155, InventoryBtn.transform.position.z);
            MessageBtn.transform.position = new Vector3(MessageBtn.transform.position.x, MessageBtn.transform.position.y + 155, MessageBtn.transform.position.z);
            LocateBtn.transform.position = new Vector3(LocateBtn.transform.position.x, LocateBtn.transform.position.y + 155, LocateBtn.transform.position.z);
        }
        else
        {
            IsExpanded = false;
            StatusPannel.SetActive(false);
            ObjRT.sizeDelta = new Vector2(600, -250);
            transform.position = new Vector3(transform.position.x, transform.position.y + 155, transform.position.z);
            UnitNameText.transform.position = new Vector3(UnitNameText.transform.position.x, UnitNameText.transform.position.y - 155, UnitNameText.transform.position.z);
            CollapseBtn.transform.position = new Vector3(CollapseBtn.transform.position.x, CollapseBtn.transform.position.y - 155, CollapseBtn.transform.position.z);
            InventoryBtn.transform.position = new Vector3(InventoryBtn.transform.position.x, InventoryBtn.transform.position.y - 155, InventoryBtn.transform.position.z);
            MessageBtn.transform.position = new Vector3(MessageBtn.transform.position.x, MessageBtn.transform.position.y - 155, MessageBtn.transform.position.z);
            LocateBtn.transform.position = new Vector3(LocateBtn.transform.position.x, LocateBtn.transform.position.y - 155, LocateBtn.transform.position.z);
        }
        
        
    }

}
