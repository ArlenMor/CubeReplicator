using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance = null;

    private const int MouseNumber = 0;
    private const string CubeLayer = nameof(CubeLayer);

    [SerializeField] private Camera _camera;

    private LayerMask _cubeLayerMask;

    public event Action<Cube> OnCubePressed;

    private void Start()
    {
        if (Instance = null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        _cubeLayerMask = LayerMask.GetMask(CubeLayer);
    }

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(MouseNumber))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _cubeLayerMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out Cube cube))
                    OnCubePressed?.Invoke(cube);
            }
        }
    }
}
