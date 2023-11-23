using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNoCode4 : MonoBehaviour 
{
    // 블록에 할당된 점수
    public int Score = 10;
    private bool isStart = false;
    public bool isActivated = false;

    private Renderer blockRenderer;
    private Color blueColor = Color.blue; // 파란색으로 바꿀 색상

    private void Start()
    {
        // Renderer 컴포넌트를 가져옵니다.
        blockRenderer = GetComponent<MeshRenderer>();
    }

    // 다음 블록을 활성화하는 함수
    public void ActivateBlock()
    {
        isActivated = true;

        Debug.Log("노코드 연결 성공!! 최종 스코어 : "+Score);
        // 블록의 색상을 파란색으로 변경합니다.
        blockRenderer.material.color = blueColor;
    }
}
