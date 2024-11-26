using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField, Range(1, 10)] float _radius;
    [SerializeField, Range(100, 500)] float _explosionForce;

    MeshRenderer _renderer;
    public float DestroyChance { get; private set; }

    private static float _maxRgbValue = 1;
    private float _reduceSpawnChanceCoef = 2f;

    private void Awake()
    {
        DestroyChance = 1f;

        System.Random random = new System.Random();

        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.color = new Color(Convert.ToSingle(random.NextDouble() % _maxRgbValue),
                                                Convert.ToSingle(random.NextDouble() % _maxRgbValue),
                                                Convert.ToSingle(random.NextDouble() % _maxRgbValue));
    }

    public void Explode(List<Collider> smallCubes)
    {
        for (int i = 0; i < smallCubes.Count; i++)
        {
            Rigidbody rigitbody = smallCubes[i].attachedRigidbody;

            if (rigitbody)
                rigitbody.AddExplosionForce(_explosionForce, transform.position, _radius);
        }
    }

    public void ReduceDestroyChance(float oldChance)
    {
        DestroyChance = oldChance / _reduceSpawnChanceCoef;
    }
}
