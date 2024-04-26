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
}
