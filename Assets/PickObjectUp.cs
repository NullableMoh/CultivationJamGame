using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObjectUp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.gameObject.GetComponent<HeartOrgan>())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.parent = transform;
                }
            }
        }
    }
}
