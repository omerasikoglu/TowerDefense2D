﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using System.Runtime.CompilerServices;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float orthographicSize;
    private float targetOrthographicSize;

    private void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }
    private void Update()
    {
        HandleCamera();
        HandleZoom();
    }
    private void HandleZoom()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 movDir = new Vector2(x, y).normalized;
        float moveSpeed = 5f;
        transform.position += (Vector3)movDir * moveSpeed * Time.deltaTime;//deltatime framerate den bagımsız oldugunu belirtiyor
    }
    private void HandleCamera()
    {
        float zoomAmount = 2f;
        targetOrthographicSize -= Input.mouseScrollDelta.y * zoomAmount;


        float minOrthographicSize = 5f;
        float maxOrthographicSize = 20f;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }
}
