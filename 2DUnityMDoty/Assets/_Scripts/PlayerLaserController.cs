using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserController : MonoBehaviour
{
    public GameObject laserHit; //particle effect explosion
    public float speed = 5.0f;
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x + speed * Time.deltaTime,
            transform.position.y,
            transform.position.z
            );
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        Instantiate(laserHit, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(other.gameObject);
        if (other.gameObject.tag == "Enemy")
        {
            gameController.numberOfEnemies--;
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
