using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool isTriggered = false;
    
    private MeshRenderer mesh;
    private Rigidbody rigid;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        rigid = GetComponent<Rigidbody>();
    }

    public void TriggerBall()
    {
        if (!isTriggered)
        {
            isTriggered = true;
        }
    }

    public void ResetTrigger()
    {
        isTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Duplicate"))
        {
            TriggerBall();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Duplicate"))
        {
            TriggerBall();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Duplicate"))
        {
            ResetTrigger();
        }
    }
}