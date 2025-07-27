using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform blueNexus;
    public Transform redNexus;
    public  bool isGameActive;
    public static GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        instance = this;
    }

    public  void GameOver(string teamname)
    {
        isGameActive = false;
        Debug.Log(teamname + " has won the game. Congrats!");
    }
}
