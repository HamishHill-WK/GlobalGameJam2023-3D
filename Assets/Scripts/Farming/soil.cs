using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script was written by Hamish Hill GitHub: @HamishHill-WK
//this script tracks the growth and moisture level of the soil object when planted 
//this script updates the soil mesh according to the plant's stage of growth 

public class soil : MonoBehaviour
{
    public bool plantable = false;
    public bool planted = false;

    public int monthPlanted = 0;
    private int monthsAfterPlant = 0;
    private float seasonMod = 0.0f;

    public float moisture = 100.0f;
    private float moistureMod = 1.0f;

    private int lastDay = 0;
    private int currentDay = 0;

    private int lastMonth = 0;
    private int currentMonth = 0;

    public float yield = 0.0f;
    private float yieldFactor = 0.0f;
    public float maxYield = 50.0f;

    private float growthFactor = 0.01f;

    private GameObject timer;

    private MeshFilter meshFilter;
    public Mesh initialMesh;
    public Mesh sproutMesh;
    public Mesh grownMesh;
    public Mesh deadMesh;
    public Mesh blankMesh;

    public enum growthStage { initial = 0, sprout, grown, dead, noPlant };    
    growthStage currentGrowthStage = growthStage.initial;

    enum moistureLevel { low = 0, medium, high };
    moistureLevel currentMoistureLevel = moistureLevel.low;

    Stack<moistureLevel> moistureLevels;

    void updateMoisture()
    {
        if (moisture <= 0.0f)
            return;

        float random = 0.0f;

        if (moisture > 100.0f)
            random = Random.Range(1.0f, 2.5f);

        if (moisture > 30.0f && moisture <= 100.0f)
        {
            random = Random.Range(0.1f, 0.8f);

            if(moisture <= 65.0f)
            {
                currentMoistureLevel = moistureLevel.medium;
            }            
            
            if(moisture > 65.0f)
            {
                currentMoistureLevel = moistureLevel.high;
            }
        }

        if (moisture <= 30.0f)
        {
            random = Random.Range(0.0f, 0.05f);
            currentMoistureLevel = moistureLevel.low;
        }
        moisture -= random;        
    }

    void updateMoistureMod()
    {
        if(moisture >= 20.0f)
            moistureMod = moisture / 100.0f;
    }

    void updateSeasonMod()
    {
        if (monthPlanted >= 3 && monthPlanted <= 5)
        {
            maxYield += 5.0f;
            seasonMod = 0.1f;
        }

        else
            seasonMod = 0.05f;
    }

    void updateYield()
    {
        if (yield < maxYield && planted)
        {
            yield += (growthFactor + seasonMod + moistureMod);

            if (yield > (yieldFactor + 5.0f))
                yieldFactor = Mathf.Round(yield);
        }

        if (monthsAfterPlant < 1)
        {
            updateMesh(growthStage.initial);
            return;
        }

        if (monthsAfterPlant == 1)
        {
            updateMesh(growthStage.sprout);
            return;
        }

        if(monthsAfterPlant > 1 && monthsAfterPlant < 6)
        {
            updateMesh(growthStage.grown);
        }        
        
        if(monthsAfterPlant >= 6 )
        {
            updateMesh(growthStage.dead);
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

                //meshFilter.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
                meshFilter.mesh = sproutMesh;
                break;

            case growthStage.grown:
                meshFilter.mesh = grownMesh;
                break;

            case growthStage.dead:
                meshFilter.mesh = deadMesh;
                break;            
                    
            case growthStage.noPlant:
                meshFilter.mesh = blankMesh;
                break;
        }
    }

    void updateMaxYield()
    {
        float lowCount = 0.0f;
        float medCount = 0.0f;
        float highCount = 0.0f;

        foreach (moistureLevel ML in moistureLevels)
        {
            if (ML == moistureLevel.low)
                lowCount++;

            if (ML == moistureLevel.medium)
                medCount++;

            if (ML == moistureLevel.high)
                highCount++;
        }

        switch (currentGrowthStage)
        {
            case growthStage.initial:
                maxYield += (1.5f * lowCount) + (2.0f * medCount) + (2.5f * highCount);
            break;

            case growthStage.sprout:
                maxYield += (1.0f * lowCount) + (1.5f * medCount) + (2.0f * highCount);
            break;

            case growthStage.grown:
                if (maxYield < 100.0f)
                    maxYield += (0.5f * lowCount) + (1.0f * medCount) + (1.5f * highCount);
            break;

            case growthStage.dead:
                yield = 0;
                maxYield = 0;
            break;
        }    

        if (maxYield > 100.0f)
            maxYield = 100.0f;
    }

    public void addMoisture()
    {
        if(moisture < 100.0f)
            moisture += 10.0f;
    }

    public void plantPot()
    {
        planted = true;
        monthPlanted = timer.GetComponent<timeTracking>().getCurrentTime().monthNum;
        monthsAfterPlant = 0;
        yieldFactor = 0;
        updateSeasonMod();
        moistureLevels.Clear();
        moistureLevels.Push(currentMoistureLevel);
    }

    void Start()
    {
        timer = GameObject.Find("Timer");
        TreeData data = new TreeData(this, timer.GetComponent<timeTracking>());
        lastDay = data.day;
        currentDay = lastDay;
        lastMonth = data.currentMonth;
        currentMonth = lastMonth;
        monthPlanted = data.monthPlanted;
        planted = data.planted;
        yield = data.currentYield;
        yieldFactor = Mathf.Round(yield);
        meshFilter = this.GetComponent<MeshFilter>();
        moistureLevels = new Stack<moistureLevel>();

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

            if(planted && currentGrowthStage != growthStage.dead)
                updateYield();

            if (currentGrowthStage == growthStage.dead)
                updateMaxYield();
        }

        if(currentMonth != lastMonth)
        {
            lastMonth = currentMonth;

            if (planted)
            {
                monthsAfterPlant++;

                moistureLevels.Push(currentMoistureLevel);
                updateMaxYield();
            }
        }

        if (plantable)
            planted = false;
    }
}