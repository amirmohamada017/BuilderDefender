using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceTypeSO, int> _resourceAmountDictionary;

    private void Awake()
    {
        Instance = this;
        _resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        
        var resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));

        foreach (var resourceType in resourceTypeList.list)
        {
            _resourceAmountDictionary[resourceType] = 0;
        }
    }
    
    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourceAmountDictionary[resourceType] += amount;
    }
}
