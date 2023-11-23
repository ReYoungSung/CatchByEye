using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNoCode : MonoBehaviour
{
    // 블록에 할당된 점수
    public int Score = 10;
    private bool isStart = false;
    public bool isActivated = false;

    private Renderer blockRenderer;
    private Color blueColor = Color.blue; // 파란색으로 바꿀 색상
    private Color redColor = Color.red;

    private float MaxRayDistance = 3f;
    private RaycastHit hit;

    private void Start()
    {
        // Renderer 컴포넌트를 가져옵니다.
        blockRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        activeNoCoding();
    } 

    private void activeNoCoding()
    {
        // 블록의 오른쪽 방향으로 ray를 발사합니다.
        Ray ray = new Ray(transform.position, -transform.forward);

        if (isStart || !isActivated) 
        {
            // Visualize the ray in the Scene view
            Debug.DrawRay(ray.origin, ray.direction * MaxRayDistance, Color.red);
            return;
        }
        else if (Physics.Raycast(ray, out hit, MaxRayDistance) && !hit.collider.CompareTag("Block2"))
        {
            ErrorBlock(); 
            return; 
        }

        // Check if the raycast hit something before accessing the hit variable
        if (hit.collider != null)
        {        
            // 충돌 중 플래그를 설정
            isStart = true; 
        
            // 블록을 활성화하고 점수를 추가
            ActivateBlock();
            hit.transform.GetComponent<BasicNoCode2>().Score += Score;
            hit.transform.GetComponent<BasicNoCode2>().ActivateBlock();
        }
    }

    // 다음 블록을 활성화하는 함수
    public void ActivateBlock()
    {
        isActivated = true;

        // 블록의 색상을 파란색으로 변경합니다.
        blockRenderer.material.color = blueColor;
    }

    public void ErrorBlock()
    {
        Debug.Log("노코드 연결 실패!! 다시 시도해보세요..!!");
        hit.transform.GetComponent<MeshRenderer>().material.color = redColor;
    }
}
