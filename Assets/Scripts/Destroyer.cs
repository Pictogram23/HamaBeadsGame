using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject[] objTargets = null;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.transform.root.gameObject);
    }
}