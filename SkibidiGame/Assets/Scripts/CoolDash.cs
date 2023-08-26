using System.Collections;
using UnityEngine;

public class CoolDash : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private int _currentPoint;
    [SerializeField] private Vector3 _path;
    [SerializeField] private float _afterImageCount;
    [SerializeField] private float _opacity;
    [SerializeField] private float _lifeTime;
    [SerializeField] private Color _color;
    [SerializeField] private GameObject _sound;

    private void Start()
    {
        _currentPoint = 0;
        transform.position = _points[_currentPoint].position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Move();
            AfterImage();
        }
    }
    private void Move()
    {
        _currentPoint++;
        if (_currentPoint == _points.Length) _currentPoint = 0;
        transform.position = _points[_currentPoint].position;
        Instantiate(_sound, transform.position, Quaternion.identity);
    }

    private void AfterImage()
    {
        if (_currentPoint == 0) _path = _points[_currentPoint].position - _points[_points.Length - 1].position;
        else _path = _points[_currentPoint].position - _points[_currentPoint - 1].position;
        for (int i = 0; i < _afterImageCount; i++)
        {
            InstantiateAfterImage(_path * (i / _afterImageCount), (i / _afterImageCount) * _lifeTime, (i / _afterImageCount));
        }
    }

    private void InstantiateAfterImage(Vector3 position, float lifeTime, float alpha)
    {
        _color.a = alpha;
        int ind;
        if (_currentPoint == 0) ind = _points.Length - 1;
        else ind = _currentPoint - 1;
        var afterImage = Instantiate(gameObject, _points[ind].position + position, Quaternion.identity);
        Destroy(afterImage.GetComponent<Collider>());
        Destroy(afterImage.GetComponent<CoolDash>());
        afterImage.GetComponent<Renderer>().material.color = _color;
        StartCoroutine(DestroyThing(afterImage, lifeTime));
    }

    private IEnumerator DestroyThing(GameObject thing, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(thing);
    }
}
