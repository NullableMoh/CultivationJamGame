using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undoable : MonoBehaviour
{
    [SerializeField] GameObject firstMixable, secondMixable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<MixableUndoer>())
        {
            GetComponent<MeshRenderer>().enabled = false;
            Instantiate(firstMixable, transform.position, Quaternion.identity);
            Instantiate(secondMixable, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
