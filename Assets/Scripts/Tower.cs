using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float shootTimerMax;
    
    private const float LookForTargetTimerMax = .2f;
    private Vector3 _projectileSpawnPosition;
    private float _lookForTargetTimer;
    private float _shootTimer;
    private Enemy _targetEnemy;

    private void Awake()
    {
        _projectileSpawnPosition = transform.Find("ProjectileSpawnPosition").position;
        _lookForTargetTimer = LookForTargetTimerMax;
        _shootTimer = shootTimerMax;
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void LookForTarget()
    {
        const float targetMaxRadius = 20f;
        var colliders = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        
        foreach (var col in colliders)
        {
            var enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (_targetEnemy == null)
                    _targetEnemy = enemy;
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, _targetEnemy.transform.position))
                        _targetEnemy = enemy;
                }
            }
        }
    }
    
    private void HandleTargeting()
    {
        _lookForTargetTimer -= Time.deltaTime;
        if (_lookForTargetTimer < 0)
        {
            _lookForTargetTimer = LookForTargetTimerMax;
            LookForTarget();
        }
    }

    private void HandleShooting()
    {
        if (_targetEnemy == null)
            return;
            
        _shootTimer -= Time.deltaTime;
        if (_shootTimer < 0)
        {
            _shootTimer = shootTimerMax;
            ArrowProjectile.Create(_projectileSpawnPosition, _targetEnemy);
        }
    }
}
