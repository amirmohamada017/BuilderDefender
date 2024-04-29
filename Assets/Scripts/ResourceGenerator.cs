using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public static int NearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        var collider2Ds =
            Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

        var nearbyResourceAmount = 0;
        foreach (var collider2d in collider2Ds)
        {
            var resourceNode = collider2d.GetComponent<ResourceNode>();
            if (resourceNode != null && resourceNode.resourceType == resourceGeneratorData.resourceType)
                nearbyResourceAmount++;
        }
        
        return Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
    }
    
    private ResourceGeneratorData _resourceGeneratorData;
    private float _timer;
    private float _timerMax;
    private int _resourceAmount;

    private void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;

        _timerMax = _resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        var nearbyResourceAmount = NearbyResourceAmount(_resourceGeneratorData, transform.position);
        
        if (nearbyResourceAmount == 0)
            enabled = false;
        else
        {
            _resourceAmount = nearbyResourceAmount;
            _timerMax = _resourceGeneratorData.timerMax / _resourceAmount;
            _timer = _timerMax;
        }
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
    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return _resourceGeneratorData;
    }
}
