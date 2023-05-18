using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rigid;
    private bool isFalling = false;

    private float fallSpeed = 1f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }

    private void Update()
    {
        if(!isFalling)
        {
            if(transform.position.y <= 0.5f)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void StartFalling()
    {
        isFalling = true;
        rigid.useGravity = true;
        rigid.velocity = new Vector3(0f, -fallSpeed, 0f);
    }
}
