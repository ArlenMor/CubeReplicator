using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Raycaster))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private int _minNumbersSmallCube = 2;
    private int _maxNumbersSmallCube = 6;
    private float _reduceScaleCoef = 2f;

    private System.Random _random = new System.Random();

    public event Action<Transform, List<Rigidbody>> OnExplode;

    public void SpawnSmallCubes(Cube bigCube)
    {
        List<Rigidbody> rigidbodyOfCreatedCubes = new List<Rigidbody>();

        bigCube.gameObject.SetActive(false);

        if (bigCube.MultiplyChance >= _random.NextDouble())
        {
            int numberOfCubes = UnityEngine.Random.Range(_minNumbersSmallCube, _maxNumbersSmallCube + 1);

            for (int i = 0; i < numberOfCubes; i++)
            {
                Cube smallCube = InstantiateSmallCube(bigCube);

                if (smallCube.Rigidbody)
                    rigidbodyOfCreatedCubes.Add(smallCube.Rigidbody);
            }

            OnExplode?.Invoke(bigCube.transform, rigidbodyOfCreatedCubes);
        }

        Destroy(bigCube.gameObject);
    }

    private Cube InstantiateSmallCube(Cube bigCube)
    {
        Vector3 newScale = bigCube.transform.localScale / _reduceScaleCoef;

        Vector3 spawnPoint = new Vector3(bigCube.transform.position.x + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()),
                                            bigCube.transform.position.y + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()),
                                            bigCube.transform.position.z + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()));

        Cube newSmallCube = Instantiate(_cubePrefab, spawnPoint, Quaternion.identity);
        newSmallCube.transform.localScale = newScale;
        newSmallCube.Init(bigCube.MultiplyChance);

        return newSmallCube;
    }
}