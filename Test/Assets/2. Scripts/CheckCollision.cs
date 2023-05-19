using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCollision : MonoBehaviour
{
    private List<GameObject> reachedObjects = new List<GameObject>();
   
    private int reachedSphereCount = 0;
    private bool isDetectionEnabled = true;

    private void OnCollisionEnter(Collision other)
    {
        if (!isDetectionEnabled || reachedObjects.Contains(other.gameObject) || !other.gameObject.CompareTag("Ball"))
            return;

        reachedObjects.Add(other.gameObject);
        reachedSphereCount++;
        Debug.Log("Ball Count : " + reachedSphereCount);

        if (reachedSphereCount >= 20)
        {
            RemoveReachedObjects();
        }
    }

    private void OnCollisionExit(Collision other)
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
            if (obj.CompareTag("Ball"))
            {
                Destroy(this.gameObject);
            }
        }

        reachedObjects.Clear();
        reachedSphereCount = 0;
        isDetectionEnabled = true;
    }
}
