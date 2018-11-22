using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManger : MonoBehaviour {

    public  static GameManger _instance;

    public GameObject gameover; 

    public int level = 1;
    public int energy = 100;

    public List<Enemy> enemyList = new List<Enemy>();

    private bool sleepStep = true;

    private  Text energyCount;



    // Use this for initialization
    void Awake () {
        _instance = this;
        energyCount = GameObject.FindGameObjectWithTag ("EnergyCount").GetComponent <Text >() ;
        InitGame();

    }
    private void Update()
    {
        if (energy<=0)
        {
            Time.timeScale = 0;
           gameover .SetActive ( true);
        }
    }

    void InitGame()
    {
        UpdateEnergyText(0);
    }
    void UpdateEnergyText(int energyChange)
    {
        if (energyChange == 0)
        {
            energyCount.text = "Energy:" +  energy;
        }
        else
        {
            string str = "";
            if (energyChange < 0)
            {
                str = energyChange.ToString();
            }
            else
            {
                str = "+" + energyChange;
            }
            energyCount.text = "Energy:" + energy+ str ;
        }
    }

public void AddEnergy(int Count)
    {
        energy += Count;
        UpdateEnergyText(Count );
    }
    public void  ReduceEnergy(int Count)
    {
        energy -= Count;
        UpdateEnergyText(-Count);
    }
    public void OnPlayerMove()
    {
        if (sleepStep ==true )
        {
            sleepStep = false;
        }
        else
        {
            foreach (var enemy in enemyList)
            {
                enemy.Move();
            }
            sleepStep = true;
        }
    }
}
