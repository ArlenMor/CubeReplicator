using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]
public class Explosion : MonoBehaviour
{
    public static Explosion Instance = null;

    [SerializeField] private CubeSpawner _spawner;
    [SerializeField, Range(1, 10)] private float _radius;
    [SerializeField, Range(100, 500)] private float _explosionForce;

    private void Start()
    {
        if (Instance = null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        _spawner.OnExplode += Explode;
    }

    private void Explode(Transform centerExplosion, List<Rigidbody> explosiveObjects)
    {
        for (int i = 0; i < explosiveObjects.Count; i++)
            explosiveObjects[i].AddExplosionForce(_explosionForce, centerExplosion.position, _radius);
    }
}
