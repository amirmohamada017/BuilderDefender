using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    
    private BuildingTypeSO _activeBuildingType;
    private BuildingTypeListSO _buildingTypeList;
    private Camera _mainCamera;

    private void Awake()
    {
        Instance = this;
        _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_activeBuildingType != null)
                Instantiate(_activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.E))
            _activeBuildingType = _buildingTypeList.list[0];
        else if (Input.GetKeyDown(KeyCode.R))
            _activeBuildingType = _buildingTypeList.list[1];
        else if (Input.GetKeyDown(KeyCode.T))
            _activeBuildingType = _buildingTypeList.list[2];
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mousePosition = Input.mousePosition;
        var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
    
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuildingType = buildingType;
    }
    
    public BuildingTypeSO GetActiveBuildingType()
    {
        return _activeBuildingType;
    }
}
