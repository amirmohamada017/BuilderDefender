using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private BuildingTypeSO _buildingType;
    private BuildingTypeListSO _buildingTypeList;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;

        _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        _buildingType = _buildingTypeList.list[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Instantiate(_buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);

        
        if (Input.GetKeyDown(KeyCode.E))
            _buildingType = _buildingTypeList.list[0];
        else if (Input.GetKeyDown(KeyCode.R))
            _buildingType = _buildingTypeList.list[1];
        else if (Input.GetKeyDown(KeyCode.T))
            _buildingType = _buildingTypeList.list[2];

    }

    private Vector3 GetMouseWorldPosition()
    {
        var mousePosition = Input.mousePosition;
        var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
