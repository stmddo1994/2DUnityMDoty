using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover2 : MonoBehaviour
{
    private float horizontalSpeed;
    private float verticalSpeed;
    private float rotationSpeed;

    public enum HorizontalDirection { East, West, None };
    public enum VerticalDirection { North, South, None };
    public enum RotationDirection { Clockwise, CounterClockwise, None };

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
        public VerticalDirection direction;

    }

    [System.Serializable]
    public struct RotationMovement
    {
        public float speed;
        public RotationDirection direction;

    }

    public HorizontalMovement horizontalMovement;
    public VerticalMovement verticalMovement;
    public RotationMovement rotationMovement;

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalSpeed = horizontalMovement.speed;
        verticalSpeed = verticalMovement.speed;
        rotationSpeed = rotationMovement.speed;
        if (horizontalMovement.direction == HorizontalDirection.West)
            horizontalSpeed = -horizontalSpeed;
        if (verticalMovement.direction == VerticalDirection.South)
            verticalSpeed = -verticalSpeed;
        if (rotationMovement.direction == RotationDirection.Clockwise)
            rotationSpeed = -rotationSpeed;

        transform.position = new Vector3(
            transform.position.x + horizontalSpeed * Time.deltaTime,
            transform.position.y + verticalSpeed * Time.deltaTime,
            transform.position.z
            ) ;
        transform.rotation = Quaternion.Euler(
            0,
            0,
            transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime
            );

    }

    public void OnValidate()
    {
        horizontalMovement.speed = Mathf.Clamp(horizontalMovement.speed, 0f, 15f);
        verticalMovement.speed = Mathf.Clamp(verticalMovement.speed, 0f, 15f);
        rotationMovement.speed = Mathf.Clamp(rotationMovement.speed, 0f, 15f);

        if (horizontalMovement.direction == HorizontalDirection.None)
            horizontalMovement.speed = 0f;
        if (verticalMovement.direction == VerticalDirection.None)
            verticalMovement.speed = 0f;
        if (rotationMovement.direction == RotationDirection.None)
            rotationMovement.speed = 0f;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
