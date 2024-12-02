using System.Collections.Generic;
using UnityEngine;

public class Explosioner : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float _radius;
    [SerializeField, Range(100,500)] private float _explosionForce;

    public void Explode(Transform centerExplosion, List<Rigidbody> explosiveObjects)
    {
        for (int i = 0; i < explosiveObjects.Count; i++)
            explosiveObjects[i].AddExplosionForce(_explosionForce, centerExplosion.position, _radius);
    }
}