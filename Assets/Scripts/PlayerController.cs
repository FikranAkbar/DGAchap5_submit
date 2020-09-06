using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float hookArea = 6f;
    public float maxDistanceTower = 2.5f;
    public float moveSpeed = 5f;
    public float pullForce = 100f;
    public float rotateSpeed = 360f;
    public Collider2D[] towers;
    public bool isPulled = false;
    public LayerMask layer;
    private AudioSource myAudio;
    private bool isCrashed = false;
    private UIControllerScript uIControl;

    // Debugging Purpose
    [SerializeField] private float distance;
    [SerializeField] private Vector3 pullDirection;
    [SerializeField] private float newPullForce;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        uIControl = GameObject.Find("Canvas").GetComponent<UIControllerScript>();
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move the object
        rigidbody2D.velocity = -transform.up * moveSpeed;
        towers = Physics2D.OverlapCircleAll(transform.position, hookArea, layer);
        for (int i = 0; i < towers.Length; i++)
        {
            if (Vector3.Distance(transform.position, towers[i].transform.position) < maxDistanceTower)
            {
                towers[i].GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                towers[i].GetComponent<SpriteRenderer>().color = Color.white;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Wall"))
        {
            if (!isCrashed)
            {
                // Play SFX
                myAudio.Play();
                rigidbody2D.velocity = new Vector3(0, 0, 0);
                rigidbody2D.angularVelocity = 0f;
                isCrashed = true;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Goal"))
        {
            if (SceneManager.GetActiveScene().name == "Stage 1")
            {
                SceneManager.LoadScene("Stage 2");
            }
            else
            {
                uIControl.EndGame();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isPulled) return;
        if (other.tag.Equals("Tower"))
        {
            //Change the tower color back to normal
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}