using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountChanged;

    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    private void Awake()
    {
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
            
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResourceTypeListSO resourceTypeList= Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            AddSource(resourceTypeList.list[0],5);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TestLogResourceAmountDictionary();
        }
    }

    private void TestLogResourceAmountDictionary()
    {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddSource(ResourceTypeSO resourceType,int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChanged? .Invoke(this, EventArgs.Empty);    //NULL CHECK - null değilse sağ taraf gerçekleşir
        //BUNLA AYNI ŞEY - if(OnResourceAmountChanged!=null){OnResourceAmountChanged(this,EventArgs.Empty)}
       
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }

}
