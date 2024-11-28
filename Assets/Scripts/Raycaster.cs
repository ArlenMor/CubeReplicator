using UnityEngine;

public class Raycaster : MonoBehaviour
{
    private const int MouseNumber = 0;

    [SerializeField] private Camera _camera;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private LayerMask _cubeLayerMask;

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(MouseNumber))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _cubeLayerMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out Cube bigCube))
                    _cubeSpawner.SpawnSmallCubes(bigCube);
            }
        }
    }
}