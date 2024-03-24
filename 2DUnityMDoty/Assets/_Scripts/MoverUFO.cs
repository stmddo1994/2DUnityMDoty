using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverUFO : MonoBehaviour
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
    public float overshootDistance = 0.5f;

    public GameObject laserShot;
    public Transform firePoint;
    public float shotDelay = 2.0f;
    float shotTimer;
    public float startDelay = 0.0f;
    float startTimer;

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        shotTimer = shotDelay;
        startTimer = startDelay;
        gameController = FindObjectOfType<GameController>();
        gameController.numberOfEnemies++;

        verticalSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalSpeed = horizontalMovement.speed;
        if (horizontalMovement.direction == HorizontalDirection.West)
            horizontalSpeed = -horizontalSpeed;

        startTimer -= Time.deltaTime;
        if (startTimer <= 0)
            shotTimer -= Time.deltaTime;

        if (shotTimer <= 0)
        {
            if (transform.position.y < player.position.y)
            {
                MoveUp();
            }
            else if (transform.position.y > player.position.y)
            {
                MoveDown();
            }
            else
            {
                FireLaser();
            }
            shotTimer = shotDelay;
        }

        if (verticalSpeed > 0 && transform.position.y >= player.position.y + overshootDistance)
        {
            FireLaser();
        }
        if (verticalSpeed < 0 && transform.position.y <= player.position.y - overshootDistance)
        {
            FireLaser();
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
        verticalMovement.speed = Mathf.Clamp(verticalMovement.speed, 0f, 20f);

        if (horizontalMovement.direction == HorizontalDirection.None)
            horizontalMovement.speed = 0f;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void FireLaser()
    {
        verticalSpeed = 0;
        Instantiate(laserShot, firePoint.position, firePoint.rotation);
        shotTimer = shotDelay;
    }

    void MoveUp()
    {
        verticalSpeed = verticalMovement.speed;
    }

    void MoveDown()
    {
        verticalSpeed = -verticalMovement.speed;
    }
}

/*if (transform.position.y<player.position.y)
            {
                while (transform.position.y<player.position.y + overshootDistance)
                    verticalSpeed = verticalMovement.speed;
            }
            else if (transform.position.y > player.position.y)
            {
                while (transform.position.y > player.position.y - overshootDistance)
                    verticalSpeed = -verticalMovement.speed;
            }
            else
                verticalSpeed = 0;

            verticalSpeed = 0;
            Instantiate(laserShot, firePoint.position, firePoint.rotation);
shotTimer = shotDelay;*/