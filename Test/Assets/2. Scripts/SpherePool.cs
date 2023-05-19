using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePool : MonoBehaviour
{
    private List<GameObject> spherePool;
    public GameObject spherePrefab;

    private  float spawnRange = 1.5f;           // 구체가 생성될 수 있는 범위

    public void Initialize(int size)
    {
        spherePool = new List<GameObject>();

        for (int i = 0; i < size; ++i)
        {
            GameObject obj = Instantiate(spherePrefab);
            obj.SetActive(true);
            spherePool.Add(obj);

            // 랜덤한 위치에 구체 생성
            Vector3 randomPosition = new Vector3(Random.Range(-spawnRange, spawnRange), 0.5f, Random.Range(-spawnRange, spawnRange));
            obj.transform.position = randomPosition;
            obj.transform.parent = transform;
        }
    }
}
