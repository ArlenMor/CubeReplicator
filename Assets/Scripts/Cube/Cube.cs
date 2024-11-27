using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField, Range(1, 10)] float _radius;
    [SerializeField, Range(100, 500)] float _explosionForce;

    MeshRenderer _renderer;
    public float MultiplyChance { get; private set; }

    private float _reduceMultiplyChanceCoef = 2f;

    private void Awake()
    {
        MultiplyChance = 1f;

        System.Random random = new System.Random();

        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.color = new Color(Convert.ToSingle(random.NextDouble()),
                                                Convert.ToSingle(random.NextDouble()),
                                                Convert.ToSingle(random.NextDouble()));
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

    public void ReduceMultiplyChance(float multiplyChanceBigCube)
    {
        MultiplyChance = multiplyChanceBigCube / _reduceMultiplyChanceCoef;
    }
}
