using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndBed : MonoBehaviour
{
    private bool pileMerged;
    private bool plantFed;


    private RopePile ropePile;
    private RopePileRevealer ropePileRevealer;
    private Plant plant;
    
    
    private void OnEnable()
    {
        ropePileRevealer = FindObjectOfType<RopePileRevealer>();
        ropePileRevealer.OnRopePileRevealed += FindRopePile;
     
        plant = FindObjectOfType<Plant>();
        plant.OnFoodAccepted += PlantFedTrue;
    }

    private void FindRopePile()
    {
        ropePile = FindObjectOfType<RopePile>();
        ropePile.OnPileMerged += PileMergedTrue;
    }

    private void OnDisable()
    {
        if(ropePile)
            ropePile.OnPileMerged -= PileMergedTrue;
    
        if(plant)
            plant.OnFoodAccepted -= PlantFedTrue;

        if (ropePileRevealer)
            ropePileRevealer.OnRopePileRevealed -= FindRopePile;
    }

    private void PileMergedTrue()
    {
        pileMerged = true;
    }

    private void PlantFedTrue()
    {
        plantFed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>() && pileMerged && plantFed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
