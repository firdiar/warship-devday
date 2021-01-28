using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]float smoothTime = 1;
    Vector3 _target = Vector2.zero;
    [SerializeField]Transform _obj = null;
    Vector3 target { get {
            if (_obj != null)
            {
                Vector3 pos = _obj.position;
                pos.z = transform.position.z;
                return pos;
            }
            else
                return _target;
        } }
    [SerializeField]bool isTargeting = false;
    [SerializeField] bool isTargetingLock = false;

    Vector3 velocity = Vector2.zero;

    // Start is called before the first frame update
    public void SetTarget(Vector2 pos)
    {
        _target = pos;
        _target.z = transform.position.z;
        isTargeting = true;
    }



    public void SetTargetingOff() {
        isTargeting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargeting) {
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
            if (transform.position == target && !isTargetingLock) {
                SetTargetingOff();
            }
        }
    }
}
