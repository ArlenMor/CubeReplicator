using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float _radius;
    [SerializeField, Range(100, 500)] private float _explosionForce;

    private float _reduceMultiplyChanceCoef = 2f;
    private MeshRenderer _renderer;

    public Rigidbody Rigidbody { get; private set; }
    public float MultiplyChance { get; private set; }

    private void Awake()
    {
        MultiplyChance = 1f;

        System.Random random = new System.Random();

        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.color = new Color(Convert.ToSingle(random.NextDouble()),
                                                Convert.ToSingle(random.NextDouble()),
                                                Convert.ToSingle(random.NextDouble()));

        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(float multiplyChanceBigCube)
    {
        MultiplyChance = multiplyChanceBigCube / _reduceMultiplyChanceCoef;
    }

    public void Explode(List<Rigidbody> smallCubes)
    {
        for (int i = 0; i < smallCubes.Count; i++)
            smallCubes[i].AddExplosionForce(_explosionForce, transform.position, _radius);
    }
}
