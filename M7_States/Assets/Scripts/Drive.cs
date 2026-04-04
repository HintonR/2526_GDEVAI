using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour {

 	public float speed = 10.0F;
    public float rotationSpeed = 100.0F;

    [SerializeField] GameObject bullet, turret;

    void Update() {

        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
            Fire();
	}

    void Movement()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }

    void Fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }
}
