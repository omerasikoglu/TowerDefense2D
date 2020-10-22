using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    private Dictionary<BuildingTypeSO, Transform> btnTransformDictionary;
    private Transform arrowButtonTransform;
    private void Awake()
    {
        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        arrowButtonTransform = Instantiate(btnTemplate, transform);
        arrowButtonTransform.gameObject.SetActive(true);

        float offsetAmount = +120f; // offset=öteleme
        arrowButtonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        arrowButtonTransform.Find("image").GetComponent<Image>().sprite = arrowSprite;
        arrowButtonTransform.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30f);
        arrowButtonTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            //offsetAmount = +120f; // offset=öteleme
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;
            //resourceTypeTransformDictionary[resourceType] = btnTransform;
            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            btnTransformDictionary[buildingType] = btnTransform;

            index++;
        }
    }
    private void Update()
    {
        UpdateActiveBuidingTypeIcon();
    }
    private void UpdateActiveBuidingTypeIcon()
    {
        arrowButtonTransform.Find("selected").gameObject.SetActive(false);

        foreach (BuildingTypeSO buildingType in btnTransformDictionary.Keys)
        {
            Transform btnTransform = btnTransformDictionary[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null)
        {
            arrowButtonTransform.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }




    }


}
