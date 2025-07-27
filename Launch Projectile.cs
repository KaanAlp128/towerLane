using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject towerProjectile;
    public bool isRedTeam;
    private List<GameObject> defenders;
    private float attackSpeed = 2f;
    private float timeLeft;
    

    private void Start()
    {
        defenders = new List<GameObject>();
        timeLeft = attackSpeed;
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (defenders.Count > 0)
        {
            if (defenders[0] == null)
                defenders.RemoveAt(0);
            if (defenders[0].IsDestroyed())
                defenders.RemoveAt(0);
            if (timeLeft <= 0)
                Attack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (isRedTeam != other.gameObject.GetComponent<MinionController>().isRedTeam)
            {
                defenders.Add(other.gameObject);
            }
        }
    }
  
    private void Attack()
    {
        GameObject projectile = Instantiate(towerProjectile, transform.position, transform.rotation);
        projectile.transform.DOMove(defenders[0].transform.position, 1.4f);
        timeLeft = attackSpeed;
    }
}

