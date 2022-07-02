using System;
using UnityEngine;

public class Holdable : MonoBehaviour
{
    [SerializeField] float radius = 2f;

    public float Radius { get { return radius; } }

    PickObjectUp pickObjectUp;

    private void Awake()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        pickObjectUp = FindObjectOfType<PickObjectUp>();
    }

    private void OnDestroy()
    {
        Debug.Log("destroyed");
        pickObjectUp.ResetHoldingState();
    }
}