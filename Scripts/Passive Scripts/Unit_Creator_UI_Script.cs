using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit_Creator_UI_Script : MonoBehaviour
{
    public GameObject UnitImageContainer;
        private Dropdown UnitTypeDropdown;
        private Image Unit_Image;
        private Image Private_Image;
        private Image Officer_Image;
        private Image General_Image;

    public GameObject UnitNameLable;
        private TMP_InputField UnitNameInput;

    public GameObject BackStoryDropdownsText;
        private TMP_Dropdown LifeDropdown;
        private TMP_Dropdown TrainingDropdown;

    public GameObject TrainingContainer;
        private Scrollbar TankSlider;
        private Scrollbar RangerSlider;
        private Scrollbar MentalSlider;

    public GameObject StatisticsPannel;
        private TMP_Text HealthStatText;
            private TMP_Text LifeSpanText;
            private TMP_Text DietGradeText;
            private TMP_Text SleepGradeText;
            private TMP_Text IllnessResistText;
            private TMP_Text IllnessesText;
            private TMP_Text InjuriesText;
    
        private TMP_Text MentalStatText;
            private TMP_Text EngineeringText;
            private TMP_Text MedicalText;
            private TMP_Text ScienceText;
            private TMP_Text StratagyText;
    
        private TMP_Text StrengthText;
            private TMP_Text SwordsmanText;
            private TMP_Text BowmanText;
            private TMP_Text CarryCapacityText;
    
        private TMP_Text PerseptionStatText;
            private TMP_Text SightDistanceText;
            private TMP_Text SightAccuracyText;
    
        private TMP_Text StaminaStatText;
            private TMP_Text SpeedRangeText;
            private TMP_Text SustainedRunText;
    
        private TMP_Text MoralStatText;
            private TMP_Text MoralCapText;
            private TMP_Text TraumaResistText;
            private TMP_Text StressResistText;

        private TMP_Text LearningText;
            private TMP_Text LearnSpeedText;
            private TMP_Text RetainText;

        private GameObject ResetCancelFinishPannel;
            private Button ResetButton;
            private Button CancelButton;
            private Button FinishButton;
            private TMP_Text CostText;
            private TMP_Text AvailibleText;

    public string TempUnitName = "";
    public string UnitGender = "";
    public string ReproductiveOrgans = "";//Reproductive Organs
    public float UnitTypeMultiplyer = 0f;
    public int UnitImageDropdownVal = 0;
    public int LifeDropdownVal = 0;
    public int TrainingDropdownVal = 0;
    public float TankSliderVal = 0f;
    public float RangerSliderVal = 0f;
    public float MentalSliderVal = 0f;
    public float TotalCost = 0f;

    private float HealthLVL;
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
        private float KnowladgeRetainLVL;



    void Start()
    {
        HealthSeed1 = new float[5];
        HealthSeed2 = new float[5];
        HealthSeed3 = new float[5];

        Afactor = Random.Range(1, 8) + Random.Range(0, 1) - Random.Range(0, 1); // Life encounters of disease riden or hazardus places places (0 = none 9 = its where you live)
        Bfactor = Random.Range(1, 8) + Random.Range(0, 1) - Random.Range(0, 1); // Genetic occurences of mutations (0 = none 9 = very damaged DNA)
        Cfactor = Random.Range(1, 8) + Random.Range(0, 1) - Random.Range(0, 1); // Life encounters of STDs (0 = none 9 = regularly)
        Dfactor = Random.Range(1, 8) + Random.Range(0, 1) - Random.Range(0, 1); // Immune system strength (0 = almost no immune system 9 = increadibly strong immune system)
        Efactor = Random.Range(1, 8) + Random.Range(0, 1) - Random.Range(0, 1); // Immune system reactiveness (attacking self -> functioning properly -> attacking nothing)

        Randomize(false,true);

        //HealthSeed = new float[5];
        //HealthSeed[0] = Afactor;
        //HealthSeed[1] = Bfactor;
        //HealthSeed[2] = Cfactor;
        //HealthSeed[3] = Dfactor;
        //HealthSeed[4] = Efactor;

        int GChance = Random.Range(0,100);
        if (GChance >= 0 && GChance < 60) { UnitGender = "Male"; ReproductiveOrgans = "Male"; }
        if (GChance >= 60 && GChance < 90) { UnitGender = "Female"; ReproductiveOrgans = "Female"; }
        if (GChance >= 90 && GChance <= 100) { UnitGender = "LGBTQ"; if (GChance >= 90 && GChance < 95) { ReproductiveOrgans = "Male"; } if (GChance >= 95 && GChance < 100) { ReproductiveOrgans = "Female"; } }
        if (Random.Range(0,50) < 48) {ReproductiveOrgans = "Sterile"; }//if (GChance >= 80 && GChance < 85) { UnitGender = "Female"; } if (GChance >= 85 && GChance < 90) { UnitGender = "Female"; } 

        Unit_Image = UnitImageContainer.transform.GetChild(0).gameObject.GetComponent("Image") as Image;
        Private_Image = UnitImageContainer.transform.GetChild(1).gameObject.GetComponent("Image") as Image;
        Officer_Image = UnitImageContainer.transform.GetChild(2).gameObject.GetComponent("Image") as Image;
        General_Image = UnitImageContainer.transform.GetChild(3).gameObject.GetComponent("Image") as Image;
        UnitTypeDropdown = UnitImageContainer.transform.GetChild(4).gameObject.GetComponent("Dropdown") as Dropdown;

        UnitNameInput = UnitNameLable.transform.GetChild(0).gameObject.GetComponent("TMP_InputField") as TMP_InputField;

        TankSlider = TrainingContainer.transform.GetChild(1).gameObject.GetComponent("Scrollbar") as Scrollbar;
        RangerSlider = TrainingContainer.transform.GetChild(3).gameObject.GetComponent("Scrollbar") as Scrollbar;
        MentalSlider = TrainingContainer.transform.GetChild(5).gameObject.GetComponent("Scrollbar") as Scrollbar;

        LifeDropdown = BackStoryDropdownsText.transform.GetChild(0).gameObject.GetComponent("TMP_Dropdown") as TMP_Dropdown;
        TrainingDropdown = BackStoryDropdownsText.transform.GetChild(1).gameObject.GetComponent("TMP_Dropdown") as TMP_Dropdown;

        //StatisticsPannel
        HealthStatText = StatisticsPannel.transform.GetChild(0).gameObject.GetComponent("TMP_Text") as TMP_Text;
        LifeSpanText = HealthStatText.transform.GetChild(0).gameObject.GetComponent("TMP_Text") as TMP_Text;
        DietGradeText = HealthStatText.transform.GetChild(1).gameObject.GetComponent("TMP_Text") as TMP_Text;
        SleepGradeText = HealthStatText.transform.GetChild(2).gameObject.GetComponent("TMP_Text") as TMP_Text;
        IllnessResistText = HealthStatText.transform.GetChild(3).gameObject.GetComponent("TMP_Text") as TMP_Text;
        IllnessesText = HealthStatText.transform.GetChild(4).gameObject.GetComponent("TMP_Text") as TMP_Text;
        InjuriesText = HealthStatText.transform.GetChild(5).gameObject.GetComponent("TMP_Text") as TMP_Text;

        MentalStatText = StatisticsPannel.transform.GetChild(1).gameObject.GetComponent("TMP_Text") as TMP_Text;
        EngineeringText = MentalStatText.transform.GetChild(0).gameObject.GetComponent("TMP_Text") as TMP_Text;
        MedicalText = MentalStatText.transform.GetChild(1).gameObject.GetComponent("TMP_Text") as TMP_Text;
        ScienceText = MentalStatText.transform.GetChild(2).gameObject.GetComponent("TMP_Text") as TMP_Text;
        StratagyText = MentalStatText.transform.GetChild(3).gameObject.GetComponent("TMP_Text") as TMP_Text;

        StrengthText = StatisticsPannel.transform.GetChild(2).gameObject.GetComponent("TMP_Text") as TMP_Text;
        SwordsmanText = StrengthText.transform.GetChild(0).gameObject.GetComponent("TMP_Text") as TMP_Text;
        BowmanText = StrengthText.transform.GetChild(1).gameObject.GetComponent("TMP_Text") as TMP_Text;
        CarryCapacityText = StrengthText.transform.GetChild(2).gameObject.GetComponent("TMP_Text") as TMP_Text;

        PerseptionStatText = StatisticsPannel.transform.GetChild(3).gameObject.GetComponent("TMP_Text") as TMP_Text;
        SightDistanceText = PerseptionStatText.transform.GetChild(0).gameObject.GetComponent("TMP_Text") as TMP_Text;
        SightAccuracyText = PerseptionStatText.transform.GetChild(1).gameObject.GetComponent("TMP_Text") as TMP_Text;

        StaminaStatText = StatisticsPannel.transform.GetChild(4).gameObject.GetComponent("TMP_Text") as TMP_Text;
        SpeedRangeText = StaminaStatText.transform.GetChild(0).gameObject.GetComponent("TMP_Text") as TMP_Text;
        SustainedRunText = StaminaStatText.transform.GetChild(1).gameObject.GetComponent("TMP_Text") as TMP_Text;

        MoralStatText = StatisticsPannel.transform.GetChild(5).gameObject.GetComponent("TMP_Text") as TMP_Text;
        MoralCapText = MoralStatText.transform.GetChild(0).gameObject.GetComponent("TMP_Text") as TMP_Text;
        TraumaResistText = MoralStatText.transform.GetChild(1).gameObject.GetComponent("TMP_Text") as TMP_Text;
        StressResistText = MoralStatText.transform.GetChild(2).gameObject.GetComponent("TMP_Text") as TMP_Text;

        LearningText = StatisticsPannel.transform.GetChild(6).gameObject.GetComponent("TMP_Text") as TMP_Text;
        LearnSpeedText = LearningText.transform.GetChild(0).gameObject.GetComponent("TMP_Text") as TMP_Text;
        RetainText = LearningText.transform.GetChild(1).gameObject.GetComponent("TMP_Text") as TMP_Text;

        ResetCancelFinishPannel = StatisticsPannel.transform.GetChild(8).gameObject;
        FinishButton = ResetCancelFinishPannel.transform.GetChild(0).gameObject.GetComponent("Button") as Button;
        CancelButton = ResetCancelFinishPannel.transform.GetChild(1).gameObject.GetComponent("Button") as Button;
        ResetButton = ResetCancelFinishPannel.transform.GetChild(2).gameObject.GetComponent("Button") as Button;
        CostText = ResetCancelFinishPannel.transform.GetChild(3).gameObject.GetComponent("TMP_Text") as TMP_Text;
        AvailibleText = ResetCancelFinishPannel.transform.GetChild(4).gameObject.GetComponent("TMP_Text") as TMP_Text;

        //RectTransform BlankRect = new RectTransform();
        //Update_Unit_Creator_UI(BlankRect);
        Update_Stats();

    }

    void Update()
    {



    }

    public void Update_Unit_Creator_UI(RectTransform UpdatedBy)
    {
        //Debug.Log("UI Update");
        if (UpdatedBy == UnitTypeDropdown.gameObject.GetComponent("RectTransform") as RectTransform)
        {
            UnitImageDropdownVal = UnitTypeDropdown.value;
            if (UnitImageDropdownVal == 0)
            {
                Unit_Image.gameObject.SetActive(true);
                Private_Image.gameObject.SetActive(false);
                Officer_Image.gameObject.SetActive(false);
                General_Image.gameObject.SetActive(false);
                UnitTypeMultiplyer = 1f;
            }
            else if (UnitImageDropdownVal == 1)
            {
                Unit_Image.gameObject.SetActive(false);
                Private_Image.gameObject.SetActive(true);
                Officer_Image.gameObject.SetActive(false);
                General_Image.gameObject.SetActive(false);
                UnitTypeMultiplyer = 1.1f;
            }
            else if (UnitImageDropdownVal == 2)
            {
                Unit_Image.gameObject.SetActive(false);
                Private_Image.gameObject.SetActive(false);
                Officer_Image.gameObject.SetActive(true);
                General_Image.gameObject.SetActive(false);
                UnitTypeMultiplyer = 1.3f;
            }
            else if (UnitImageDropdownVal == 3)
            {
                Unit_Image.gameObject.SetActive(false);
                Private_Image.gameObject.SetActive(false);
                Officer_Image.gameObject.SetActive(false);
                General_Image.gameObject.SetActive(true);
                UnitTypeMultiplyer = 1.5f;
            }
        }
        else if (UpdatedBy == UnitNameInput.gameObject.GetComponent("RectTransform") as RectTransform)
        {
            TempUnitName = UnitNameInput.text; // Add name requirements
        }
        else if (UpdatedBy == TankSlider.gameObject.GetComponent("RectTransform") as RectTransform || UpdatedBy == RangerSlider.gameObject.GetComponent("RectTransform") as RectTransform || UpdatedBy == MentalSlider.gameObject.GetComponent("RectTransform") as RectTransform)
        {
            TankSliderVal = TankSlider.value;
            RangerSliderVal = RangerSlider.value;
            MentalSliderVal = MentalSlider.value;
        }
        else if (UpdatedBy == LifeDropdown.gameObject.GetComponent("RectTransform") as RectTransform || UpdatedBy == TrainingDropdown.gameObject.GetComponent("RectTransform") as RectTransform)
        {
            LifeDropdownVal = LifeDropdown.value;
            TrainingDropdownVal = TrainingDropdown.value;
        }
        else if (UpdatedBy == ResetButton.gameObject.GetComponent("RectTransform") as RectTransform) //Randomize false true
        {
            Randomize(false, true);
        }
        else if (UpdatedBy == CancelButton.gameObject.GetComponent("RectTransform") as RectTransform) //Randomize false true
        {
            Destroy(gameObject);
        }
        else if (UpdatedBy == FinishButton.gameObject.GetComponent("RectTransform") as RectTransform) //Randomize false true
        {
            
        }

        Update_Stats();

    }

    private void Update_Stats() 
    {

        if (LifeDropdownVal == 0)//peasent
        {

            HealthLVL = 4;
            LifeSpan = 1;
            DietGrade = 1;
            SleepGrade = 1;
            IllnessResist = 1;

            Afactor = HealthSeed1[0];
            Bfactor = HealthSeed1[1];
            Cfactor = HealthSeed1[2];
            Dfactor = HealthSeed1[3];
            Efactor = HealthSeed1[4];

            Illnessess = new List<string>();

            MentalLVL = 0;
            EngineeringLVL = 0;
            MedicalLVL = 0;
            ScienceLVL = 0;
            StratagyLVL = 0;

            StrengthLVL = 3;
            SwordsmanLVL = 1;
            BowmanLVL = 0;
            CarryCapacity = 30; // Lbs - must be realistic in all curcumstances; 10 - 15 = 0, 100+ = 10

            PerseptionLVL = 2; // in miles (maximum is 1.6 mi from 5 ft off the ground distinguishing a candle light)
                               //SightDistance = 20 + (Random.Range(0,1) * 5) - (Random.Range(0, 1) * 5);
                               //EyeHealth = new List<float>();
                               //EyeHealth.Add(15 + Random.Range(0,5)); EyeHealth.Add(15 + Random.Range(0, 5)); // 0 = Right (0-20) / 1 = Left (0 - 20)

            StaminaLVL = 2;
            //SpeedRange = new List<float>();
            //SpeedRange.Add(Random.Range(0.75f, 1)); // 0 = slowest
            //float[] a = SpeedRange.ToArray();
            //SpeedRange.Add(Random.Range(a[0], a[0] + 0.5f)); // 1 = fastest in MPH
            //a = SpeedRange.ToArray();
            //SpeedRange.Add(Random.Range(a[0], 3));
            //EnduranceDistances = new List<float>();
            //EnduranceDistances.Add((0.1f) / (a[0] / 60));             // 0 = 1/10mi, in seconds
            //EnduranceDistances.Add(1 / (((a[0] + a[1]) / 2.0f) / 60)); // 1 = 1mi, in seconds
            //EnduranceDistances.Add((10f) / (a[1] / 60));              // 2 = 10 mi, in seconds

            MoralLVL = 0;
            MoralCap = 0;
            TraumaResist = 0;
            StressResist = 0;

            LearningLVL = 0;
            LearnSpeed = 0;
            KnowladgeRetainLVL = 0;


        }
        else if (LifeDropdownVal == 1)//countery boy
        {

            HealthLVL = 8;
            LifeSpan = 2;
            DietGrade = 2;
            SleepGrade = 2;
            IllnessResist = 2;

            Afactor = HealthSeed2[0];
            Bfactor = HealthSeed2[1];
            Cfactor = HealthSeed2[2];
            Dfactor = HealthSeed2[3];
            Efactor = HealthSeed2[4];

            Illnessess = new List<string>();

            MentalLVL = 2;
            EngineeringLVL = 2;
            MedicalLVL = 0;
            ScienceLVL = 0;
            StratagyLVL = 0;

            StrengthLVL = 4;
            SwordsmanLVL = 1;
            BowmanLVL = 1;
            CarryCapacity = 40; // Lbs - must be realistic in all curcumstances; 10 - 15 = 0, 100+ = 10

            PerseptionLVL = 3; // in miles (maximum is 1.6 mi from 5 ft off the ground distinguishing a candle light)
                               //SightDistance = 0;
                               //EyeHealth = new List<float>();
                               //EyeHealth.Add(30 + Random.Range(-5, 25) + Random.Range(-15, 25)); // 0 = Right (0-20) / 1 = Left (0 - 20)

            StaminaLVL = 0;
            //SpeedRange = new List<float>();
            //SpeedRange.Add(Random.Range(1, 1.5f)); // 0 = slowest
            //float[] b = SpeedRange.ToArray();
            //SpeedRange.Add(Random.Range(b[0], b[0] + 1f)); // 1 = fastest
            //b = SpeedRange.ToArray();
            //SpeedRange.Add(Random.Range(b[0], 3));
            //EnduranceDistances = new List<float>();
            //EnduranceDistances.Add((0.1f) / (b[0] / 60));             // 0 = 1/10mi, in seconds
            //EnduranceDistances.Add(1 / (((b[0] + b[1]) / 2.0f) / 60)); // 1 = 1mi, in seconds
            //EnduranceDistances.Add((10f) / (b[1] / 60));              // 2 = 10 mi, in seconds

            MoralLVL = 5;
            MoralCap = 2;
            TraumaResist = 1;
            StressResist = 2;

            LearningLVL = 4;
            LearnSpeed = 2;
            KnowladgeRetainLVL = 2;

        }
        else if (LifeDropdownVal == 2)//aristocrat
        {

            HealthLVL = 14;
            LifeSpan = 3;
            DietGrade = 4;
            SleepGrade = 4;
            IllnessResist = 3;

            Afactor = HealthSeed3[0];
            Bfactor = HealthSeed3[1];
            Cfactor = HealthSeed3[2];
            Dfactor = HealthSeed3[3];
            Efactor = HealthSeed3[4];

            // Illness: C = chroninc, I = inactive, A = Alergy, P = Persistant

            // Injuries: U = untreated, T = Treated, H = healing

            Illnessess = new List<string>();

            if (HealthSeed3[0] <= 4 || HealthSeed3[2] <= 6 && HealthSeed3[4] >= 4) // STD (Abiguous)
            {

                if (ReproductiveOrgans == "Male") { Illnessess.Add("STD_P"); }
                else if (ReproductiveOrgans == "Female" || ReproductiveOrgans == "Sterile") { Illnessess.Add("STD_I"); }

            }
            if ((HealthSeed3[0] <= 7) || (HealthSeed3[3] >= 3) && (HealthSeed3[4] >= 3))// Pox and Shingles
            {

                if (Random.Range(0, 10) == 10) { Illnessess.Add("Pox&Shingles_C"); }

            }

            MentalLVL = 5;
            EngineeringLVL = 1;
            MedicalLVL = 1;
            ScienceLVL = 1;
            StratagyLVL = 3;

            StrengthLVL = 5;
            SwordsmanLVL = 2;
            BowmanLVL = 2;
            CarryCapacity = 35; // Lbs - must be realistic in all curcumstances; 10 - 15 = 0, 100+ = 10

            PerseptionLVL = 4; // in miles (maximum is 1.6 mi from 5 ft off the ground distinguishing a candle light)
                               //SightDistance = 0;
                               //EyeHealth = new List<float>();
                               //EyeHealth.Add(40 + Random.Range(-10, 30) + Random.Range(-20, 30)); // 0 = Right (0-20) / 1 = Left (0 - 20)

            StaminaLVL = 3;
            //SpeedRange = new List<float>();
            //SpeedRange.Add(Random.Range(0.5f, 2.5f)); // 0 = slowest
            //float[] c = SpeedRange.ToArray();
            //SpeedRange.Add(Random.Range(c[0], c[0] + 0.5f)); // 1 = fastest
            //c = SpeedRange.ToArray();
            //SpeedRange.Add(Random.Range(c[0], 3));
            //EnduranceDistances = new List<float>();
            //EnduranceDistances.Add((0.1f) / (c[0] / 60));             // 0 = 1/10mi, in seconds
            //EnduranceDistances.Add(1 / (((c[0] + c[1]) / 2.0f) / 60)); // 1 = 1mi, in seconds
            //EnduranceDistances.Add((10f) / (c[1] / 60));              // 2 = 10 mi, in seconds

            MoralLVL = 6;
            MoralCap = 3;
            TraumaResist = 2;
            StressResist = 1;

            LearningLVL = 6;
            LearnSpeed = 3;
            KnowladgeRetainLVL = 3;

        }

        if (TrainingDropdownVal == 0)
        {

            StrengthLVL = StrengthLVL + 2 + ((5 - StrengthLVL) * TankSliderVal) + ((3 - StrengthLVL) * RangerSliderVal);
            SwordsmanLVL = SwordsmanLVL + 1 + ((3 - SwordsmanLVL) * TankSliderVal);
            BowmanLVL = BowmanLVL + 1 + ((3 - BowmanLVL) * RangerSliderVal);
            if (CarryCapacity < 35) { CarryCapacity = CarryCapacity + ((35 - CarryCapacity) * TankSliderVal); }

        }
        else if (TrainingDropdownVal == 1)
        {

            StrengthLVL = StrengthLVL + 5 + ((12 - StrengthLVL) * TankSliderVal) + ((5 - StrengthLVL) * RangerSliderVal);
            SwordsmanLVL = SwordsmanLVL + 2 + ((5 - SwordsmanLVL) * TankSliderVal);
            BowmanLVL = BowmanLVL + 2 + ((5 - BowmanLVL) * RangerSliderVal);
            if (CarryCapacity < 45) { CarryCapacity = CarryCapacity + 5 + ((40 - CarryCapacity) * TankSliderVal); }

        }
        else if (TrainingDropdownVal == 2)
        {

            StrengthLVL = StrengthLVL + 7 + ((17 - StrengthLVL) * TankSliderVal) + ((7 - StrengthLVL) * RangerSliderVal);
            SwordsmanLVL = SwordsmanLVL + 3 + ((7 - SwordsmanLVL) * TankSliderVal);
            BowmanLVL = BowmanLVL + 3 + ((7 - BowmanLVL) * RangerSliderVal);
            if (CarryCapacity < 50) { CarryCapacity = CarryCapacity + 5 + ((45 - CarryCapacity) * TankSliderVal); }

        }
        else if (TrainingDropdownVal == 3)
        {

            EngineeringLVL = EngineeringLVL + 5 + ((5 - EngineeringLVL) * MentalSliderVal);
            //MedicalLVL = 0;
            //ScienceLVL = 0;
            //StratagyLVL = 0;
            MentalLVL = EngineeringLVL + MedicalLVL + ScienceLVL + StratagyLVL;

        }
        else if (TrainingDropdownVal == 4)
        {

            //MentalLVL = 0;
            //EngineeringLVL = 0;
            MedicalLVL = MedicalLVL + 5 + ((4 - MedicalLVL) * MentalSliderVal);
            ScienceLVL = ScienceLVL + 1 + ((2 - ScienceLVL) * MentalSliderVal);
            //StratagyLVL = 0;
            MentalLVL = EngineeringLVL + MedicalLVL + ScienceLVL + StratagyLVL;

        }
        else if (TrainingDropdownVal == 5)
        {

            //MentalLVL = 0;
            //EngineeringLVL = 0;
            //MedicalLVL = 0;
            ScienceLVL = ScienceLVL + 5 + ((5 - ScienceLVL) * MentalSliderVal);
            //StratagyLVL = 0;
            MentalLVL = EngineeringLVL + MedicalLVL + ScienceLVL + StratagyLVL;

        }
        else if (TrainingDropdownVal == 6)
        {

            //MentalLVL = 0;
            //EngineeringLVL = 0;
            //MedicalLVL = 0;
            //ScienceLVL = 0;
            StratagyLVL = ScienceLVL + 5 + ((5 - ScienceLVL) * MentalSliderVal);
            MentalLVL = EngineeringLVL + MedicalLVL + ScienceLVL + StratagyLVL;

        }

        HealthStatText.text = "Health LVL: " + HealthLVL;
        LifeSpanText.text = "Lifespan Grade: " + LifeSpan;
        DietGradeText.text = "Diet Grade: " + DietGrade;
        SleepGradeText.text = "Sleep Grade: " + SleepGrade;
        IllnessResistText.text = "Illness Resist: " + IllnessResist;
        IllnessesText.text = "Illnessess: ";
        foreach (string Illness in Illnessess)
        {
            IllnessesText.text = IllnessesText.text + Illness;
            string[] TempArray = Illnessess.ToArray();
            if (Illness != TempArray[TempArray.Length - 1]) { IllnessesText.text = IllnessesText.text + " , "; }
        }
        InjuriesText.text = "Injuries: ";
        foreach (string Injury in CurrentInjuries)
        {
            InjuriesText.text = InjuriesText.text + Injury;
            string[] TempArray = CurrentInjuries.ToArray();
            if (Injury != TempArray[TempArray.Length - 1]) { InjuriesText.text = InjuriesText.text + " , "; }
        }

        MentalStatText.text = "Mental LVL: " + MentalLVL;
        EngineeringText.text = "Engineering LVL: " + EngineeringLVL;
        MedicalText.text = "Medical LVL: " + MedicalLVL;
        ScienceText.text = "Science LVL: " + ScienceLVL;
        StratagyText.text = "Stratagy LVL: " + StratagyLVL;

        StrengthText.text = "Strength LVL: " + StrengthLVL;
        SwordsmanText.text = "Swordsman LVL: " + SwordsmanLVL;
        BowmanText.text = "Bowman LVL: " + BowmanLVL;
        CarryCapacityText.text = "Carry Capacity LVL: " + CarryCapacity;

        PerseptionStatText.text = "Perseption LVL: " + PerseptionLVL;
        SightDistanceText.text = "Sight Dist: " + SightDistance + " Mi";
        SightAccuracyText.text = "Sight Accuracy: ";
        int x = 0;
        foreach (float Eye in EyeHealth)
        {
            x++;
            SightAccuracyText.text = SightAccuracyText.text + Eye;
            float[] TempArray = EyeHealth.ToArray();
            if (x != TempArray.Length) { SightAccuracyText.text = SightAccuracyText.text + " , "; }
        }

        StaminaStatText.text = "Stamina LVL: " + StaminaLVL;
        SpeedRangeText.text = "Speed Range: ";
        foreach (float Speed in SpeedRange)
        {
            SpeedRangeText.text = SpeedRangeText.text + (Mathf.Round(Speed * 10)) / 10;
            float[] TempArray = SpeedRange.ToArray();
            if (Speed != TempArray[TempArray.Length - 1]) { SpeedRangeText.text = SpeedRangeText.text + "/"; }
        }
        SustainedRunText.text = "Sustained Run: ";
        foreach (float Distance in EnduranceDistances)
        {
            SustainedRunText.text = SustainedRunText.text + (Mathf.Round(Distance * 10)) / 10;
            float[] TempArray = EnduranceDistances.ToArray();
            if (Distance != TempArray[TempArray.Length - 1]) { SustainedRunText.text = SustainedRunText.text + " , "; }
        }

        MoralStatText.text = "Stamina LVL: " + MoralLVL;
        MoralCapText.text = "Moral Cap: " + MoralCap;
        TraumaResistText.text = "Trauma Resist: " + TraumaResist;
        StressResistText.text = "Stress Resist: " + StressResist;

        LearningText.text = "Learning LVL: " + LearningLVL;
        LearnSpeedText.text = "Learn Speed: " + LearnSpeed;
        RetainText.text = "Retain LVL: " + KnowladgeRetainLVL;

    }

    private void Randomize(bool RandomName, bool RandomHSeed)
    {
        if (LifeDropdownVal == 2)//aristocrat
        {
            //Aristocarat
            if (Afactor != 0 && Afactor != 1) { HealthSeed1[0] = 0 + Random.Range(0, 1); } // Life encounters of disease riden or hazardus places places (0 = none 9 = its where you live)
            if (Bfactor != 5 && Bfactor != 9) { HealthSeed1[1] = 5 + Random.Range(0, 4); } // Genetic occurences of mutations (0 = none 9 = very damaged DNA)
            if (Cfactor != 0) { HealthSeed1[2] = 5 + Random.Range(0, 4); } // Life encounters of STDs (0 = none 9 = regularly)
            if (Dfactor != 9) { HealthSeed1[3] = Dfactor + Random.Range(0, 1); } // Immune system strength (0 = almost no immune system 9 = increadibly strong immune system)
            if (Efactor != 9) { HealthSeed1[4] = Efactor + Random.Range(0, 1); } // Immune system reactiveness (attacking self -> functioning properly -> attacking nothing)

            SightDistance = 0;
            EyeHealth = new List<float>();
            EyeHealth.Add(40 + Random.Range(-10, 30) + Random.Range(-20, 30)); // 0 = Right (0-20) / 1 = Left (0 - 20)

            SpeedRange = new List<float>();
            SpeedRange.Add(Random.Range(0.5f, 2.5f)); // 0 = slowest
            float[] c = SpeedRange.ToArray();
            SpeedRange.Add(Random.Range(c[0], c[0] + 0.5f) + 0.25f); // 1 = fastest
            c = SpeedRange.ToArray();
            EnduranceDistances = new List<float>();
            EnduranceDistances.Add((0.1f) / (c[0] / 60));             // 0 = 1/10mi, in seconds
            EnduranceDistances.Add(1 / (((c[0] + c[1]) / 2.0f) / 60)); // 1 = 1mi, in seconds
            EnduranceDistances.Add((10f) / (c[1] / 60));              // 2 = 10 mi, in seconds
        }
        else if (LifeDropdownVal == 1)//countery boy
        {
            if (Afactor != 0 && Afactor != 1) { HealthSeed2[0] = 0 + Random.Range(0, 1); } // Life encounters of disease riden or hazardus places places (0 = none 9 = its where you live)
            if (Bfactor != 5 && Bfactor != 9) { HealthSeed2[1] = 5 + Random.Range(0, 4); } // Genetic occurences of mutations (0 = none 9 = very damaged DNA)
            if (Cfactor != 0) { HealthSeed2[2] = 5 + Random.Range(0, 4); } // Life encounters of STDs (0 = none 9 = regularly)
            if (Dfactor != 9) { HealthSeed2[3] = Dfactor + Random.Range(0, 1); } // Immune system strength (0 = almost no immune system 9 = increadibly strong immune system)
            if (Efactor != 9) { HealthSeed2[4] = Efactor + Random.Range(0, 1); } // Immune system reactiveness (attacking self -> functioning properly -> attacking nothing)

            SightDistance = 0;
            EyeHealth = new List<float>();
            EyeHealth.Add(30 + Random.Range(-5, 25) + Random.Range(-15, 25)); // 0 = Right (0-20) / 1 = Left (0 - 20)

            SpeedRange = new List<float>();
            SpeedRange.Add(Random.Range(1, 1.5f)); // 0 = slowest
            float[] b = SpeedRange.ToArray();
            SpeedRange.Add(Random.Range(b[0], b[0] + 0.75f) + 0.25f); // 1 = fastest
            b = SpeedRange.ToArray();
            EnduranceDistances = new List<float>();
            EnduranceDistances.Add((0.1f) / (b[0] / 60));             // 0 = 1/10mi, in seconds
            EnduranceDistances.Add(1 / (((b[0] + b[1]) / 2.0f) / 60)); // 1 = 1mi, in seconds
            EnduranceDistances.Add((10f) / (b[1] / 60));              // 2 = 10 mi, in seconds
        }
        else if (LifeDropdownVal == 0)//peasent
        {
            if (Afactor <= 7) { HealthSeed3[0] = 7 + Random.Range(0, 2); } // Life encounters of disease riden or hazardus places places (0 = none 9 = its where you live)
            if (Bfactor <= 5) { HealthSeed3[1] = 5 + Random.Range(0, 4); } // Genetic occurences of mutations (0 = none 9 = very damaged DNA)
            if (Cfactor != 0) { HealthSeed3[2] = 5 + Random.Range(0, 4); } // Life encounters of STDs (0 = none 9 = regularly)
            if (Dfactor != 9) { HealthSeed3[3] = Dfactor + Random.Range(0, 1); } // Immune system strength (0 = almost no immune system 9 = increadibly strong immune system)
            if (Efactor != 9) { HealthSeed3[4] = Efactor + Random.Range(0, 1); } // Immune system reactiveness (attacking self -> functioning properly -> attacking nothing)

            SightDistance = 20 + (Random.Range(0, 1) * 5) - (Random.Range(0, 1) * 5);
            EyeHealth = new List<float>();
            EyeHealth.Add(15 + Random.Range(-5, 5)); EyeHealth.Add(15 + Random.Range(-5, 5)); // 0 = Right (0-20) / 1 = Left (0 - 20)

            SpeedRange = new List<float>();
            SpeedRange.Add(Random.Range(0.75f, 1)); // 0 = slowest
            float[] a = SpeedRange.ToArray();
            SpeedRange.Add(Random.Range(a[0], a[0] + 0.5f) + 0.25f); // 1 = fastest in MPH
            a = SpeedRange.ToArray();
            EnduranceDistances = new List<float>();
            EnduranceDistances.Add((0.1f) / (a[1] / 60));             // 0 = 1/10mi, in seconds
            EnduranceDistances.Add(1 / (((a[0] + a[1]) / 2.0f) / 60)); // 1 = 1mi, in seconds
            EnduranceDistances.Add((10f) / (a[0] / 60));              // 2 = 10 mi, in seconds
        }

    }

}
