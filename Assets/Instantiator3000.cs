using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator3000 : MonoBehaviour
{
    [Range(0.001f, 1f)]
    public float qtyPerSecond = 1;
    public GameObject prefab; 

    private void Start() {
        InvokeRepeating("InstantiateStuff", 1, qtyPerSecond);
    }

    void InstantiateStuff()
    {
        GameObject instance = GameObject.Instantiate(prefab, transform.position, Random.rotation, this.transform);
    }

}
