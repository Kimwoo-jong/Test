using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private List<GameObject> reachedObjects = new List<GameObject>();

    private int reachedSphereCount = 0;
    private bool isDetectionEnabled = true;

    private MeshRenderer mesh;
    public Material[] materials;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void ChangeMaterial()
    {
        if (mesh != null && reachedSphereCount == 5)
        {
            mesh.material = materials[0];
        }

        if (mesh != null && reachedSphereCount == 10)
        {
            mesh.material = materials[1];
        }

        if (mesh != null && reachedSphereCount >= 15)
        {
            mesh.material = materials[2];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDetectionEnabled || !other.gameObject.CompareTag("Ball"))
        {
            return;
        }

        BallController ballController = other.gameObject.GetComponent<BallController>();
        if (ballController != null && ballController.isTriggered)
        {
            return;
        }

        reachedObjects.Add(other.gameObject);
        reachedSphereCount++;

        ChangeMaterial();
        //Debug.Log("Name: " + other.gameObject.name);
        Debug.Log("Ball Count : " + reachedSphereCount);

        if (reachedSphereCount >= 20)
        {
            RemoveReachedObjects();
        }
        else
        {
            ballController?.TriggerBall();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (reachedObjects.Contains(other.gameObject) && other.gameObject.CompareTag("Ball"))
        {
            reachedObjects.Remove(other.gameObject);
        }
    }

    private void RemoveReachedObjects()
    {
        foreach (GameObject obj in reachedObjects)
        {
            BallController ballController = obj.GetComponent<BallController>();
            if (ballController != null && ballController.isTriggered)
            {
                ballController.ResetTrigger();
            }
        }

        reachedObjects.Clear();
        reachedSphereCount = 0;
        isDetectionEnabled = true;
        Destroy(gameObject);
    }
}
