using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<Transform> targets; // 이동할 대상 오브젝트
    public float movementSpeed = 5f; // 이동 속도
    public float rotationSpeed = 1.5f; // 로테이션 속도

    private bool isMoving = false; // 이동 중인지 여부

    private Quaternion originalRotation; // 초기 로테이션 값

    public Coroutine isMovingCoroutine = null; // 이동 코루틴 참조 변수

    void Start()
    {
        originalRotation = transform.rotation; // 초기 로테이션 값을 저장
    }

    // 현재 타겟에 도달하면 타겟을 파괴하고 다음 타겟으로 변경
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Target")
        {
            StartCoroutine(DelayedDestroy(other.gameObject, 3.0f));
        }
    }

    IEnumerator DelayedDestroy(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(target);
        targets.RemoveAt(0);
    }

    IEnumerator MoveToTarget()
    {
        if (targets.Count > 0)
        {
            isMoving = true;

            // 대상 오브젝트까지 이동
            while (transform.position != targets[0].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, targets[0].position, movementSpeed * Time.deltaTime);
                yield return null;
            }

            // 대상 오브젝트에 도착하면 천천히 Y축 회전 조정
            Quaternion targetRotation = Quaternion.Euler(originalRotation.eulerAngles.x, originalRotation.eulerAngles.y - 70f, originalRotation.eulerAngles.z);
            Quaternion startRotation = transform.rotation;
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * rotationSpeed;
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
                yield return null;
            }

            yield return new WaitForSeconds(3.0f);
            isMoving = false;

            isMovingCoroutine = null;

            //기본 로테이션 값 조정
            originalRotation = transform.rotation;
        }
    }
}
