using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject _spriteGameObject;
    private ResourceNearbyOverlay _resourceNearbyOverlay;

    private void Awake()
    {
        _spriteGameObject = transform.Find("Sprite").gameObject;
        _resourceNearbyOverlay = transform.Find("ResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender,
        BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.ActiveBuildingType == null)
        {
            Hide();
            _resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.ActiveBuildingType.sprite);
            _resourceNearbyOverlay.Show(e.ActiveBuildingType.resourceGeneratorData);
        }
    }

    private void Hide()
    {
        _spriteGameObject.SetActive(false);
    }
    private void Show(Sprite sprite)
    {
        _spriteGameObject.SetActive(true);
        _spriteGameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
