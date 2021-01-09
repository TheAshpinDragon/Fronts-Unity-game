using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Script : MonoBehaviour
{


    public Canvas TopBarCanvas;
        public GameObject PanTopBar;
        public Canvas ButtonsCanvas;
            public GameObject DropDMaps;
            public GameObject DropDOverlays;
            public GameObject DropDUnits;
            public GameObject DropDUnitSettings;
            public GameObject DropDBattleInfo;
            public GameObject DropDBarConfig;

    public Canvas TextCanvas;

    public Canvas SideBarCanvas;
        public GameObject PanSideBar;

        public GameObject ScrollBar;
        private Scrollbar ScrollbarComp;
        private float ScrollBarVal;

        public GameObject DropDQuick;

    public static List<string> GameLog = new List<string>();
    private string DesplayedLog;
    public TMP_InputField Log_Input_Field;
    public TMP_Text LogText;


    void Start()
    {
        //ScrollbarComp = ScrollBar.GetComponent<Scrollbar>();


    }

    void Update()
    {
        


    }


    public void Scrollbar_Was_Changed() 
    {
        //ScrollBarVal = ScrollbarComp.value;


    }

    public void Log_Entry_Handler(string Entry) 
    {

        Entry = Log_Input_Field.text;
        Log_Input_Field.text = "";

        if (GameLog.Capacity < 10 && Entry != "")
        {

            GameLog.Add(Entry);

        }
        else if(Entry != "")
        {

            GameLog.Add(Entry);
            GameLog.RemoveAt(0);

        }

        if (Entry == "/debug cmdList" || Entry == "/debug cmd1")
        {
            GameLog.Add("/debug cmdList or /debug cmd1 = This help list \n /debug unitCreator or /debug cmd2 = Character Creator UI");
            if (GameLog.Capacity < 10)
            {
                //GameLog.RemoveAt(0);
            }
        }
        if (Entry == "/debug unitCreator" || Entry == "/debug cmd2")
        {
            GameLog.Add("Bringing up Character Creator UI...");
            if (GameLog.Capacity < 10)
            {
                //GameLog.RemoveAt(0);
            }
        }

        int A = 0;
        foreach (string EachEntry in GameLog)
        {
            //Debug.Log(Entry);
            if (A == 0) { DesplayedLog = EachEntry; }
            else { DesplayedLog = DesplayedLog + "\n" + EachEntry; }
            A++;
            LogText.text = Regex.Unescape(DesplayedLog);
        }

    }

}