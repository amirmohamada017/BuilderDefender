using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    
    private Dictionary<BuildingTypeSO, Transform> _buttonTransformDictionary;
    private Transform _arrowButton;
    private void Awake()
    {
        var buttonTemplate = transform.Find("ButtonTemplate");
        buttonTemplate.gameObject.SetActive(false);
        var buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        _buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        
        const float offsetAmount = 120f;
        var index = 0;
        
        _arrowButton = Instantiate(buttonTemplate, transform);
        _arrowButton.gameObject.SetActive(true);
        _arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
        _arrowButton.Find("Image").GetComponent<Image>().sprite = arrowSprite;
        _arrowButton.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -40f);
        _arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;
        
        foreach (var buildingType in buildingTypeList.list)
        {
            var buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            buttonTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });
            
            _buttonTransformDictionary.Add(buildingType, buttonTransform);
            
            index++;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        
        UpdateActiveBuildingTypeButton();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender,
        BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton()
    {
        _arrowButton.Find("Selected").gameObject.SetActive(false);
        foreach (var buildingType in _buttonTransformDictionary.Keys)
        {
            var buttonTransform = _buttonTransformDictionary[buildingType];
            buttonTransform.Find("Selected").gameObject.SetActive(false);
        }
        
        var selectedBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (selectedBuildingType == null)
            _arrowButton.Find("Selected").gameObject.SetActive(true);
        else
            _buttonTransformDictionary[selectedBuildingType].Find("Selected").gameObject.SetActive(true);
    }
}
