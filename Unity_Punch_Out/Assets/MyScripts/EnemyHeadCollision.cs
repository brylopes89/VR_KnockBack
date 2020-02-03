using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class EnemyHeadCollision : MonoBehaviour
{
    private Animator anim;
    private string hitDirection;

    private int jabHashID;
    private int rightHookHashID;
    private int leftHookHashID;

    private bool isLeftHook = false;
    private bool isRightHook = false;
    private bool isJab = false;

    public SteamVR_Behaviour_Pose[] hands;
    public SteamVR_Action_Boolean trackPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {               
        float collisionForce = collision.impulse.magnitude / Time.fixedDeltaTime;
        if (collision.gameObject.CompareTag("PlayerHand"))
        {
            hitDirection = DirectionImpact(collision.transform.position);

            if (trackPadAction.GetState(hands[0].inputSource))
            {
                isLeftHook = true;
                isRightHook = false;
                isJab = false;
            }
            else if(trackPadAction.GetState(hands[1].inputSource))
            {
                isLeftHook = false;
                isRightHook = true;
                isJab = false;
            }
            else
            {
                isLeftHook = false;
                isRightHook = false;
                isJab = true;
            }

            StartCoroutine(ImpactAnim(hitDirection));
        }
    }
        private IEnumerator ImpactAnim(string animBoolName)
    {           
        jabHashID = Animator.StringToHash(animBoolName);
        rightHookHashID = Animator.StringToHash("isRightHook");
        leftHookHashID = Animator.StringToHash("isLeftHook");

        if (isJab)
        {
            anim.SetBool(jabHashID, true);
            yield return new WaitForEndOfFrame();
            anim.SetBool(jabHashID, false);
        }
        else if(isRightHook)
        {
            anim.SetBool(rightHookHashID, true);
            yield return new WaitForEndOfFrame();
            anim.SetBool(rightHookHashID, false);
        }
        else if(isLeftHook)
        {
            anim.SetBool(leftHookHashID, true);
            yield return new WaitForEndOfFrame();
            anim.SetBool(leftHookHashID, false);
        }
    }
    string DirectionImpact(Vector3 OtherObject)
    {
        string front;
        string right;
        //string direction;
        float tolerance = 0.15f;

        if (Vector3.Dot(transform.forward, OtherObject - transform.position) > tolerance) 
            front = "isForward";
        else 
            front = "isForward";

        if (Vector3.Dot(transform.right, OtherObject - transform.position) < -tolerance) 
            right = "LeftJab";
        else if (Vector3.Dot(transform.right, OtherObject - transform.position) > tolerance) 
            right = "RightJab";
        else 
            right = "Jab";

        return front + right;
    }
}
