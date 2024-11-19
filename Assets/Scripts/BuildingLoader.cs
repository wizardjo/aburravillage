using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class BuildingLoader : MonoBehaviour
{
    [SerializeField]
    private BuildingData buildingData;

    [SerializeField]
    private SpriteRenderer mainSprite;

    private void Start()
    {
        mainSprite.sprite = buildingData.sprite;
    }
}
