using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script was written by Hamish Hill GitHub: @HamishHill-WK
//this script tracks the growth and moisture level of the soil object when planted 
//this script updates the soil mesh according to the plant's stage of growth 

public class TreeGrowth : MonoBehaviour
{
    private int monthsAfterPlant = 0;   //how many months the plant has been alive
    private float seasonMod = 0.0f;

    public float moisture = 1000.0f;
    private float moistureMod = 1.0f;

    private int lastDay = 0;
    private int currentDay = 0;
    private int lastMonth = 0;
    private int currentMonth = 0;

    public float Growth = 0.0f;             //current growth of the plant
    public float GrowthCap = 50.0f;          //growth cap 
    [SerializeField] private float MaxGrowth = 1000.0f;
    private float growthFactor = 0.01f;
    private float FertiliserLevel;

    private GameObject timer;

    private MeshFilter meshFilter;
    public Mesh initialMesh;
    public Mesh sproutMesh;
    public Mesh grownMesh;
    public Mesh deadMesh;
    public Mesh blankMesh;

    private GameObject smallTree;
    private GameObject medTree;
    private GameObject bigTree;

    public enum growthStage { initial = 0, sprout, grown, dead, noPlant };    
    growthStage currentGrowthStage = growthStage.initial;

    enum moistureLevel { low = 0, medium, high };
    moistureLevel currentMoistureLevel = moistureLevel.low;

    void Start()
    {
        smallTree = GameObject.Find("smalltree");
        medTree = GameObject.Find("mediumtree");
        bigTree = GameObject.Find("bigtree");

        medTree.SetActive(false);
        bigTree.SetActive(false);

        SetData();
        meshFilter = this.GetComponent<MeshFilter>();
        plantPot();
    }

    void Update()
    {
        currentDay = timer.GetComponent<timeTracking>().getCurrentTime().day;
        currentMonth = timer.GetComponent<timeTracking>().getCurrentTime().monthNum;
        if (currentDay != lastDay)
        {
            lastDay = currentDay;

            updateMoisture();

            updateMoistureMod();

            if (currentGrowthStage != growthStage.dead)
                updateYield();

            if (currentGrowthStage == growthStage.dead)
                updateMaxYield();
        }

        if (currentMonth != lastMonth)
        {
            lastMonth = currentMonth;
            monthsAfterPlant++;
            updateMaxYield();
        }
    }

    void updateMoisture()
    {
        if (moisture <= 0.0f)
            return;

        float random = 0.0f;

        if (moisture > 800.0f)
            random = Random.Range(0.5f, 1.5f);

        if (moisture > 300.0f && moisture <= 1000.0f)
        {
            random = Random.Range(0.1f, 0.8f);

            if(moisture <= 650.0f){
                currentMoistureLevel = moistureLevel.medium;
            }            
            
            if(moisture > 650.0f){
                currentMoistureLevel = moistureLevel.high;
            }
        }

        if (moisture <= 300.0f) {
            random = Random.Range(0.0f, 0.05f);
            currentMoistureLevel = moistureLevel.low;
        }
        moisture -= random;        
    }

    void updateMoistureMod()
    {
        if(moisture >= 300.0f)
            moistureMod = moisture / 1000.0f;

        if (moisture < 300.0f)
            moistureMod = moisture / 2000.0f;
    }

    void updateSeasonMod()
    {
        if (currentMonth >= 3 && currentMonth <= 9){
            GrowthCap += 5.0f;
            seasonMod = 0.2f;
        }

        else{
            GrowthCap += 5.0f;
            seasonMod = 0.05f;
        }
    }

    public void updateMesh(growthStage stage)
    {
        currentGrowthStage = stage;

        switch (currentGrowthStage)
        {
            case growthStage.initial:
                meshFilter.mesh = initialMesh;
                break;

            case growthStage.sprout:
                smallTree.SetActive(false);
                medTree.SetActive(true);
                break;

            case growthStage.grown:
                medTree.SetActive(false);
                bigTree.SetActive(true);
                break;

            case growthStage.dead:
                meshFilter.mesh = deadMesh;
                break;

            case growthStage.noPlant:
                meshFilter.mesh = blankMesh;
                break;
        }
    }


    void updateYield()
    {
        if (Growth < GrowthCap) {
            float diff;
            diff = GrowthCap - Growth;
            float diffPrcnt;
            diffPrcnt = (diff / GrowthCap);

            Growth += ((growthFactor + seasonMod + moistureMod) *  diffPrcnt);
        }

        float prcnt;
        prcnt = (Growth / MaxGrowth) * 100;

        if (prcnt < 33.3) {
            updateMesh(growthStage.initial);
            return;
        }

        if (prcnt >= 33.4 && prcnt <= 66.6) {
            updateMesh(growthStage.sprout);
            return;
        }

        if(prcnt >=66.7) 
            updateMesh(growthStage.grown);
    }

    void updateMaxYield()
    {
        float diff;
        diff = GrowthCap - Growth;
        if (diff < 0)
            diff = 0.1f;
        float diffPrcnt;
        diffPrcnt = (diff / GrowthCap);

        switch (currentGrowthStage)
        {
            case growthStage.initial:
                GrowthCap += ((1.5f * moistureMod) / diffPrcnt);
            break;

            case growthStage.sprout:
                GrowthCap += ((1.0f * moistureMod) / diffPrcnt);
                break;

            case growthStage.grown:
                if (GrowthCap < MaxGrowth)
                    GrowthCap += ((0.5f * moistureMod) / diffPrcnt);
                break;

            case growthStage.dead:
                Growth = 0;
                GrowthCap = 0;
            break;
        }    

        if (GrowthCap > MaxGrowth)
            GrowthCap = MaxGrowth;
    }

    public void addMoisture()
    {
        if(moisture < 1000.0f)
            moisture += 100.0f;
    }

    public void addMoisture(float amount)
    {
        if (moisture < 1000.0f)
            moisture += amount;
    }    

    private void SetData()
    {
        timer = GameObject.Find("Timer");
        TreeData data = new TreeData(timer.GetComponent<timeTracking>());
        lastDay = data.day;
        currentDay = lastDay;
        lastMonth = data.currentMonth;
        currentMonth = lastMonth;  
    }
    public void plantPot()
    {
        monthsAfterPlant = 0;
        Growth = 0;
        updateSeasonMod();
    }

    private void updateFert()
    {
        if(FertiliserLevel < 0)
            FertiliserLevel -= .1f;
    }

    public void addFetiliser()
    {
        FertiliserLevel = 100.0f;
    }
}