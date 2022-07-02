using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plant : MonoBehaviour
{
    [SerializeField] GameObject[] acceptableFoods;

    GameObject currentFood;

    private void OnTriggerEnter(Collider collision)
    {
        bool badFood = true;

        foreach(GameObject food in acceptableFoods)
        {
            if(food.CompareTag(collision.gameObject.tag))
            {
                currentFood = collision.gameObject;
                Debug.Log("Food Accepted");
                badFood = false;
                break;
            }
            else
                badFood = true;
        }

        if(badFood)
            RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
