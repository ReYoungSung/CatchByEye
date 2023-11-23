using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<Transform> targets; // �̵��� ��� ������Ʈ
    public float movementSpeed = 5f; // �̵� �ӵ�
    public float rotationSpeed = 1.5f; // �����̼� �ӵ�

    private bool isMoving = false; // �̵� ������ ����

    private Quaternion originalRotation; // �ʱ� �����̼� ��

    public Coroutine isMovingCoroutine = null; // �̵� �ڷ�ƾ ���� ����

    void Start()
    {
        originalRotation = transform.rotation; // �ʱ� �����̼� ���� ����
    }

    // ���� Ÿ�ٿ� �����ϸ� Ÿ���� �ı��ϰ� ���� Ÿ������ ����
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

            // ��� ������Ʈ���� �̵�
            while (transform.position != targets[0].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, targets[0].position, movementSpeed * Time.deltaTime);
                yield return null;
            }

            // ��� ������Ʈ�� �����ϸ� õõ�� Y�� ȸ�� ����
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

            //�⺻ �����̼� �� ����
            originalRotation = transform.rotation;
        }
    }
}
