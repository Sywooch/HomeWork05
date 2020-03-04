using UnityEngine;

public class Movement : MonoBehaviour
{
    // поля для кнопок управления
    #region Controll
    [Header("Controll")]
    [SerializeField] private KeyCode _left = KeyCode.A;
    [SerializeField] private KeyCode _right = KeyCode.D;
    [SerializeField] private KeyCode _jump = KeyCode.Space;
    [SerializeField] private KeyCode _kick = KeyCode.LeftControl;
    #endregion

    // поля для настроек всяких
    #region Settings
    [Header("Settings")]
    // скорость
    [SerializeField] private float _speed = 4;
    // радиус "ощущения" поверхности
    [SerializeField] private float _groundCheckRadius = 0.5f;
    // ускорение прыжка
    [SerializeField] private float _jumpForce = 6;
    #endregion

    #region Other
    [Header("Other")]
    // не знаю как названить отсюда, потому перетянем на это полде в инспекторе.
    [SerializeField] private Transform _groundCheck = null;
    // маска слоя - что считать поверхностью, как тут назначить я пока не понял.
    [SerializeField] private LayerMask _whatIsGround;
    #endregion

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
        //пересекает ли поверхность "_whatIsGround" окружнось радиуса "_groundCheckRadius" с центром в "_groundCheck.position"
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);

        //движение влево
        if (Input.GetKey(_left))
        {
            if (_facingRight)
                Flip();
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }

        // движение вправо
        if (Input.GetKey(_right))
        {
            if (!_facingRight)
                Flip();
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }

        // переключение анимаций согласно назначенным клавишам(наверняка есть лучше решение, но я его не знаю)
        // анимация бега по нажатию клавиш влево/вправо
        // бежим по поверхности, в полете анимация бега отключается.

        if ((Input.GetKey(_left) || Input.GetKey(_right)) && _isGrounded == true)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }

        // анимация атаки
        if (Input.GetKey(_kick))
        {
            _animator.SetTrigger("kick");
        }


        if (Input.GetKeyDown(_jump))
        {
            // сброс кол-ва прыжков если стоим на поверхности
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

    // разворот
    void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
