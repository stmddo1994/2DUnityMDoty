using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    GameController gameController;

    Vector2 moveInput;
    public float speed = 8.0f;
    public Transform upperLeftBounds, lowerRightBounds;
    public GameObject playerSprite;
    public GameObject laserShot; //Prebab player laser to instantiate
    public Transform firePoint; //Where to create the player laser
    public float shotDelay = 0.4f;
    float shotTimer;

    public float invncblDurationSec = 10f;
    public float invncblDeltaTime = 0.15f;

    public int numberOfFlashes = 5;
    public SpriteRenderer mySprite;
    public Collider2D triggerCollider;

    public bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shotTimer = shotDelay;
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        rb.velocity = moveInput * speed;
        transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, upperLeftBounds.position.x, lowerRightBounds.position.x),
                Mathf.Clamp(transform.position.y, lowerRightBounds.position.y, upperLeftBounds.position.y),
                transform.position.z);
        anim.SetFloat("PlayerMovement", Input.GetAxisRaw("Vertical"));
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Instantiate(laserShot, firePoint.position, firePoint.rotation);
        }
        if (Input.GetButton("Fire1") && canShoot)
        {
            shotTimer -= Time.deltaTime;
            if(shotTimer <= 0)
            {
                Instantiate(laserShot, firePoint.position, firePoint.rotation);
                shotTimer = shotDelay;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //If other.gameObject.tag == "anything that can cause damage"
        //gamecontroller.GettingHit()
        gameController.GettingHit();
        StartCoroutine(BecomeTemporarilyInvincible());
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Invincible");
        Physics2D.IgnoreLayerCollision(8, 9, true);
        canShoot = false;

        for (float i = 0; i < numberOfFlashes; i++)
        {
            mySprite.color = new Color(0,0,0,0.5f);
            yield return new WaitForSeconds((invncblDurationSec * invncblDeltaTime)/(numberOfFlashes * 2));
            mySprite.color = Color.white;
            yield return new WaitForSeconds((invncblDurationSec * invncblDeltaTime)/(numberOfFlashes * 2));
        }

        Debug.Log("Over");
        Physics2D.IgnoreLayerCollision(8, 9, false);
        canShoot = true;
    }

    
}
