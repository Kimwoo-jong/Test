using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<GameObject> spheres;            // 구체들을 담을 리스트

    public Transform group;                     // 구체들을 그룹화할 GameObject

    private void Start()
    {
        // 구체들을 그룹 GameObject의 자식으로 설정
        foreach (GameObject sphere in spheres)
        {
            sphere.transform.parent = group;
        }
    }

    private void Update()
    {
        // 그룹 GameObject의 위치를 구체들의 평균 위치로 설정
        Vector3 averagePosition = Vector3.zero;
        foreach (GameObject sphere in spheres)
        {
            averagePosition += sphere.transform.position;
        }
        averagePosition /= spheres.Count;

        group.position = averagePosition;

        // 카메라의 위치를 그룹 GameObject의 위치로 설정
        transform.position = group.position + new Vector3(0f, 10f, -30f);
    }
}