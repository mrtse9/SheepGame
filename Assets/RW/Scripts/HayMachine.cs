using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    [SerializeField]
    float speed;

    private float horizontalBoundary = 22;

    public GameObject hayBalePrefab; 
    public Transform haySpawnpoint;
    public float shootInterval; 
    private float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateShooting();
    }

    private void UpdateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0 && transform.position.x > -horizontalBoundary)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    private void ShootHay()
    {
        SoundManager.Instance.PlayShootClip();
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
    }

    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            ShootHay();
        }
    }



}
