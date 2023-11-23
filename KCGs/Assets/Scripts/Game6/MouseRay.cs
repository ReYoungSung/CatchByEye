using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{
    public Color rayColor = Color.red; // Ray의 색상
    //public GameObject detectionArea; // 인식 범위를 나타내는 빈 오브젝트

    private AudioSource removeRobberSound;
    // 문 닫히는 소리를 재생할 AudioSource


    
    private LineRenderer lineRenderer;
    private Ray ray;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        removeRobberSound = GetComponent<AudioSource>();
        lineRenderer.startColor = rayColor;
        lineRenderer.endColor = rayColor;
        
    }

    private void FixedUpdate(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.origin + ray.direction * 10000f); // Ray의 길이를 조정할 경우 수정 가능

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            
            if (hitObject.CompareTag("Robber"))
            {
                Debug.Log("Robber"); 
                Destroy(hitObject);
                removeRobberSound.Play();

                Transform parentTransform = hitObject.transform.parent;
                if (parentTransform != null)
                {
                    parentTransform.tag = "UnRobber";
                }
            }
        }
    }
}