using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody rigid;
    private Quaternion initRotation;

    private float rotateSpeed = 180f;
    private float moveSpeed = 0.5f;

    private bool isRotate;

    public SpherePool objectPool;
    private int poolSize = 10;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.useGravity = false;
        rigid.isKinematic = true;

        objectPool.Initialize(poolSize);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            isRotate = true;
            initRotation = transform.rotation;
        }

        if (Input.GetMouseButton(0))
        {
            float deltaX = Input.mousePosition.x - mousePosition.x;

            transform.Translate(Vector3.right * deltaX * moveSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isRotate)
            {
                // 오브젝트를 180도 회전하는 애니메이션 시작
                StartCoroutine(RotateObject());
            }
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
    }

    IEnumerator RotateObject()
    {
        isRotate = false;
        float elapsedTime = 0f;
        Quaternion targetRotation = initRotation * Quaternion.Euler(0f, 0f, rotateSpeed); // 180도 회전

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(initRotation, targetRotation, elapsedTime);
            yield return null;
        }

        // Sphere 풀에서 재사용할 Sphere 가져오기
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject sphere = objectPool.GetObject();

            // Sphere 초기화 및 위치 설정
            Vector3 spherePosition = transform.position + Vector3.up * i;
            spherePosition.y = GetSpherePosition(spherePosition, sphere.GetComponent<SphereCollider>().radius);
            
            sphere.transform.position = spherePosition;
            sphere.GetComponent<BallController>().StartFalling();
        }

        // Sphere 생성 후 오브젝트 초기 위치로 이동
        // transform.position = new Vector3(0f, 9f, 0f);
        // transform.rotation = Quaternion.identity;
    }

    float GetSpherePosition(Vector3 desiredPosition, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(desiredPosition, radius);

        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Ball"))
            {
                desiredPosition.y += radius * 1.5f;
            }
        }

        return desiredPosition.y;
    }
}