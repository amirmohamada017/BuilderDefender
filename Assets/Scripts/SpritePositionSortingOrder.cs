using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void LateUpdate()
    {
        _spriteRenderer.sortingOrder += -(int)(transform.position.y * 10);
        
        if (runOnce)
        {
            Destroy(this);
        }
    }
}
