using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header ("SErialized Fields")]
    [SerializeField] float health = 100;
    [SerializeField] float countingTheShots;
    [SerializeField] float minTimeAidaShots = 0.2f;
    [SerializeField] float maxTimeAidaShots = 3f;
    [SerializeField] GameObject lazerShot;
    [SerializeField] float projectileSpeed = 10f;

	// Use this for initialization
	void Start () {
        countingTheShots = Random.Range(minTimeAidaShots, maxTimeAidaShots);
	}
	
	// Update is called once per frame
	void Update () {
        RandoCountShoot();//shots at random interval 
	}

    private void RandoCountShoot()
    {
        countingTheShots -= Time.deltaTime; 
        if (countingTheShots <= 0f)//per frame we reduce the float from Random to 0
        {
            Fire();//then we shoot
            countingTheShots = Random.Range(minTimeAidaShots, maxTimeAidaShots);//do the calculation again
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(//creating a projectile
            lazerShot,
            transform.position,
            Quaternion.identity
            ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);//shooting downwards
    }

    private void OnTriggerEnter2D(Collider2D other)//what happens on collision
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();//we get a damage dealer
        if (!damageDealer) { return; }
        CountHit(damageDealer);
    }

    private void CountHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();//method is called on collision and collision decreases health
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
