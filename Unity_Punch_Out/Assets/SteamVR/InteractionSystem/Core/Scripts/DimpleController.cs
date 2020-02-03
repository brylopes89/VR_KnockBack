using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DimpleController : MonoBehaviour
{
    public HandPhysics rightHand;
    public HandPhysics leftHand;    
    
    private int animHashID;
    private Animator anim;
    private string hitDirection;
    // Start is called before the first frame update
    void Start()
    {    
        anim = GetComponent<Animator>();       
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider col)
    {          
        if (col.gameObject.CompareTag("PlayerHand"))
        {
            Debug.Log("Hit By" + col.gameObject);
            hitDirection = DirectionImpact(col.transform.position);
            StartCoroutine(ImpactAnim(hitDirection));
        }
       
    }
    private IEnumerator ImpactAnim(string boolName)
    {        
        animHashID = Animator.StringToHash(boolName);
        //anim.SetTrigger(animHashID);
        anim.SetBool(animHashID, true);
        yield return new WaitForEndOfFrame();
        anim.SetBool(boolName, false);      
    }
 
    string DirectionImpact(Vector3 OtherObject)
    {
        string front;
        string right;
        //string direction;
        float tolerance = 0.1f;

        if (Vector3.Dot(transform.forward, OtherObject - transform.position) >= tolerance)
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

