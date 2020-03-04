using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private KeyCode _left = KeyCode.A;
    [SerializeField] private KeyCode _right = KeyCode.D;
    [SerializeField] private KeyCode _jump = KeyCode.Space;
    [SerializeField] private KeyCode _kick = KeyCode.LeftControl;
    [SerializeField] private float _speed = 4;
    [SerializeField] private float _groundCheckRadius = 0.5f;
    [SerializeField] private float _jumpForce = 6;
    [SerializeField] private Transform _groundCheck = null;
    [SerializeField] private LayerMask _whatIsGround;
    protected int _countJamp = 1;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private Animator _animator;
    private bool _facingRight = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);

        if (Input.GetKey(_left))
        {
            if (_facingRight)
                Flip();
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }

        if (Input.GetKey(_right))
        {
            if (!_facingRight)
                Flip();
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }

        if ((Input.GetKey(_left) || Input.GetKey(_right)) && _isGrounded == true)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(_kick))
        {
            _animator.SetTrigger("kick");
        }

        if (Input.GetKeyDown(_jump))
        {
            if (_isGrounded == true)
            {
                _countJamp = 1;
            }
            if (_countJamp >= 0)
            {
                _animator.SetTrigger("jump");
                _rigidbody2D.velocity = Vector2.up * _jumpForce;
                _countJamp--;
            }
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}