﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    //[SerializeField] private Transform mouseVisualTransform;
    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;
 

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        // mouseVisualTransform.position = GetMouseWorldPosition();

        if (Input.GetMouseButtonDown(0) && activeBuildingType!=null && !EventSystem.current.IsPointerOverGameObject())
        {
            Instantiate(activeBuildingType.prefab,GetMouseWorldPosition(), Quaternion.identity);
            SetActiveBuildingType(null);
        }
      
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
    }
    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

}