using UnityEngine;

public class Raycaster : MonoBehaviour
{
    private const int MouseClickNumber = 0;

    [SerializeField] private Camera _camera;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private LayerMask _cubeLayerMask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseClickNumber))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _cubeLayerMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out Cube bigCube))
                    _cubeSpawner.SpawnSmallCubes(bigCube);
            }
        }
    }
}