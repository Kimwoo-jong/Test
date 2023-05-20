using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateBall : MonoBehaviour
{
    public SpherePool spherePool;
    public Color targetColor;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
        {
            return;
        }

        if (other.gameObject.CompareTag("Ball"))
        {
            BallController ballController = other.gameObject.GetComponent<BallController>();

            if (ballController != null && !ballController.isTriggered)
            {
                hasTriggered = true;

                ballController.TriggerBall();

                MeshRenderer mesh = other.GetComponent<MeshRenderer>();

                if (mesh != null)
                {
                    Material material = mesh.material;
                    Color color = material.color;

                    if (color == targetColor)
                    {
                        int ballCount = spherePool.GetBallCount();
                        int newBallCount = ballCount * 2;
                        spherePool.IncreaseBallCount(newBallCount / 2);
                    }
                }
            }
        }
    }
}