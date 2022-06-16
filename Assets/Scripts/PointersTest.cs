using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PointersTest : MonoBehaviour
{
    private void Start()
    {
        PointerTesting();
    }

    private unsafe void PointerTesting()
    {
        int a = 3;
        int *p = &a;
        Debug.Log("a:" + a);
        Debug.Log("p: " + *p);
        a = 4;
        Debug.Log("a: " + a);
        Debug.Log("p: " + *p);
        Debug.Log(p->ToString());
        Debug.Log((*p).ToString());

    }
}
