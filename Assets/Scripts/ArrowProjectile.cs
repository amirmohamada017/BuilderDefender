using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private Enemy _targetEnemy;
    private Vector3 _lastMoveDir;
    private float _timeToDestroy = 2f;

    public static ArrowProjectile Create(Vector3 position, Enemy targetEnemy)
    {
        var arrowProjectilePrefab = Resources.Load<Transform>(nameof(ArrowProjectile));
        var arrowTransform = Instantiate(arrowProjectilePrefab, position, Quaternion.identity);
        
        var arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(targetEnemy);
        
        return arrowProjectile;
    }
    
    private void Update()
    {
        Vector3 moveDir;
        if (_targetEnemy != null)
        {
            moveDir = (_targetEnemy.transform.position - transform.position).normalized;
            _lastMoveDir = moveDir;
        }
        else
            moveDir = _lastMoveDir;
        
        const float moveSpeed = 20f;
        transform.position += moveDir * (moveSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

        if (_targetEnemy != null) return;
        
        _timeToDestroy -= Time.deltaTime;
        if (_timeToDestroy < 0)
            Destroy(gameObject);
    }
    
    private void SetTarget(Enemy enemy)
    {
        _targetEnemy = enemy;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            const int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            
            Destroy(gameObject);
        }
    }
}
