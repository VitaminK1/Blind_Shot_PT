using System.Threading;
using UnityEngine;

public abstract class BaseMonsterController : MonoBehaviour
{
	[SerializeField] protected int _damage;
	[SerializeField] protected Animator _animator;
	[SerializeField] protected Define.EnemyState _enemyState = Define.EnemyState.None;
	
	protected GameObject _lockTarget => Player.Instance.gameObject;
	protected readonly int _movingId = Animator.StringToHash("Moving");
	protected readonly int _attackId = Animator.StringToHash("Attack");
	protected readonly int _dieId = Animator.StringToHash("Die");
	public Define.EnemyState enemyState
	{
		get { return _enemyState; }
		set
		{
			if (_enemyState == value) return;
			
			_enemyState = value;
			OnEnemyStateChanged(_enemyState);
		}
	}

	void Update()
	{
		switch (enemyState)
		{
			case Define.EnemyState.None:
				break;
			case Define.EnemyState.Die:
				UpdateDie();
				break;
			case Define.EnemyState.Moving:
				UpdateMoving();
				break;
			case Define.EnemyState.Attack:
				UpdateAttack();
				break;
		}
	}

	protected void ChangeEnemyState(Define.EnemyState state)
	{
		enemyState = state;
	}

	protected virtual void Start()
	{
		Init();
	}

	protected virtual void Init()
	{
		ChangeEnemyState(Define.EnemyState.Moving);
	}

	protected virtual void UpdateDie() { }
	protected virtual void UpdateMoving() { }
	protected virtual void UpdateAttack() { }

	protected virtual void OnEnemyStateChanged(Define.EnemyState newState)
	{
		switch (newState)
		{
			case Define.EnemyState.Die:

				//_animator.SetBool(_dieId, true);
				break;
			case Define.EnemyState.Moving:
				//_animator.SetBool(_movingId, true);
				break;
			case Define.EnemyState.Attack:
				//_animator.SetBool(_attackId, true);
				break;
		}
	}

	public void AttackPlayer()
	{
		Player.Instance.Hit(_damage);
	}

	public void AttackedByPlayer()
	{
		ChangeEnemyState(Define.EnemyState.Die);
	}

	public void Despawn()
	{
		
	}

	protected void OnDestroy()
	{
		EnemySpawningPool.Instance.OnEnemyDespawn(this);
	}
}

