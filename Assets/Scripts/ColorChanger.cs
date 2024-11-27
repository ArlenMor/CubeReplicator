using UnityEngine;

public static class ColorChanger
{
    public static void GetRandomColor(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out MeshRenderer renderer))
            renderer.material.color = Random.ColorHSV();
        else
            Debug.LogWarning($"�� ������� {gameObject.name} ����������� {typeof(MeshRenderer)}.");
    }
}
