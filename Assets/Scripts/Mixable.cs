using System;
using UnityEngine;

public class Mixable : MonoBehaviour
{
    [SerializeField] BaseOrgan baseOrgan;
    [SerializeField] GameObject newMixedOrgan;

    private void OnCollisionEnter(Collision collision)
    {
        var organ = collision.gameObject.GetComponent<BaseOrgan>();
        if (organ)
        {
            organ.GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

            organ.GetComponent<Collider>().enabled = false;
            GetComponent<Collider>().enabled = false;

            Instantiate(newMixedOrgan, organ.transform.position, Quaternion.identity);

            Destroy(organ);
            Destroy(this.gameObject);
        }
    }
}
