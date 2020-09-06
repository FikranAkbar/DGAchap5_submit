using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMouseClick : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float moveSpeed = 1f;
    public float pullForce = 100f;
    public float rotateSpeed = 120f;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = -transform.up * moveSpeed;
    }

    private void OnMouseDown()
    {
        Debug.Log("Ship has been Clicked !");
        rb2D.angularVelocity = -rotateSpeed;
    }

    private void OnMouseUp()
    {
        rb2D.angularVelocity = 0;
    }
}
