using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    public MapGen mapGen;

    public ParticleSystem particleSystem;

    public void ChangeParticleRate(float newRate)
    {
        var emission = particleSystem.emission;
        var rateOverTime = emission.rateOverTime;
        rateOverTime.constant = newRate;
        emission.rateOverTime = rateOverTime;
    }

    private void Start()
    {
        mapGen = GameObject.Find("Maps").GetComponent<MapGen>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mapGen.TakeDamage();
            ChangeParticleRate(30);
            DestroyAfterTime(0.5f);

        }
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
