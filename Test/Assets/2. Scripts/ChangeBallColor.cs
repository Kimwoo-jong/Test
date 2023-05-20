using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBallColor : MonoBehaviour
{
    public Color[] colors; // 변경할 색상들의 배열
    private int currentColorIndex = 0; // 현재 선택된 색상의 인덱스

    private MeshRenderer mesh;
    private Material material;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        material = mesh.material;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // 클릭할 때마다 다음 색상으로 변경
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            ChangeMaterialColor();
        }
    }

    private void ChangeMaterialColor()
    {
        if (material != null)
        {
            // 색상 변경
            material.color = colors[currentColorIndex];
        }
    }
}
