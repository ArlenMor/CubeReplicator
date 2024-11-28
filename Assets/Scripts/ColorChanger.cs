using UnityEngine;

public class ColorChanger
{
    public void SetRandomColor(MeshRenderer renderer)
    {
        renderer.material.color = Random.ColorHSV();
    }
}