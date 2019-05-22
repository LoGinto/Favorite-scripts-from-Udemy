using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // configuration parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Lazer")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] AudioClip shootingSFX;

    [Header("Destruction sound")]
    [SerializeField] AudioClip deathSFX;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Use this for initialization
    void Start () {
        SetUpMoveBoundaries();
	}
 
    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();//reducing the hp per hit(collision)
        if (health <= 0)
        {
            Destroy(gameObject);//destroy the game object
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);//playing death effect

        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(//creating laser 
                    laserPrefab,
                    transform.position,
                    Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            //creating shooting vector taken from rigitbody????
            AudioSource.PlayClipAtPoint(shootingSFX, Camera.main.transform.position);
            yield return new WaitForSeconds(projectileFiringPeriod);
            //You use a yield return statement to return each element one at a time.
        }
    }


    private void Move()
    {
        //movement by axis 
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;//getting x axis
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;//getting y axis

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);//new position of x
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);//new Y pos

        //Clamps the given value between the given minimum float and maximum float values. Returns the given value if it is within the min and max range.

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        //restricting camera
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
