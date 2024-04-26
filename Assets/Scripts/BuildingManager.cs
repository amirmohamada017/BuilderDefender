using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO ActiveBuildingType;
    }
    
    private BuildingTypeSO _activeBuildingType;
    private BuildingTypeListSO _buildingTypeList;

    private void Awake()
    {
        Instance = this;
        _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_activeBuildingType != null)
                Instantiate(_activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.E))
            _activeBuildingType = _buildingTypeList.list[0];
        else if (Input.GetKeyDown(KeyCode.R))
            _activeBuildingType = _buildingTypeList.list[1];
        else if (Input.GetKeyDown(KeyCode.T))
            _activeBuildingType = _buildingTypeList.list[2];
    }
    
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuildingType = buildingType;

        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs
        {
            ActiveBuildingType = _activeBuildingType
        });
    }
    
    public BuildingTypeSO GetActiveBuildingType()
    {
        return _activeBuildingType;
    }
}
