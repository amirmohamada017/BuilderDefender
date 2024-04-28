using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData _resourceGeneratorData;
    private Transform _textTransform;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        UpdateText();
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        gameObject.SetActive(true);
        _resourceGeneratorData = resourceGeneratorData;
        
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = _resourceGeneratorData.resourceType.sprite;
        _textTransform = transform.Find("Text");
        
        UpdateText();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateText()
    {
        var nearbyResourceAmount = ResourceGenerator.NearbyResourceAmount(_resourceGeneratorData, transform.position);
        var percent = Mathf.RoundToInt((float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount * 100);
        _textTransform.GetComponent<TextMeshPro>().text = $"{percent}%";
    }
}
