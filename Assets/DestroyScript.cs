using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    private ParticleSystem particles;
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (!particles.IsAlive())
        {
            Destroy(this.gameObject);
        }
    }
}
