using UnityEngine;

public static class UtilsClass
{
    private static Camera _mainCamera;
    
    public static Vector3 GetMouseWorldPosition()
    {
        if (_mainCamera == null && Camera.main == null)
            return Vector3.zero;
        if (_mainCamera == null)
            _mainCamera = Camera.main;
        
        var mouseWorldPosition = _mainCamera!.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        var radians = Mathf.Atan2(vector.y, vector.x);
        var degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }
}
