using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosion;
	
	void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.TryGetComponent<TankAI>(out var tank))
		    tank.TakeDamage();
    	
		GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
    	Destroy(e,1.5f);
    	Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
