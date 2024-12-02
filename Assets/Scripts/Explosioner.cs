using System.Collections.Generic;
using UnityEngine;

public class Explosioner
{
    private float _radius;
    private float _explosionForce;

    public Explosioner(float explosionRadius, float explosionForce)
    {
        _radius = explosionRadius;
        _explosionForce = explosionForce;
    }

    public void Explode(Transform centerExplosion, List<Rigidbody> explosiveObjects)
    {
        for (int i = 0; i < explosiveObjects.Count; i++)
            explosiveObjects[i].AddExplosionForce(_explosionForce, centerExplosion.position, _radius);
    }
}