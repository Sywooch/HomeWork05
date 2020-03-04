using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private Transform[] _points;
    private int _currentPoint;
    private bool _facingRight = true;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("isPatrol", true);
    }

    private void Update()
    {
        Transform target = _points[_currentPoint];
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }

            Flip();
            _facingRight = false;
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}