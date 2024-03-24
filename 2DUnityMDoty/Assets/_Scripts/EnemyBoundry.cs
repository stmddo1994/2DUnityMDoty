using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoundry : MonoBehaviour
{
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
        if (other.gameObject.tag == "Enemy")
        {
            gameController.numberOfEnemies--;
        }
    }
}
