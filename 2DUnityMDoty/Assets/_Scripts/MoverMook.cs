using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverMook : MonoBehaviour
{
    private float horizontalSpeed;

    public enum HorizontalDirection { East, West, None };

    public GameObject laserShot;
    public Transform firePoint;
    public float shotDelay = 1.0f;
    float shotTimer;

    [System.Serializable]
    public struct HorizontalMovement
    {
        public float speed;
        public HorizontalDirection direction;

    }

    public HorizontalMovement horizontalMovement;

    Vector3 startingPos;
    Transform trans;
    public float pingPongDistance = 2.0f;
    public float pingPongTime = 1.0f;

    GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        shotTimer = shotDelay;
        trans = GetComponent<Transform>();
        startingPos = trans.position;
        gameController = FindObjectOfType<GameController>();
        gameController.numberOfEnemies++;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalSpeed = horizontalMovement.speed;
        if (horizontalMovement.direction == HorizontalDirection.West)
            horizontalSpeed = -horizontalSpeed;

        trans.position = new Vector3(
            transform.position.x + horizontalSpeed * Time.deltaTime,
            startingPos.y + Mathf.PingPong(Time.time * pingPongTime, pingPongDistance),
            transform.position.z
            );

        //repeated fire
        shotTimer -= Time.deltaTime;
        if (shotTimer <= 0)
        {
            Instantiate(laserShot, firePoint.position, firePoint.rotation);
            shotTimer = shotDelay;
        }
    }

    public void OnValidate()
    {
        horizontalMovement.speed = Mathf.Clamp(horizontalMovement.speed, 0f, 15f);

        if (horizontalMovement.direction == HorizontalDirection.None)
            horizontalMovement.speed = 0f;
    }
    private void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }
}
