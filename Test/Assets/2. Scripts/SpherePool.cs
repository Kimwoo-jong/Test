using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePool : MonoBehaviour
{
    private List<GameObject> spherePool;
    public GameObject spherePrefab;

    private float spawnRange = 1.5f;
    private int ballCount = 0;
    private bool initBallCreated = false;

    private void Start()
    {
        Initialize(10);
    }

    public void Initialize(int size)
    {
        spherePool = new List<GameObject>();

        if (!initBallCreated)
        {
            CreateInitialBalls(size);
            initBallCreated = true;
        }
    }

    private void CreateInitialBalls(int size)
    {
        for (int i = 0; i < size; ++i)
        {
            CreateBall();
        }
    }

    private void CreateBall()
    {
        GameObject obj = Instantiate(spherePrefab);
        obj.SetActive(true);
        obj.name = "Sphere" + ballCount;
        spherePool.Add(obj);

        ballCount++;

        // 떨어지고 있는 공의 위치를 기준으로 새로운 공 생성
        Vector3 randomPosition = GetRandomSpawnPosition();
        obj.transform.position = randomPosition;
        obj.transform.parent = transform;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 fallingBallPosition = FindFallingBallPosition();
        Vector3 spawnPosition = fallingBallPosition;

        float spawnOffset = Random.Range(-spawnRange, spawnRange);
        spawnPosition += new Vector3(spawnOffset, 0f, 0f);

        return spawnPosition;
    }

    private Vector3 FindFallingBallPosition()
    {
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            if (rb.velocity.y < 0f)
            {
                return rb.transform.position;
            }
        }

        // 떨어지고 있는 공이 없는 경우 SpherePool 오브젝트의 위치를 반환하도록 수정
        return transform.position;
    }

    public int GetBallCount()
    {
        return ballCount;
    }

    public void IncreaseBallCount(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            CreateBall();
        }
    }
}