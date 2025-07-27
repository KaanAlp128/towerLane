using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class NexusController : MonoBehaviour
{

    public int health;
    public bool isRedTeam;
    public Slider healthSlider;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        health = 15;
        healthSlider.maxValue = health;
        healthSlider.value = health;
        damage = 3;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;

    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            string teamName = !isRedTeam ? "Red" : "Blue";
            GameManager.instance.GameOver(teamName);
        }
    }
}
