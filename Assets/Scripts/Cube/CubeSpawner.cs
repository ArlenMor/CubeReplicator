using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(InputSystem))]
public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance = null;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private InputSystem _inputSystem;

    private int _minNumbersSmallCube = 2;
    private int _maxNumbersSmallCube = 6;
    private float _reduceScaleCoef = 0.5f;

    private System.Random _random = new System.Random();

    public event Action<Transform, List<Rigidbody>> OnExplode;

    private void Start()
    {
        if (Instance = null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        _inputSystem.OnCubePressed += SpawnSmallCubesFromBigCube;
    }

    private void SpawnSmallCubesFromBigCube(Cube bigCube)
    {
        List<Rigidbody> rigidbodyOfCreatedCubes = new List<Rigidbody>();

        bigCube.gameObject.SetActive(false);

        if (bigCube.MultiplyChance >= _random.NextDouble())
        {
            int numberOfCubes = UnityEngine.Random.Range(_minNumbersSmallCube, _maxNumbersSmallCube);

            for (int i = 0; i < numberOfCubes; i++)
            {
                Cube smallCube = InstantiateSmallCubeFromBigCube(bigCube);

                if (smallCube.Rigidbody)
                    rigidbodyOfCreatedCubes.Add(smallCube.Rigidbody);
            }

            OnExplode?.Invoke(bigCube.transform, rigidbodyOfCreatedCubes);
        }

        Destroy(bigCube.gameObject);
    }

    private Cube InstantiateSmallCubeFromBigCube(Cube bigCube)
    {
        Vector3 newScale = bigCube.transform.localScale * _reduceScaleCoef;

        Vector3 spawnPoint = new Vector3(bigCube.transform.position.x + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()),
                                            bigCube.transform.position.y + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()),
                                            bigCube.transform.position.z + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()));

        Cube newSmallCube = Instantiate(_cubePrefab, spawnPoint, Quaternion.identity);
        newSmallCube.transform.localScale = newScale;
        newSmallCube.Init(bigCube.MultiplyChance);

        return newSmallCube;
    }
}