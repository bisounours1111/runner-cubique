using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    public MapGen mapGen;

    private void Start()
    {
        mapGen = GameObject.Find("Maps").GetComponent<MapGen>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mapGen.TakeDamage();
        }
    }
}
