using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DimpleController : MonoBehaviour
{
    public HandPhysics rightHand;
    private Collider[] rightColliders;
    List<Collider> rightColliderList = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        //SetRigidBodyState(true);
        //SetColliderState(false);

        GetComponent<Collider>().enabled = true;       
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        foreach (Collider rightCollider in rightColliders)
        {
            if (col == rightCollider)
            {
                GetPunched();
                Debug.Log(col.gameObject);
            }
        }      
    }

    public void GetPunched()
    {
        StartCoroutine(PunchAnim());
        //SetRigidBodyState(false);
        //SetColliderState(true);
        // Debug.Log(col.gameObject);                        
    }
    private IEnumerator PunchAnim()
    {
        GetComponent<Animator>().SetBool("isRightHit", true);
        yield return new WaitForSeconds(.1f);
        GetComponent<Animator>().SetBool("isRightHit", false);
    }
    void SetRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rigidBody in rigidbodies)
        {
            rigidBody.isKinematic = state;
        }

        //GetComponent<Rigidbody>().isKinematic = !state;
    }

    void SetColliderState(bool state)
    {
        Collider[] colliders= GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

       // GetComponent<Collider>().enabled = !state;
    }
}

