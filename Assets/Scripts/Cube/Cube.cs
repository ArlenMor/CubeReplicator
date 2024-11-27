using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private float _reduceMultiplyChanceCoef = 2f;

    public Rigidbody Rigidbody { get; private set; }
    public float MultiplyChance { get; private set; }

    private void Awake()
    {
        MultiplyChance = 1f;

        Rigidbody = GetComponent<Rigidbody>();

        ColorChanger.GetRandomColor(this.gameObject);
    }

    public void Init(float multiplyChanceBigCube)
    {
        MultiplyChance = multiplyChanceBigCube / _reduceMultiplyChanceCoef;
    }
}
