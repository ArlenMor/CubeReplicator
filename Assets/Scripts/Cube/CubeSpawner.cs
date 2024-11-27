using UnityEngine;
using System;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    private const string CubeLayer = nameof(CubeLayer);
    private const int PressLeftClick = 0;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Camera _camera;

    private int _minNumbersOfSmallCube = 2;
    private int _maxNumbersOfSmallCube = 6;
    private float _reduceScaleCoef = 0.5f;

    private LayerMask _cubeLayerMask;
    private System.Random _random = new System.Random();

    private void Awake()
    {
        _cubeLayerMask = LayerMask.GetMask(CubeLayer);
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(PressLeftClick))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _cubeLayerMask))
            {
                if (hit.collider.gameObject.TryGetComponent<Cube>(out Cube cube))
                    SpawnSmallCubesFromBigCube(cube);
            }
        }
    }

    private void SpawnSmallCubesFromBigCube(Cube bigCube)
    {
        List<Rigidbody> rigidbodyOfCreatedCubes = new List<Rigidbody>();
        
        int numberOfCubes = _random.Next(_minNumbersOfSmallCube, _maxNumbersOfSmallCube + 1);

        bigCube.gameObject.SetActive(false);

        if (bigCube.MultiplyChance >= _random.NextDouble())
        {
            for (int i = 0; i < numberOfCubes; i++)
            {
                Cube smallCube = InstantiateSmallCubeFromBigCube(bigCube);

                if (smallCube.Rigidbody)
                    rigidbodyOfCreatedCubes.Add(smallCube.Rigidbody);
            }

            bigCube.Explode(rigidbodyOfCreatedCubes);
        }

        Destroy(bigCube.gameObject);
    }

    private Cube InstantiateSmallCubeFromBigCube(Cube bigCube)
    {
        Vector3 newScale = bigCube.transform.localScale * _reduceScaleCoef;

        Vector3 spawnPoint = new Vector3(bigCube.transform.position.x + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()),
                                            bigCube.transform.position.y + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()),
                                            bigCube.transform.position.z + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()));

        Cube newCube = Instantiate(_cubePrefab, spawnPoint, Quaternion.identity);
        newCube.transform.localScale = newScale;
        newCube.Init(bigCube.MultiplyChance);

        return newCube;
    }
}