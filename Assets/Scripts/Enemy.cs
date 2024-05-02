using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _targetTransform;
    private Rigidbody2D _rigidbody2D;
    private const float LookForTargetTimerMax = .2f;
    private float _lookForTargetTimer;
    private HealthSystem _healthSystem;

    public static Enemy Create(Vector3 position)
    {
        var enemyPrefab = Resources.Load<Enemy>(nameof(Enemy));
        var instance = Instantiate(enemyPrefab, position, Quaternion.identity);
        
        var enemy = instance.GetComponent<Enemy>();
        return enemy;
    }
    private void Start()
    {
        _targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _healthSystem = GetComponent<HealthSystem>();
        
        _healthSystem.OnDied += HealthSystem_OnDied;
        
        _lookForTargetTimer = Random.Range(0f, LookForTargetTimerMax);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargeting();
    }

    private void HandleMovement()
    {
        if (_targetTransform != null)
        {
            const float moveSpeed = 10f;
            var moveDir = (_targetTransform.position - transform.position).normalized;
            _rigidbody2D.velocity = moveDir * moveSpeed;
        }
        else
            _rigidbody2D.velocity = Vector2.zero;
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

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        var building = collision2D.gameObject.GetComponent<Building>();
        if (building != null)
        {
            var healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(15);

            Destroy(gameObject);
        }
    }

    private void LookForTarget()
    {
        const float targetMaxRadius = 10f;
        var colliders = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        
        foreach (var col in colliders)
        {
            var building = col.GetComponent<Building>();
            if (building != null)
            {
                if (_targetTransform == null)
                    _targetTransform = building.transform;
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, _targetTransform.position))
                        _targetTransform = building.transform;
                }
            }
        }
        
        if (_targetTransform == null)
            _targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
    }
    
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }
}
