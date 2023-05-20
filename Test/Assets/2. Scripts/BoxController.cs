using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Vector3 initMousePosition;
    private Vector3 initialObjectPosition;
    private Quaternion initRotation;

    private Rigidbody rigid;

    private float moveSpeed = 25f;

    private bool isRotate = false;
    private bool isDragging = false;

    private float minX = -16f;
    private float maxX = 16f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.useGravity = false;
        rigid.isKinematic = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRotate)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    initMousePosition = Input.mousePosition;
                    initialObjectPosition = transform.position;
                    initRotation = transform.rotation; // 회전을 시작하기 전에 초기 회전값 설정
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging && !isRotate)
        {
            isDragging = false;
            isRotate = true;
            StartCoroutine(RotateObject());
        }
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f, transform.rotation);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Wall"))
            {
                transform.position = rigid.position;
                break;
            }
        }

        if (isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - initMousePosition;
            float deltaX = mouseDelta.x / Screen.width;                     // 마우스 이동거리를 화면 비율로 변환

            float targetX = initialObjectPosition.x + deltaX * moveSpeed;
            targetX = Mathf.Clamp(targetX, minX, maxX);                     // X축 이동 제한 적용

            Vector3 newPosition = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }

    IEnumerator RotateObject()
    {
        float elapsedTime = 0f;
        float rotationTime = 1f; // 회전에 걸리는 시간

        Quaternion targetRotation = initRotation * Quaternion.Euler(0f, 0f, 180f); // 180도 회전

        while (elapsedTime < rotationTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / rotationTime);
            Quaternion currentRotation = Quaternion.Lerp(initRotation, targetRotation, t);

            transform.rotation = currentRotation;
            yield return null;
        }

        isRotate = false;
    }
}