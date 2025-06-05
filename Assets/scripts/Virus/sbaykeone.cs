using UnityEngine;

public class sbaykeone : G_AI
{

    [SerializeField] private float jumpTimer = 0.5f, jumpForce = 10f;

    private float _timer;
    public override void Move()
    {
        if (PublicData.pause || PublicData.gameover) return;
        if (_timer < jumpTimer) _timer += Time.deltaTime;
        else
        {
            _timer = 0;

            Vector2 _playerTangint = Random.insideUnitCircle;
            _playerTangint.Normalize();

            _rb.AddForce(_playerTangint * jumpForce, ForceMode2D.Impulse);
        }

        _rb.AddForce(m_playerdir.normalized * Speed * Time.deltaTime * 10000f, ForceMode2D.Force);
        _rb.AddTorque(_rotation, ForceMode2D.Force);
    }
}
