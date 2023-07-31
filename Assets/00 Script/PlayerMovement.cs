using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using Unity.VisualScripting;
//using Unity.VisualScripting.Dependencies.Sqlite;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _jumpForce = 300f, _angle = -90;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] PlayerState _playerState = PlayerState.IDLE;
    [SerializeField] AnimationControllerBase _anim;
    Rigidbody2D _rigi;
    Collider2D _colli;
    private float _dirX;
    private BoxCollider2D _collider;
    public enum PlayerState
    {
        IDLE = 0,
        RUN = 1,
        JUMP = 2,
        FALL = 3,
        HIT = 4,
        DOUBLEJUMP = 5,
        WALLSLIDE = 6,
        DEATH = 7,
        DASH = 8,

    }


    void Start()
    {
        _vectorGravity = new Vector2(0, -Physics2D.gravity.y);
        _rigi = this.GetComponent<Rigidbody2D>();
        _colli = this.GetComponent<Collider2D>();
        _collider = this.GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (_isDash)
        {
            return;
        }
        IsOnGround();
        WallSlide();
        WallJump();
        if (!_isWallSlide)
        {
            MoveX();
            Jump();
            GetDash();
        }
        UpdateState();
        CheckDeath();
        _anim.UpdateAnimation(_playerState);


       // SlopeDetect();
    }


    void CheckDeath()
    {
        if (this.GetComponent<PlayerHealth>().Health <= 0)
        {
            _playerState = PlayerState.DEATH;
        }
    }


    private void UpdateState()
    {
        if (_isMove) { _playerState = PlayerState.RUN; }
        if (_isIdle) { _playerState = PlayerState.IDLE; }
        if (_isJump) { _playerState = PlayerState.JUMP; }
        if (_isFall) { _playerState = PlayerState.FALL; }
        if (_is2xJump) { _playerState = PlayerState.DOUBLEJUMP; }
        if (_isDash) { _playerState = PlayerState.DASH; }
        if (_isHit) { _playerState = PlayerState.HIT; }
        if (_isWallSlide) { _playerState = PlayerState.WALLSLIDE; }
    }


    #region Jump
    [Header("Jump System")]
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _fallMultiplier;
    [SerializeField] float _jumpMultiplier;
    [SerializeField] float _jumpTime;
    bool _isGround;
    public bool IsGround { get => _isGround; set => _isGround = value; }
    bool _canDoubleJump = false;
    bool _isJump, _is2xJump, _isFall;
    Vector2 _vectorGravity;
    void Jump()
    {

        if (!_isGround && _canDoubleJump && Input.GetButtonDown("Jump"))
        {
            _is2xJump = true;
        }
        else if (_isGround)
        {
            _is2xJump = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (_isGround || _canDoubleJump)
            {
                _canDoubleJump = !_canDoubleJump;
                _rigi.velocity = new Vector2(0, _jumpForce);
                MusicPlayer.Instant.PlaySFX("Jump");
            }
        }
        if (_rigi.velocity.y > 0.1f)
        {
            _isJump = true;
            _rigi.velocity = new Vector2(_rigi.velocity.x, _rigi.velocity.y);
        }
        else { _isJump = false; }

        if (_rigi.velocity.y < -0.1f)
        {
            _isFall = true;
            _rigi.velocity -= _vectorGravity * _fallMultiplier * Time.deltaTime;
        }
        else { _isFall = false; }
    }

    private void IsOnGround()
    {
        _isGround = Physics2D.OverlapBox(_groundCheck.position, new Vector2(0.55f, 0.025f), 0f, _layerMask);
    }
    #endregion

    #region Move
    [Header("Move System")]
    [SerializeField] float moveSpeed = 10, accelertion = 7, decceleration = 7, velPower = 0.9f;
    bool _isMove, _isIdle;
    void MoveX()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        float targetSpeed = _dirX * moveSpeed;
        float speedDif = targetSpeed - _rigi.velocity.x;
        float acceRate = (Mathf.Abs(targetSpeed) > 0.01f ? accelertion : decceleration);
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * acceRate, velPower) * Mathf.Sign(speedDif);
        _rigi.AddForce(movement * Vector2.right);
        if (_dirX == 0)
        {
            _isIdle = true;
            _isMove = !_isIdle;
            return;
        }
        else
        {
            _isIdle = !_isMove;
            this.transform.localScale = new Vector3(_dirX, 1, 1);
        }
        if (_rigi.velocity.x != 0) { _isMove = true; }
    }
    #endregion

    #region Dash
    [Header("Dash System")]
    private bool _canDash = true, _isDash;
    float _dashPower = 20f, _dashTime = 0.2f, _dashCooldown = 1f;
    [SerializeField] TrailRenderer _trailRenderer;

    void GetDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
            MusicPlayer.Instant.PlaySFX("Dash");
        }
    }
    IEnumerator Dash()
    {
        _canDash = false;
        _isDash = true;
        float originnalGravity = _rigi.gravityScale;
        _rigi.gravityScale = 0f;
        _rigi.velocity = new Vector2(transform.localScale.x * _dashPower, 0f);
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(_dashTime);
        _trailRenderer.emitting = false;
        _rigi.gravityScale = originnalGravity;
        _isDash = false;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
    #endregion

    #region GetHit
    bool _isHit = false;
    public void GetHit()
    {
        StartCoroutine(isHit());
    }
    IEnumerator isHit()
    {
        _isHit = true;
        yield return new WaitForSeconds(0.5f);
        _isHit = false;
    }
    #endregion

    #region WallSlide & WallJump
    [Header("WallSlide & WallJump")]
    [SerializeField] Transform _wallCheck;
    [SerializeField] LayerMask _wallLayer;
    [SerializeField] bool _isWallSlide;
    [SerializeField] Vector2 _wallJumpPower = new Vector2(20f, 10f);
    [SerializeField] float _wallSlideSpeed = 0.5f;
    bool _isWallJump;
    float _wallJumpDir, _wallJumptime = 0.2f, _wallJumpcounter, _wallJumpDuration = 0.4f;

    private bool IsWall()
    {
        return Physics2D.OverlapCircle(_wallCheck.position, 0.2f, _wallLayer);
    }
    private void WallSlide()
    {
        if (IsWall() && !_isGround && _dirX != 0f)
        {
            _isWallSlide = true;
            _rigi.velocity = new Vector2(_rigi.velocity.x, Mathf.Clamp(_rigi.velocity.y, -_wallSlideSpeed, 1.5f));
        }
        else
        {
            _isWallSlide = false;
        }
    }
    private void WallJump()
    {
        if (_isWallSlide)
        {
            _isWallJump = false;
            _wallJumpDir = -this.transform.localScale.x;
            _wallJumpcounter = _wallJumptime;
            CancelInvoke("StopWallJump");
        }
        else
        {
            _wallJumpcounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && _wallJumpcounter > 0f)
        {
            _isWallJump = true;
            _rigi.velocity = new Vector2(_wallJumpDir * _wallJumpPower.x, _wallJumpPower.y);
            _wallJumpcounter = 0f;
            if (this.transform.localScale.x != _wallJumpDir)
            {
                Vector3 localScale = this.transform.localScale;
                localScale.x *= -1f;
                this.transform.localScale = localScale;
            }
            Invoke("StopWallJump", _wallJumpDuration);
        }
    }
    void StopWallJump()
    {
        _isWallJump = false;
    }

    #endregion
    void MultipleRaytest()
    {
        float length = 1.3f;
        Vector2 dir = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad));
        Debug.DrawRay(this.transform.position, dir.normalized * length, Color.red);
        RaycastHit2D[] hits = new RaycastHit2D[10];
        _colli.Cast(dir, hits, 100, false);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.DrawRay(hit.point, hit.normal, Color.yellow);
            }
        }
    }
    void SlopeDetect()
    {
        float length = 2f;
        Vector2 dir = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad));
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir, length, _layerMask);
        Debug.DrawRay(this.transform.position, dir.normalized * length, Color.red);
        if (hit.collider != null)
        {
            //Debug.Log("Name"+hit.collider.gameObject.name);
            //Debug.Log("Tag" + hit.collider.gameObject.tag);
            Debug.DrawRay(hit.point, hit.normal, Color.yellow);
        }
    }
}
