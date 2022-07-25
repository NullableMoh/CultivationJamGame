using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class RopePileRevealer : MonoBehaviour
{
    [SerializeField] private GameObject ropePile;

    public delegate void RevealRopePileEventHandler();
    public event RevealRopePileEventHandler OnRopePileRevealed;


    private Plant plant;
    
    private void OnEnable()
    {
        plant = FindObjectOfType<Plant>();
        plant.OnFoodAccepted += RevealRopePile;
    }

    private void OnDisable()
    {
        if(plant)
            plant.OnFoodAccepted -= RevealRopePile;
    }

    private void RevealRopePile()
    {
        ropePile.SetActive(true);
        OnRopePileRevealed?.Invoke();
    }

}
