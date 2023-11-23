using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLight : MonoBehaviour
{
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 200.0f;

    void Update()
    {
        Cursor.visible = false;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;

            Vector3 directionToTarget = targetPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            float step = rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        }   
    }
}
