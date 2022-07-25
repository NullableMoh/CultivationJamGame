using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameObject[] acceptableFoods;
    [SerializeField] private GameObject acceptedParticleSystem;
    [SerializeField] private GameObject rejectedParticleSystem;

    private GameObject currentParticleSystem;
    private GameObject currentFood;
    
    //events
    public delegate void FoodAcceptedEventHandler();
    public event FoodAcceptedEventHandler OnFoodAccepted;

    private void OnTriggerEnter(Collider collision)
    {
        bool badFood = true;

        foreach(GameObject food in acceptableFoods)
        {
            if(food.CompareTag(collision.gameObject.tag))
            {
                currentFood = collision.gameObject;
                badFood = false;
                break;
            }
            else
                badFood = true;
        }

        if(badFood)
        {
            FoodRejected();
        }
        else
        {
            FoodAccepted();
        }
    }

    private void FoodAccepted()
    {
        Destroy(currentParticleSystem);
        currentParticleSystem = Instantiate(acceptedParticleSystem, transform.position, Quaternion.Euler(-90f,0f,0f));
        Destroy(currentFood);
        OnFoodAccepted?.Invoke();
    }

    private void FoodRejected()
    {
        Destroy(currentParticleSystem);
        currentParticleSystem = Instantiate(rejectedParticleSystem, transform.position, Quaternion.Euler(-90f, 0f, 0f));
    }
}
