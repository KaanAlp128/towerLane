using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody fireballRb;
    public bool isRedTeam;
    private float timeLeft = 2f;
    private int missChance = 5;
    // Start is called before the first frame update
    void Start()
    {
        fireballRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isRedTeam != other.gameObject.GetComponent<MinionController>().isRedTeam)
        {
            Destroy(this.gameObject);
            if (UnityEngine.Random.Range(0, 20) > missChance)
            {
                    other.gameObject.GetComponent<MinionController>().TakeDamage(3);
                    other.gameObject.GetComponent<MinionController>().TakeDamage(other.gameObject.GetComponent<MinionController>().damage);

            }
            if (other.gameObject.tag == "nexus" && isRedTeam != other.gameObject.GetComponent<NexusController>().isRedTeam)
            {
                Destroy(this.gameObject);
            }
        }

    }
}