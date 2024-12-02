using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private float _reduceMultiplyChanceCoef = 2f;

    private ColorChanger _colorChanger = new ColorChanger();
    private MeshRenderer _meshRenderer;
    private List<Rigidbody> _rigidbodyReplicatedCubes = new List<Rigidbody>();

    public CubeReplicator Replicator { get; private set; }
    public Explosioner Explosioner { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public float MultiplyChance { get; private set; }

    private void Awake()
    {
        MultiplyChance = 1f;

        Rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = _colorChanger.GetRandomColor();
    }

    private void Start()
    {
        if (Replicator == null)
            Replicator = FindFirstObjectByType<CubeReplicator>();

        if (Explosioner == null)
            Explosioner = FindFirstObjectByType<Explosioner>();
    }

    public void Init(float multiplyChanceBigCube, CubeReplicator replicator, Explosioner explosioner)
    {
        MultiplyChance = multiplyChanceBigCube / _reduceMultiplyChanceCoef;

        Explosioner = explosioner;
        Replicator = replicator;
    }

    public void Replicate()
    {
        _rigidbodyReplicatedCubes = Replicator.Replicate(this);
    }

    public void Explode()
    {
        if(_rigidbodyReplicatedCubes.Count != 0)
            Explosioner.Explode(transform, _rigidbodyReplicatedCubes);
    }
}