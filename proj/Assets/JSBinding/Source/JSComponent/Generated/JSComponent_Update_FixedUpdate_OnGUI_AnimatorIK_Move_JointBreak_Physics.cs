﻿//
// Automatically generated by JSComponentGenerator.
//
using UnityEngine;

public class JSComponent_Update_FixedUpdate_OnGUI_AnimatorIK_Move_JointBreak_Physics : JSComponent
{
    int idUpdate;
    int idLateUpdate;
    int idFixedUpdate;
    int idOnGUI;
    int idOnAnimatorIK;
    int idOnAnimatorMove;
    int idOnJointBreak;
    int idOnParticleCollision;
    int idOnCollisionEnter;
    int idOnCollisionEnter2D;
    int idOnCollisionExit;
    int idOnCollisionExit2D;
    int idOnCollisionStay;
    int idOnCollisionStay2D;
    int idOnTriggerEnter;
    int idOnTriggerEnter2D;
    int idOnTriggerExit;
    int idOnTriggerExit2D;
    int idOnTriggerStay;
    int idOnTriggerStay2D;
    int idOnControllerColliderHit;

    protected override void initMemberFunction()
    {
        base.initMemberFunction();
        idUpdate = JSApi.getObjFunction(jsObjID, "Update");
        idLateUpdate = JSApi.getObjFunction(jsObjID, "LateUpdate");
        idFixedUpdate = JSApi.getObjFunction(jsObjID, "FixedUpdate");
        idOnGUI = JSApi.getObjFunction(jsObjID, "OnGUI");
        idOnAnimatorIK = JSApi.getObjFunction(jsObjID, "OnAnimatorIK");
        idOnAnimatorMove = JSApi.getObjFunction(jsObjID, "OnAnimatorMove");
        idOnJointBreak = JSApi.getObjFunction(jsObjID, "OnJointBreak");
        idOnParticleCollision = JSApi.getObjFunction(jsObjID, "OnParticleCollision");
        idOnCollisionEnter = JSApi.getObjFunction(jsObjID, "OnCollisionEnter");
        idOnCollisionEnter2D = JSApi.getObjFunction(jsObjID, "OnCollisionEnter2D");
        idOnCollisionExit = JSApi.getObjFunction(jsObjID, "OnCollisionExit");
        idOnCollisionExit2D = JSApi.getObjFunction(jsObjID, "OnCollisionExit2D");
        idOnCollisionStay = JSApi.getObjFunction(jsObjID, "OnCollisionStay");
        idOnCollisionStay2D = JSApi.getObjFunction(jsObjID, "OnCollisionStay2D");
        idOnTriggerEnter = JSApi.getObjFunction(jsObjID, "OnTriggerEnter");
        idOnTriggerEnter2D = JSApi.getObjFunction(jsObjID, "OnTriggerEnter2D");
        idOnTriggerExit = JSApi.getObjFunction(jsObjID, "OnTriggerExit");
        idOnTriggerExit2D = JSApi.getObjFunction(jsObjID, "OnTriggerExit2D");
        idOnTriggerStay = JSApi.getObjFunction(jsObjID, "OnTriggerStay");
        idOnTriggerStay2D = JSApi.getObjFunction(jsObjID, "OnTriggerStay2D");
        idOnControllerColliderHit = JSApi.getObjFunction(jsObjID, "OnControllerColliderHit");
    }

    void Update()
    {
        callIfExist(idUpdate);
    }
    void LateUpdate()
    {
        callIfExist(idLateUpdate);
    }
    void FixedUpdate()
    {
        callIfExist(idFixedUpdate);
    }
    void OnGUI()
    {
        callIfExist(idOnGUI);
    }
    void OnAnimatorIK(int layerIndex)
    {
        callIfExist(idOnAnimatorIK, layerIndex);
    }
    void OnAnimatorMove()
    {
        callIfExist(idOnAnimatorMove);
    }
    void OnJointBreak(float breakForce)
    {
        callIfExist(idOnJointBreak, breakForce);
    }
    void OnParticleCollision(GameObject other)
    {
        callIfExist(idOnParticleCollision, other);
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        callIfExist(idOnCollisionEnter, collisionInfo);
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        callIfExist(idOnCollisionEnter2D, coll);
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        callIfExist(idOnCollisionExit, collisionInfo);
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        callIfExist(idOnCollisionExit2D, coll);
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        callIfExist(idOnCollisionStay, collisionInfo);
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        callIfExist(idOnCollisionStay2D, coll);
    }
    void OnTriggerEnter(Collider other)
    {
        callIfExist(idOnTriggerEnter, other);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        callIfExist(idOnTriggerEnter2D, other);
    }
    void OnTriggerExit(Collider other)
    {
        callIfExist(idOnTriggerExit, other);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        callIfExist(idOnTriggerExit2D, other);
    }
    void OnTriggerStay(Collider other)
    {
        callIfExist(idOnTriggerStay, other);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        callIfExist(idOnTriggerStay2D, other);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        callIfExist(idOnControllerColliderHit, hit);
    }

}