using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField, Range(1, 10)] private float _radius;
    [SerializeField, Range(100, 500)] private float _explosionForce;

    private void Start()
    {
        _spawner.OnExplode += Explode;
    }

    private void OnDestroy()
    {
        _spawner.OnExplode -= Explode;
    }

    private void Explode(Transform centerExplosion, List<Rigidbody> explosiveObjects)
    {
        for(int i = 0; i < explosiveObjects.Count; i++)
        {
            Vector3 direction = explosiveObjects[i].position - centerExplosion.position;
            explosiveObjects[i].AddForceAtPosition(direction.normalized * _explosionForce, centerExplosion.position);
        }
        //for (int i = 0; i < explosiveObjects.Count; i++)
        //    explosiveObjects[i].AddExplosionForce(_explosionForce, centerExplosion.position, _radius);
    }
}