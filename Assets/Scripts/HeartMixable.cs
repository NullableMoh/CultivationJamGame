using System;
using UnityEngine;

public class HeartMixable : MonoBehaviour
{
    [SerializeField] GameObject newMixedOrgan;


    private void OnCollisionEnter(Collision collision)
    {
        var organ = collision.gameObject.GetComponent<HeartOrgan>();
        if(organ)
        {
            organ.GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

            Instantiate(newMixedOrgan, organ.transform.position, Quaternion.identity);

            Destroy(organ);
            Destroy(this.gameObject);
        }
    }
}
