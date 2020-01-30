using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class DimpleController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            SetRigidBodyState(true);
            SetColliderState(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision col) //public void GetPunched()        
        {
            if(col.gameObject.tag != "Floor")
            {
                GetComponent<Animator>().enabled = false;
                SetRigidBodyState(false);
                SetColliderState(true);
                Debug.Log(col.gameObject);
            }            
        }

        void SetRigidBodyState(bool state)
        {
            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

            foreach(Rigidbody rigidBody in rigidbodies)
            {
                rigidBody.isKinematic = state;
            }

            GetComponent<Rigidbody>().isKinematic = !state;
        }

        void SetColliderState(bool state)
        {
            Collider[] colliders= GetComponentsInChildren<Collider>();

            foreach (Collider collider in colliders)
            {
                collider.enabled = state;
            }

            GetComponent<Collider>().enabled = !state;
        }
    }
}
