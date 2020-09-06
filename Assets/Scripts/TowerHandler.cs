using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    // Debugging Purpose
    [SerializeField] private float distance;
    [SerializeField] private Vector3 pullDirection;
    [SerializeField] private float newPullForce;

    public PlayerController player;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = player.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        //sprite.color = Color.green;
    }

    private void OnMouseDown()
    {
        if (sprite.color == Color.green)
        {
            distance = Vector2.Distance(player.transform.position, transform.position);
            pullDirection = (player.transform.position - transform.position).normalized;
            newPullForce = Mathf.Clamp(player.pullForce / distance, 20, 50);
            rigidbody2D.AddForce(pullDirection * newPullForce);
            if (transform.position.y > player.transform.position.y)
            {
                rigidbody2D.angularVelocity = player.rotateSpeed / distance;
            }
            else
            {
                rigidbody2D.angularVelocity = -player.rotateSpeed / distance;
            }
        }
    }

    private void OnMouseUp()
    {
        rigidbody2D.angularVelocity = 0;
    }
}
