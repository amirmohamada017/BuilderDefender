using System;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject _spriteGameObject;

    private void Awake()
    {
        _spriteGameObject = transform.Find("Sprite").gameObject;
        
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
            Hide();
        else
            Show(e.ActiveBuildingType.sprite);
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
