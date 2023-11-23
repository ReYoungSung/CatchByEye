using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCharacter : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 마우스 커서의 현재 화면 좌표를 가져옵니다.
        Vector3 mousePosition = Input.mousePosition;

        // 마우스 커서의 화면 좌표를 XZ 평면에 프로젝션합니다.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 worldPosition = ray.GetPoint(rayDistance);
            worldPosition.y = transform.position.y; // 캐릭터의 현재 y 위치를 유지합니다.

            // 캐릭터의 위치를 조정합니다.
            transform.position = worldPosition;
        }
    }
}
