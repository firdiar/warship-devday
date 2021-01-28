using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RepairType { 
    MachineError /*ga bisa gerak*/,
    MachineMalfunction /* movement speed decreased */,
    AirBrakeError /* ga bisa berhenti */,
    OutOfAmmo /*ga bisa nembak*/,
    BrokenCannon /*ga bisa nembak dan muter*/,
    BrokenBody /* condition decreased constantly */,
    BrokenRadar /* Blind */,
} 

[RequireComponent(typeof(Rigidbody2D))]
public class MainShip : MonoBehaviour
{

    [Header("Status")]
    [SerializeField]float _condition = 100;
    float condition { get { return _condition; } set { _condition = Mathf.Clamp(value, 0, 100); } } 
    [SerializeField] List<RepairType> malfunction = new List<RepairType>();

    [Header("Controll")]
    [SerializeField] bool playerShip = false;
    [SerializeField] bool autoMove = true;
    [SerializeField] bool autoShoot = true;
    [SerializeField] bool autoRepair = true;
    [SerializeField] bool autoSecurity = true;
    

    [Header("Movement")]
    [SerializeField]Vector2 directionMove = Vector2.zero;
    [SerializeField][Range(0 , 1)]float speedMove = 0;
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float rotationSpeed = 2;
    [SerializeField] float speedBonus = 1;

    [Header("Shooting")]
    [SerializeField] Vector2 targetShoot = Vector2.zero;
    //[SerializeField] Vector2 actualDirectionShoot = Vector2.zero;
    [SerializeField] float cooldownCannonShoot = 5;
    [SerializeField] float shootBonus = 1;
    float cooldownShoot = 0;
    [SerializeField] Transform[] cannon = null;
    




    Rigidbody2D body = null;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if (autoRepair)
            InvokeRepeating("RandomError", 10, 8);
    }


    private void Update()
    {
        //Movement
        if (directionMove != Vector2.zero)
            RotateBody(this.directionMove, rotationSpeed * (body.velocity.magnitude));

        if (speedMove != 0)
            body.AddForce(transform.up * speedMove * Time.deltaTime * speedBonus);


        //Shooting
        if (targetShoot != Vector2.zero && cooldownShoot <= 0)
            RotateCanon(this.targetShoot, 2);
        else if (cooldownShoot > 0)
            cooldownShoot -= Time.deltaTime;

    }

    #region Movement
    // Update is called once per frame
    public void SetSpeedMove(float value)
    {
        Debug.Log("Setsped");
        speedMove = Mathf.Clamp(value, -1, 1)* maxSpeed;
    }
    public void SetDirectionMove(Vector2 direction)
    {
        Debug.Log("SetMove");
        this.directionMove = direction.normalized;
    }



    public void RotateBody(Vector2 direction, float speed = 6f)
    {
        Quaternion fromRot = transform.rotation;

        Quaternion targetRot = Quaternion.FromToRotation(Vector2.up, direction);

        //transform.eulerAngles = Quaternion.RotateTowards(fromRot, targetRot, speed).eulerAngles;
        transform.eulerAngles = new Vector3(0, 0, Quaternion.Slerp(fromRot, targetRot, (speed / 2f) * Time.deltaTime).eulerAngles.z);
    }

    #endregion

    #region Shooting
    protected virtual void Shoot() {
        cooldownShoot = cooldownCannonShoot;
        targetShoot = Vector2.zero;
        //spawn projectile here

        //some effect here

    }

    public void SetCannonDirection(Vector2 targetPos)
    {
        targetShoot = targetPos;
    }

    protected virtual void RotateCanon(Vector2 target, float speed = 2.5f)
    {
        foreach (Transform t in cannon)
        {
            Vector2 direction = target - (Vector2)t.position;
            Quaternion fromRot = t.rotation;

            Quaternion targetRot = Quaternion.FromToRotation(Vector2.up, direction);

            t.transform.eulerAngles = new Vector3(0, 0, Quaternion.Slerp(fromRot, targetRot, (speed / 2f) * Time.deltaTime).eulerAngles.z);
        }
    }

    #endregion

    #region Repair
    void RandomError() { 
    
    }

    #endregion

    #region Security
    #endregion

}
