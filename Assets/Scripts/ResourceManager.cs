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
            _resourceAmountDictionary.Add(resourceType, 0);
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
}
