using UnityEngine;

[RequireComponent(typeof(Grapple))]
[RequireComponent(typeof(LineRenderer))]
[DefaultExecutionOrder(100)]
public class GrappleRope : MonoBehaviour
{

    private LineRenderer _lr;
    private Spring _spring;
    private Grapple _grapple;

    [SerializeField]
    private Transform _gunTip;

    [SerializeField]
    private int _quality;
    [SerializeField]
    private float _velocity;
    [SerializeField]
    private float _strength;
    [SerializeField]
    private float _damper;
    [SerializeField]
    private float _waveHeight;
    [SerializeField]
    private float _waveCount;
    [SerializeField]
    private AnimationCurve _ropeCurve;
    [SerializeField]
    private GameObject _hook;

    public bool _test;

    private Vector3 _currentGrapplePosition;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _grapple = GetComponent<Grapple>();
        _spring = new Spring();
        _spring.SetTarget(0);
    }


    private void LateUpdate()
    {
        DrawRope();
    }

    private void DrawRope()
    {
        if (!_grapple.tethered)
        {
            _currentGrapplePosition = _gunTip.position;
            _spring.Reset();
            _hook.transform.localPosition = Vector3.zero;
            if (_lr.positionCount > 0)
            {
                _lr.positionCount = 0;
            }
            return;
        }

        if (_lr.positionCount == 0)
        {
            _spring.SetVelocity(_velocity);
            _lr.positionCount = _quality + 1;
        }

        _spring.SetDamper(_damper); // slows down the rope
        _spring.SetStrength(_strength); // strongs how the simulation of the rope
        _spring.Update(Time.deltaTime); 

        var grapplePoint = _grapple.tetherPoint;
        var gunTipPos = _gunTip.position;
        var up = Quaternion.LookRotation((grapplePoint - gunTipPos).normalized) * Vector3.up; // 

        //create line renderer
        _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);
        _hook.transform.position = Vector3.Lerp(_currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);
        for (var i = 0; i <= _quality; i++)
        {
            var delta = i / (float)_quality;
            //makes the wave effect of the rope
            var offset = up * _waveHeight * Mathf.Sin(delta * _waveCount * Mathf.PI) * _spring.Value * _ropeCurve.Evaluate(delta);

            _lr.SetPosition(i, Vector3.Lerp(gunTipPos, _currentGrapplePosition, delta) + offset);
            
        }
    }
}
