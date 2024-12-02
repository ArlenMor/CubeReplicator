using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CubeReplicator))]
public class Cube : MonoBehaviour
{
    private float _reduceMultiplyChanceCoef = 2f;

    private CubeReplicator _spawner;
    private Explosioner _explosioner;

    private ColorChanger _colorChanger = new ColorChanger();
    private MeshRenderer _meshRenderer;

    [SerializeField, Range(1, 10)] private float _explosionRadius;
    [SerializeField, Range(100, 500)] private float _explosionForce;

    public Rigidbody Rigidbody { get; private set; }
    public float MultiplyChance { get; private set; }

    private void Awake()
    {
        _spawner = GetComponent<CubeReplicator>();
        Rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _explosioner = new Explosioner(_explosionRadius, _explosionForce);

        MultiplyChance = 1f;

        _meshRenderer.material.color = _colorChanger.GetRandomColor();
    }

    public void Init(float multiplyChanceBigCube)
    {
        MultiplyChance = multiplyChanceBigCube / _reduceMultiplyChanceCoef;
    }

    public void Explode()
    {
        _spawner.Replicate(this, out List<Rigidbody> rigidbodyOfCreatedCubes);

        _explosioner.Explode(transform, rigidbodyOfCreatedCubes);
    }
}