using System;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData _resourceGeneratorData;
    private float _timer;
    private float _timerMax;

    private void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;

        _timerMax = _resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        var collider2Ds =
            Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.resourceDetectionRadius);

        var nearbyResourceAmount = 0;
        foreach (var collider2d in collider2Ds)
        {
            var resourceNode = collider2d.GetComponent<ResourceNode>();
            if (resourceNode != null && resourceNode.resourceType == _resourceGeneratorData.resourceType)
                nearbyResourceAmount++;
        }
        
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, _resourceGeneratorData.maxResourceAmount);

        if (nearbyResourceAmount == 0)
            enabled = false;
        else
        {
            _timerMax = (_resourceGeneratorData.timerMax / 2f) + _resourceGeneratorData.timerMax *
                (1 - (float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount);
        }

        Debug.Log(nearbyResourceAmount);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _timerMax;

            ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType, 1);
        }
    }
}
