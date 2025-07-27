using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MinionController : MonoBehaviour
{

    public bool isRedTeam;
    public string enemyTower;
    public GameObject enemyProjectile;
    public List<GameObject> defenders;
    private Animator animator;
    private int health = 3;
    private float attackSpeed = 2f;
    private float timeLeft;
    private string animatorString;
    private bool isAlive;
    private Rigidbody rb;
    public int damage;
    

    // Start is called before the first frame update
    void Start()
    {   
        defenders = new List<GameObject>();
        animator = GetComponent<Animator>();
        timeLeft = attackSpeed;
        MoveToNexus(35f);
        enemyTower = isRedTeam ? "Blue Tower Base" : "Red Tower Base";
        animatorString = isRedTeam ? "Base Layer.Swarm08_": "Base Layer.Swarm09_";
        isAlive = true;
        rb = GetComponent<Rigidbody>();
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (defenders.Count > 0)
            {
                if (defenders[0].IsDestroyed())
                    defenders.RemoveAt(0);
                if (timeLeft <= 0)
                    Attack();
                if (defenders[0].gameObject.tag == "nexus")
                    MoveToNexus(0.5f);
            }
            else
                MoveToNexus(25f);
            timeLeft -= Time.deltaTime;
        }
        else
            transform.DOKill();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "nexus" && enemyTower == other.transform.parent.gameObject.name)
        {
                transform.DOKill();
                transform.DODynamicLookAt(other.transform.position, 2);
                defenders[0] = other.gameObject;
        }

        if (other.gameObject.tag == "Enemy" && isRedTeam != other.gameObject.GetComponent<MinionController>().isRedTeam)
        {
                defenders.Add(other.gameObject);
                transform.DOKill();
                transform.DODynamicLookAt(other.transform.position, 5);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "nexus" && isRedTeam != collision.gameObject.GetComponent<NexusController>().isRedTeam)
        {
            Debug.Log(this.name + " has rammed himself to " + collision.transform.parent.gameObject.name);
            collision.gameObject.GetComponent<NexusController>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
    }

    private void MoveToNexus(float movespeed)
    {
        if (!isAlive)
            return;
        animator.Play(animatorString + "MoveFWD", 0, 1f);
        if (isRedTeam)
        {
            Vector3 nexusPosition = GameManager.instance.blueNexus.transform.position;
            nexusPosition.y = 0.2f;

            transform.DODynamicLookAt(nexusPosition, 5);
            transform.DOMove(nexusPosition, movespeed);
        }
        else
        {
            Vector3 nexusPosition = GameManager.instance.redNexus.transform.position;
            transform.DODynamicLookAt(nexusPosition, 5);
            transform.DOMove(nexusPosition, movespeed);
        }

    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            health -= damage;
            animator.Play(animatorString + "GetHit", 0, 0.25f);
            if (health <= 0)
            {
                isAlive = false;
                transform.DOKill();
                rb.isKinematic = true;
                animator.Play(animatorString + "Die", 0, 0.25f);
                Invoke("Die", 1.5f);
            }
        }
    }

    private void Die()
    {  

       Destroy(gameObject);
    }

    private void Attack()
    {
        if (!isAlive)
            return;
        animator.Play(animatorString + "Attack", 0, 0.25f);
        GameObject projectile = Instantiate(enemyProjectile, transform.position, transform.rotation);
        projectile.transform.DOMove(defenders[0].transform.position, 2f);
        timeLeft = attackSpeed;
        MoveToNexus(20f);
    }

}
        
   



