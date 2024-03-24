using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverSeeker : MonoBehaviour
{
    private Transform player;
    private float horizontalSpeed;
    private float verticalSpeed;

    public enum HorizontalDirection { East, West, None };
    public enum VerticalDirection { North, South, None };

    [System.Serializable]
    public struct HorizontalMovement
    {
        public float speed;
        public HorizontalDirection direction;

    }

    [System.Serializable]
    public struct VerticalMovement
    {
        public float speed;

    }

    public HorizontalMovement horizontalMovement;
    public VerticalMovement verticalMovement;

    public GameObject laserShot;
    public Transform firePoint;
    public float shotDelay = 0.5f;
    float shotTimer;

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        shotTimer = shotDelay;
        gameController = FindObjectOfType<GameController>();
        gameController.numberOfEnemies++;
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer -= Time.deltaTime;

        horizontalSpeed = horizontalMovement.speed;
        verticalSpeed = verticalMovement.speed;

        if (horizontalMovement.direction == HorizontalDirection.West)
            horizontalSpeed = -horizontalSpeed;

        if (transform.position.y < player.position.y)
            verticalSpeed = verticalSpeed;
        else if (transform.position.y > player.position.y)
            verticalSpeed = -verticalSpeed;
        else
        {
            verticalSpeed = 0f;
        }

        if(transform.position.y >= player.position.y - 0.5f && transform.position.y <= player.position.y + 0.5)
        {
            if (shotTimer <= 0)
            {
                Instantiate(laserShot, firePoint.position, firePoint.rotation);
                shotTimer = shotDelay;
            }
        }

        transform.position = new Vector3(
            transform.position.x + horizontalSpeed * Time.deltaTime,
            transform.position.y + verticalSpeed * Time.deltaTime,
            transform.position.z
            );

    }

    public void OnValidate()
    {
        horizontalMovement.speed = Mathf.Clamp(horizontalMovement.speed, 0f, 15f);
        verticalMovement.speed = Mathf.Clamp(verticalMovement.speed, 0f, 15f);

        if (horizontalMovement.direction == HorizontalDirection.None)
            horizontalMovement.speed = 0f;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
