using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    
    public event EventHandler OnResourceAmountChanged; 
    
    private Dictionary<ResourceTypeSO, int> _resourceAmountDictionary;

    private void Awake()
    {
        Instance = this;
        _resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        
        var resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO)).list;

        foreach (var resourceType in resourceTypeList)
        {
            _resourceAmountDictionary.Add(resourceType, 200);
        }
    }
    
    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourceAmountDictionary[resourceType] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return _resourceAmountDictionary[resourceType];
    }
    
    private bool CanAfford(ResourceTypeSO resourceType, int amount)
    {
        return GetResourceAmount(resourceType) >= amount;
    }

    public bool CanAfford(ResourceAmount[] resourceAmounts)
    {
        foreach (var resourceAmount in resourceAmounts)
        {
            if (!CanAfford(resourceAmount.resourceType, resourceAmount.amount))
                return false;
        }
        return true;
    }
    
    public void SpendResources(ResourceAmount[] resourceAmounts)
    {
        foreach (var resourceAmount in resourceAmounts)
        {
            AddResource(resourceAmount.resourceType, -resourceAmount.amount);
        }
    }
}
