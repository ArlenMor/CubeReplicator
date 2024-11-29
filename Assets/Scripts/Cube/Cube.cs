using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private float _reduceMultiplyChanceCoef = 2f;

    private ColorChanger _colorChanger = new ColorChanger();
    private MeshRenderer _meshRenderer;

    public Rigidbody Rigidbody { get; private set; }
    public float MultiplyChance { get; private set; }

    private void Awake()
    {
        MultiplyChance = 1f;

        Rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _meshRenderer.material.color = _colorChanger.GetRandomColor();
    }

    public void Init(float multiplyChanceBigCube)
    {
        MultiplyChance = multiplyChanceBigCube / _reduceMultiplyChanceCoef;
    }
}