using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    
    private void Start()
    {
        var resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        var nearbyResourceAmount = ResourceGenerator.NearbyResourceAmount(resourceGeneratorData, transform.position);
        var resourceGeneratedPerSecond = nearbyResourceAmount / resourceGeneratorData.timerMax;
        transform.Find("Text").GetComponent<TextMeshPro>().text = resourceGeneratedPerSecond.ToString();
    }
}
