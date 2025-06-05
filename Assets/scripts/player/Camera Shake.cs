using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float ShakeIntencety, numberofshakes;

    private Transform _camera;
    private Vector2 _R2Loc, _camera_pos;
    void Start()
    {
        _camera = Camera.main.transform;
    }

    public IEnumerator Camerashake()
    {
        for (int i = 0; i < numberofshakes; i++)
        {
            _camera_pos = _camera.position;
            _R2Loc = Random.insideUnitCircle;
            _R2Loc.Normalize();

            _camera.position = new Vector3(_camera_pos.x + _R2Loc.x * ShakeIntencety, _camera_pos.y + _R2Loc.y * ShakeIntencety, -10f);
            yield return new WaitForEndOfFrame();
            _camera.position = transform.position + new Vector3(0,0,-10f);
        }
        _camera.position = transform.position + new Vector3(0,0,-10f);
    }
}
