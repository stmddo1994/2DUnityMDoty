using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject laserHit; //particle effect explosion
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x - speed * Time.deltaTime,
            transform.position.y,
            transform.position.z
            );
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        Instantiate(laserHit, transform.position, transform.rotation);
        //Destroy(other.gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
