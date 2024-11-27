using UnityEngine;
using System;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    private static string CubeLayer = nameof(CubeLayer);
    private static int LeftClick = 0;

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Camera _camera;

    private int _minNumbersOfSmallCube = 2;
    private int _maxNumbersOfSmallCube = 6;
    private float _reduceScaleCoef = 0.5f;

    private Ray _ray;
    private LayerMask _cubeLayerMask;
    private System.Random _random = new System.Random();

    private void Awake()
    {
        _cubeLayerMask = LayerMask.GetMask(CubeLayer);
    }

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity, _cubeLayerMask))
        {
            if (Input.GetMouseButtonDown(LeftClick))
            {
                Cube cube;

                if (hit.collider.gameObject.TryGetComponent<Cube>(out cube))
                    SpawnSmallCubesFromBigCube(cube);
            }
        }
    }

    private void SpawnSmallCubesFromBigCube(Cube cube)
    {
        List<Collider> colliderOfCreatedCubes = new List<Collider>();
        
        int numberOfCubes = _random.Next(_minNumbersOfSmallCube, _maxNumbersOfSmallCube);

        cube.gameObject.SetActive(false);

        if (cube.MultiplyChance >= _random.NextDouble())
        {
            for (int i = 0; i < numberOfCubes; i++)
            {
                GameObject newCubeObject = InstantiateSmallCubeFromBigCube(cube);

                Collider collider;

                if (newCubeObject.TryGetComponent<Collider>(out collider))
                    colliderOfCreatedCubes.Add(collider);
            }

            cube.Explode(colliderOfCreatedCubes);
            Destroy(cube.gameObject);
        }
        else
        {
            Destroy(cube.gameObject);
        }
    }

    private GameObject InstantiateSmallCubeFromBigCube(Cube bigCube)
    {
        Vector3 newScale = bigCube.transform.localScale * _reduceScaleCoef;

        Vector3 spawnPoint = new Vector3(bigCube.transform.position.x + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()),
                                            bigCube.transform.position.y + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()),
                                            bigCube.transform.position.z + Convert.ToSingle(_random.NextDouble() - _random.NextDouble()));

        GameObject newCubeObject = Instantiate(_cubePrefab, spawnPoint, Quaternion.identity);
        newCubeObject.transform.localScale = newScale;

        Cube newCube;

        if (newCubeObject.TryGetComponent<Cube>(out newCube))
            newCube.ReduceMultiplyChance(bigCube.MultiplyChance);

        return newCubeObject;
    }
}