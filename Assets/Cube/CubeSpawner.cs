using UnityEngine;
using System;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    private static string CubeLayer = nameof(CubeLayer);
    private static int LeftClick = 0;

    [SerializeField] private GameObject _cubePrefab;

    [SerializeField, Range(1, 2.5f)] private float _startScale;
    [SerializeField] private Camera _camera;
    [SerializeField, Range(1, 100)] float _baseChanceToDestroyAfterSpawn = 100f;

    private Ray _ray;

    private int _minNumbersOfSmallCube = 2;
    private int _maxNumbersOfSmallCube = 6;
    private float _reduceScaleCoef = 0.5f;
    private LayerMask _cubeLayerMask;

    private void Awake()
    {
        _cubeLayerMask = LayerMask.GetMask(CubeLayer);
    }


    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(_ray.origin, _ray.direction * Mathf.Infinity, Color.magenta);

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity, _cubeLayerMask))
        {
            if (Input.GetMouseButtonDown(LeftClick))
            {
                Cube cube;

                if (hit.collider.gameObject.TryGetComponent<Cube>(out cube))
                {
                    SpawnCubesFromCube(cube);
                }
            }
        }
    }

    private void SpawnCubesFromCube(Cube cube)
    {
        List<Collider> colliderOfCreatedCubes = new List<Collider>();
        System.Random rand = new System.Random();

        int numberOfCubes = rand.Next(_minNumbersOfSmallCube, _maxNumbersOfSmallCube);

        cube.gameObject.SetActive(false);

        if (cube.DestroyChance >= Convert.ToSingle(rand.NextDouble()))
        {
            for (int i = 0; i < numberOfCubes; i++)
            {
                Vector3 newScale = cube.transform.localScale * _reduceScaleCoef;
                Vector3 spawnPoint = new Vector3(cube.transform.position.x + Convert.ToSingle(rand.NextDouble() - rand.NextDouble()),
                                                    cube.transform.position.y + Convert.ToSingle(rand.NextDouble() - rand.NextDouble()),
                                                    cube.transform.position.z + Convert.ToSingle(rand.NextDouble() - rand.NextDouble()));

                GameObject newCubeObject = Instantiate(_cubePrefab, spawnPoint, Quaternion.identity);
                newCubeObject.transform.localScale = newScale;

                Collider collider;
                Cube newCube;

                if (newCubeObject.TryGetComponent<Cube>(out newCube))
                    newCube.ReduceDestroyChance(cube.DestroyChance);

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
}