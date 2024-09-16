using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this shits a mess, look if you dare!

public class WheelAndSpringUnit : MonoBehaviour
{
    public Rigidbody body; 

    public LayerMask layerMask;

    public float wheelForce = 1;//needs better terminology

    public WheelAnimator wheelAnimator;

    public bool driven = true; 

    [Header("Wheel-Grip Settings")]//needs better terminology
    //public float maxSlideFrictionSpeed = 1;//the speed at which the wheel will exert maximum slide friction
    //public float maxSlideFriction = 1;
    [Tooltip("wheel grip as a function of how sideways the wheel is sliding")]
    public AnimationCurve wheelGripCurve;
    [Tooltip("readonly")]
    public float wheelGrip;
    public float wheelGripMultiplier = 1; 


    [Header("Stearing Settings")]
    [Range(-1, 1)]
    public float stearing = 0;
    public float maxSearAngle = 0; 

    [Header("Displacement Settings")]
    public float desiredDisplacement = 1;
    public float k = 1;//spring strength
    public float damping = 1;

    [Header("Other Settings")]
    public bool selfUpdate = false;//if on can lead to violent jittering

    [Header("Debug Settings")]
    public bool drawDebugRays = false;

    

    private Vector3 totalForce;
    private Vector3 wheelForward;
    private Vector3 wheelRight;
    public float displacementForce;
    private float displacement;
    private RaycastHit displacementHit;

    public void SetWheelBehaviour(Vector2 input) 
    {
        wheelForce = input.y;
        stearing = input.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateWheelRotation();//updates the wheelForward and wheelRight variables

        if(selfUpdate == true) 
        {
            UpdateTotalForce();
            ExertTotalForce(); 
        }

        if (wheelAnimator != null)
        {
            wheelAnimator.transform.rotation = Quaternion.LookRotation(wheelForward, this.transform.up);
            wheelAnimator.transform.localPosition = new Vector3(0, -displacement, 0);
        }
    }

    private float GetDisplacementForce(float desiredDisplacement, float currentDisplacement)
    {

        float displacement = (desiredDisplacement - currentDisplacement);
        /*
        if(displacement < 0) 
        {
            Debug.Log("ahhhhhhhhhh!!!");
        }
        */

        float springForce = -k * displacement;

        Vector3 wheelPointVelocity = body.GetPointVelocity(displacementHit.point);//the velocity of the body at the wheels location. 
        float vertialWheelVelocity = Vector3.Dot(this.transform.up, wheelPointVelocity);

        //float dampingForce = damping * body.velocity.y;
        float dampingForce = damping * vertialWheelVelocity;
        return -(springForce + dampingForce);
    }

    private void UpdateWheelRotation() 
    {
        Quaternion wheelAngle = Quaternion.AngleAxis(stearing * maxSearAngle, this.transform.up);
        wheelForward = wheelAngle * this.transform.forward;
        wheelRight = Vector3.Cross(wheelForward, this.transform.up);
    }

    public void UpdateTotalForce() 
    {
        totalForce = Vector3.zero;//rest totalForce

        //defalt values (for when the wheel is not on the ground)
        wheelGrip = 0;
        float steeringVelocity = 0;
        // -- //
        displacementForce = 0;
        displacement = desiredDisplacement;

        //ifHits: overwrites defalt values because the wheel is on the ground
        if (Physics.Raycast(this.transform.position, (-this.transform.up), out displacementHit, desiredDisplacement, layerMask))
        {
            displacement = displacementHit.distance;
            displacementForce = Mathf.Max(0, GetDisplacementForce(desiredDisplacement, displacementHit.distance));
            // -- //
            Vector3 wheelPointVelocity = body.GetPointVelocity(displacementHit.point);//the velocity of the body at the wheels location. 
            //Vector3 wheelPointVelocity = body.GetPointVelocity(this.transform.position);//the velocity of the body at the wheels location. 
            steeringVelocity = Vector3.Dot(wheelRight, wheelPointVelocity);
            if((steeringVelocity != 0) && (wheelPointVelocity.magnitude != 0))
            {
                float wheelFactorEvaluationValue = Mathf.Clamp01(Mathf.Abs(steeringVelocity) / wheelPointVelocity.magnitude);
                float wheelGripFactor = wheelGripCurve.Evaluate(wheelFactorEvaluationValue);
                float desiredVelociyChange = -steeringVelocity * wheelGripFactor;
                float desiredAcceleration = desiredVelociyChange / Time.deltaTime;
                //float rawAcceleration = (-steeringVelocity / Time.deltaTime);
                wheelGrip = desiredAcceleration * wheelGripMultiplier;
            }
            else 
            {
                wheelGrip = 0;
            }

        }

        if (displacementHit.point != null)
        {
            /*
            if(drawDebugRays == true) 
            {
                if (displacementForce < 0)
                {
                    Debug.Log("ZIPO!");
                }
                Debug.Log("displacementForce:" + displacementForce);
            }
            */
            

            totalForce += (this.transform.up) * displacementForce * Time.deltaTime;//force of the suspension

            if(driven == true)
            {
                totalForce += wheelForward * (wheelForce * Time.deltaTime);//force of wheel spinning forward or back
            }
           

            //totalForce += wheelRight * (wheelGrip * (-sidewaysVelociy / Time.deltaTime));//force of the wheel slideing left or right
            totalForce += wheelRight * wheelGrip;
        }

        if (drawDebugRays == true)
        {
            Debug.DrawRay(displacementHit.point, wheelForward);
            Debug.DrawRay(displacementHit.point, wheelRight * steeringVelocity, Color.red);
            Debug.DrawRay(displacementHit.point, (this.transform.up) * displacementForce * Time.deltaTime, Color.green);
        }
    }

    public void ExertTotalForce() 
    {
        if(displacement < desiredDisplacement) 
        {
            body.AddForceAtPosition(totalForce, displacementHit.point);
        }
    }
}
