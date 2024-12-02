using UnityEngine;
using System;
using System.Collections.Generic;

public class CubeReplicator : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private int _minNumbersSmallCube = 2;
    private int _maxNumbersSmallCube = 6;
    private float _reduceScaleCoef = 2f;

    private System.Random _random = new System.Random();

    public List<Rigidbody> Replicate(Cube cube)
    {
        List<Rigidbody> rigidbodyReplicatedCubes = new List<Rigidbody>();

        if (cube.MultiplyChance >= _random.NextDouble())
        {
            int numberOfCubes = UnityEngine.Random.Range(_minNumbersSmallCube, _maxNumbersSmallCube + 1);

            for (int i = 0; i < numberOfCubes; i++)
            {
                Cube smallCube = InstantiateReplicatedCube(cube);

                if (smallCube.Rigidbody)
                    rigidbodyReplicatedCubes.Add(smallCube.Rigidbody);
            }
        }

        Destroy(cube.gameObject);

        return rigidbodyReplicatedCubes;
    }

    private Cube InstantiateReplicatedCube(Cube cube)
    {
        Vector3 newScale = cube.transform.localScale / _reduceScaleCoef;

        Vector3 spawnPoint = new Vector3(cube.transform.position.x + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()),
                                            cube.transform.position.y,
                                            cube.transform.position.z + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()));

        Cube replicatedCube = Instantiate(_cubePrefab, spawnPoint, Quaternion.identity);
        replicatedCube.transform.localScale = newScale;
        replicatedCube.Init(cube.MultiplyChance, cube.Replicator, cube.Explosioner);

        return replicatedCube;
    }
}