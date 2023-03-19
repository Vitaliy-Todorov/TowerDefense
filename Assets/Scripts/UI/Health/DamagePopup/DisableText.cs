using UnityEngine;

public class DisableText : MonoBehaviour
{
    private float _speed;
    private float _time;

    public void Construct(float speed, float time)
    {
        _speed = speed;
        _time = time;
    }

    private void Update()
    {
        transform.Translate(transform.up * _speed * Time.deltaTime, Space.World);
        Destroy(gameObject, _time);
    }
}