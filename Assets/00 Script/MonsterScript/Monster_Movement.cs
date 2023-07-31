using System.Collections;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Monster_Movement : MonoBehaviour
{
    [SerializeField] float _angle = 0;
    [SerializeField] MonsterState _monsterState = MonsterState.IDLE;
    Rigidbody2D _rigi;
    [SerializeField] int _lengthPatrol = 20;
    // Start is called before the first frame update
    public Animator _animator;
    void Start()
    {
        _animator = this.GetComponentInChildren<Animator>();
        _rigi = this.GetComponent<Rigidbody2D>();
    }
    public enum MonsterState
    {
        IDLE = 0,
        RUN = 1,
        HIT = 2,
        ATTACK = 3,
    }
    // Update is called once per frame
    void Update()
    {
        PlayerDetect();
        MonsterMove();
        AnimationCheck();
        UpdateAnimation(_monsterState);
    }
    public bool _isMove, _isHit, _isIdle, _isAttack;
    public bool IsHit { get => _isHit; set => _isHit = value; }
    public bool IsAttack { get => _isAttack; set => _isAttack = value; }

    public void UpdateAnimation(MonsterState monsterState)
    {
        for (int i = 0; i <= (int)MonsterState.ATTACK; i++)
        {
            string stateName = ((MonsterState)i).ToString();
            if (monsterState == (MonsterState)i)
            {
                _animator.SetBool(stateName, true);
            }
            else
            {
                _animator.SetBool(stateName, false);
            }
        }
    }
    void AnimationCheck()
    {
        if (_isMove) { _monsterState = MonsterState.RUN; }
        if (_isHit) { _monsterState = MonsterState.HIT; }
        if (_isIdle) { _monsterState = MonsterState.IDLE; }
        if (_isAttack) { _monsterState = MonsterState.ATTACK; }
    }
    //----------------------------------------
    // Move Condition 

    [SerializeField] LayerMask _layerMask;
    [SerializeField] Vector3 _playerPos;
    [SerializeField] public bool _isPlayer;
    [SerializeField] float _playerDetectOffset_X = 0, _playerDetectOffset_Y = 0;
    void PlayerDetect()
    {
        Vector3 Offset = new Vector3(_playerDetectOffset_X, _playerDetectOffset_Y, 0);
        Vector2 dir = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad)) * -this.transform.localScale.normalized.x;
        RaycastHit2D hit1 = Physics2D.Raycast(this.transform.position + Offset, dir, _lengthPatrol, _layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(this.transform.position + Offset, -dir, _lengthPatrol, _layerMask);
        Debug.DrawRay(this.transform.position + Offset, dir.normalized * _lengthPatrol, Color.red);
        Debug.DrawRay(this.transform.position + Offset, -dir.normalized * _lengthPatrol, Color.red);
        if (hit1.collider != null)
        {
            _isPlayer = true;
            _playerPos = hit1.collider.gameObject.transform.position;
        }
        else if (hit2.collider != null)
        {
            _isPlayer = true;
            _playerPos = hit2.collider.gameObject.transform.position;
        }
        else
        {
            _isPlayer = false;
        }
    }
    //----------------------------------------
    [SerializeField] Transform[] _wayPoints;
    [SerializeField] float _speed;
    float _restSpeed = 0, _currentSpeed;
    public float Speed { get => _speed; set => _speed = value; }
    public float RestSpeed { get => _restSpeed; set => _restSpeed = value; }
    public float CurrentSpeed { get => _currentSpeed; set => _currentSpeed = value; }
    Vector2 _dir;
    int _currentPoint = 0;
    void AutoPatrol()
    {
        if (Vector2.Distance(this.transform.position, _wayPoints[_currentPoint].position) < 1f)
        {
            _currentPoint++;
            if (_currentPoint >= _wayPoints.Length) { _currentPoint = 0; }
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position, _wayPoints[_currentPoint].position, Time.deltaTime * _currentSpeed);
        _dir = this.transform.position - _wayPoints[_currentPoint].position;
    }
    //----------------------------------------
    void MonsterMove()
    {
        if (_currentSpeed != 0)
        {

            _isMove = true;
        }
        else
        {
            _isMove = false;
        }
        if (_isPlayer)
        {
            _isAttack = true;
            if (!_isHit) { _currentSpeed = _speed; } else { _currentSpeed = _restSpeed; }
            this.transform.position = Vector3.MoveTowards(this.transform.position, _playerPos, Time.deltaTime * _currentSpeed * 2f);
            _dir = this.transform.position - _playerPos;
            FlipFace();
        }
        else
        {
            _isAttack = false;
            _currentSpeed = _speed;
            AutoPatrol();
            FlipFace();
        }
    }
    //----------------------------------------
    void FlipFace()
    {
        if (_dir.normalized.x > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_dir.normalized.x < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            return;
        }
    }
    //---------------------------------------------------------------------


    //Death Condition
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.transform.CompareTag("FeetAttack"))
        {
            _isHit = true;_isIdle = false;_isAttack = false;_isMove=false;
            _currentSpeed = _restSpeed;
            Invoke("Death", 0.5f);
            MusicPlayer.Instant.PlaySFX("MonsterDeath");
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            _currentSpeed = _restSpeed;
        }
        else
        {
            _currentSpeed = _speed;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _currentSpeed = _speed;
    }
    void Death()
    {
        this.gameObject.SetActive(false);
    }
}
