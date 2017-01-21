using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour {

    [SerializeField]
    private float _minPosZ;
    [SerializeField]
    private float _maxPosZ;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _waveLength;

    [SerializeField]
    private float _delay;
    [SerializeField]
    private float _timeItTakesToCompleteWave;

    private float _currentSpeed;
    private float _timePassed;

    private bool _running;

    void Start()
    {
        _timePassed = _delay;
        _running = true;
        //StartCoroutine(WaveTick());
    }
    
    //IEnumerator WaveTick()
    //{
    //    _timePassed = 0;
    //    int waveDirection = 1;
    //    while (_running)
    //    {
    //        _timePassed += Time.deltaTime;
    //        Mathf.Clamp01(_timePassed / _timeItTakesToCompleteWave);
    //        _currentSpeed = Mathf.Sin();
    //        Debug.Log(_currentSpeed);
    //
    //        transform.Translate(_currentSpeed * transform.forward * _speed);
    //
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    void Update()
    {
        _timePassed += Time.deltaTime;
        
        float sined = (1 + Mathf.Sin(_timePassed * _speed))*0.5f;

        //float newZ = Mathf.Clamp(sined * _minPosZ, _minPosZ, _maxPosZ);
        float newZ = sined * _waveLength + _minPosZ;

        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, newZ);
        transform.position = newPos;
        
        //float dif = _lastPosZ - _startingPosZ;
        //
        //float move = _currentPos * dif;
        //
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z * _currentPos);

        //transform.Translate(_currentSpeed * transform.forward * _speed);
        
    }
}

