﻿//
// Automatically generated by JSComponentGenerator.
//
using UnityEngine;

public class JSComponent_Enable_Visible_AnimatorIK_Move_JointBreak : JSComponent
{
    int idOnDisable;
    int idOnEnable;
    int idOnBecameInvisible;
    int idOnBecameVisible;
    int idOnAnimatorIK;
    int idOnAnimatorMove;
    int idOnJointBreak;

    protected override void initMemberFunction()
    {
        base.initMemberFunction();
        idOnDisable = JSApi.getObjFunction(jsObjID, "OnDisable");
        idOnEnable = JSApi.getObjFunction(jsObjID, "OnEnable");
        idOnBecameInvisible = JSApi.getObjFunction(jsObjID, "OnBecameInvisible");
        idOnBecameVisible = JSApi.getObjFunction(jsObjID, "OnBecameVisible");
        idOnAnimatorIK = JSApi.getObjFunction(jsObjID, "OnAnimatorIK");
        idOnAnimatorMove = JSApi.getObjFunction(jsObjID, "OnAnimatorMove");
        idOnJointBreak = JSApi.getObjFunction(jsObjID, "OnJointBreak");
    }

    void OnDisable()
    {
        callIfExist(idOnDisable);
    }
    void OnEnable()
    {
        callIfExist(idOnEnable);
    }
    void OnBecameInvisible()
    {
        callIfExist(idOnBecameInvisible);
    }
    void OnBecameVisible()
    {
        callIfExist(idOnBecameVisible);
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

}