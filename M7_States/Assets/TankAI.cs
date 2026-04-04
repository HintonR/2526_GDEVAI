using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    [SerializeField] GameObject bullet, turret;

    Animator anim;
    GameObject player;

    int health;

    void Start()
    {
        health = 100;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));

        if (health <= 20)
            anim.SetBool("low", true);
    }

    void Fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }
    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void TakeDamage()
    {
        health -= 20;

        if (health <= 0)
            Destroy(gameObject);
    }
}
