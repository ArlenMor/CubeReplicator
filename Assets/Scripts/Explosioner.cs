using System.Collections.Generic;
using UnityEngine;

public class Explosioner : MonoBehaviour
{
    [SerializeField, Range(5, 30)] private float _radius;
    [SerializeField, Range(100, 500)] private float _explosionForce;

    private bool _explodeThisFrame = false;
    private Transform _centerExplosion;
    private List<Rigidbody> _explosiveObjects;

    private void FixedUpdate()
    {
        if (_explodeThisFrame)
        {
            if(_centerExplosion != null)
            {
                for (int i = 0; i < _explosiveObjects.Count; i++)
                {
                    if (_explosiveObjects[i] != null)
                        _explosiveObjects[i].AddExplosionForce(_explosionForce, _centerExplosion.position, _radius);
                }
            }

            _explodeThisFrame = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_centerExplosion != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_centerExplosion.position, _radius);
        }
    }

    public void Explode(Transform centerExplosion, List<Rigidbody> explosiveObjects)
    {

        if (centerExplosion != null && explosiveObjects != null)
        {
            _explodeThisFrame = true;
            _centerExplosion = centerExplosion;
            _explosiveObjects = explosiveObjects;
        }

        /*for (int i = 0; i < explosiveObjects.Count; i++)
            explosiveObjects[i].AddExplosionForce(_explosionForce, centerExplosion.position, _radius);*/
    }
}