using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover1 : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalSpeed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x + horizontalSpeed * Time.deltaTime,
            transform.position.y + verticalSpeed * Time.deltaTime,
            transform.position.z
            );
        transform.rotation = Quaternion.Euler(
            0,
            0,
            transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime
            );
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
