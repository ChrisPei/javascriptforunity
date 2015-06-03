using System;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

using jsval = JSApi.jsval;

public class JSDataExchangeMgr
{
    const int VALUE_LEN = -1;

    JSVCall vc;
    public JSDataExchangeMgr(JSVCall vc)
    {
        this.vc = vc;
    }

    public enum eGetType
    {
        GetARGV,
        GetARGVRefOut,
        GetJSFUNRET,
        Jsval,
    }

    System.Object mTempObj;
    public void setTemp(System.Object obj)
    {
        mTempObj = obj;
    }


    #region Get Operation

//     public void getJSValueOfParam(ref jsval val, int pIndex)
//     {
//         IntPtr jsObj = JSApi.JSh_ArgvObject(JSMgr.cx, vc.vp, pIndex);
//         if (jsObj != IntPtr.Zero) 
//         {
//             JSApi.GetProperty(JSMgr.cx, jsObj, "Value", VALUE_LEN, ref val);
//         }
//         else
//         {
//             Debug.LogError("ref/out param must be js object");
//         }
//     }

//     public jsval getFunction(eGetType e)
//     {
//         jsval val = new jsval();
//         val.asBits = 0;
//         switch (e)
//         {
//             case eGetType.GetARGV:
//                 JSApi.JSh_ArgvFunctionValue(JSMgr.cx, vc.vp, vc.currIndex++, ref val);
//                 break;
//             case eGetType.GetARGVRefOut:
//                 {
//                     Debug.LogError("getFunction not support eGetType.GetARGVRefOut");
//                 }
//                 break;
//             case eGetType.GetJSFUNRET:
//                 {
//                     Debug.LogError("getFunction not support eGetType.GetJSFUNRET");
//                 }
//                 break;
//             case eGetType.Jsval:
//                 {
//                     // 通过 vc.valTemp 传递值
//                     // !!! return JSApi.jsh_getjsv(ref vc.valTemp);
//                 }
//                 break;
//             default:
//                 Debug.LogError("Not Supported");
//                 break;
//         }
//         return val;
//     }
    public object getObject(int e)
    {
        int jsObjID = JSApi.getObject(e);
        if (jsObjID == 0)
        {
            // no error
            return null;
        }

        // TODO CSRepresentedObject2 怎么办
        object csObj = JSMgr.getCSObj(jsObjID);
        if (csObj == null)
        {
            csObj = new CSRepresentedObject(jsObjID);
        }
        return csObj;

//         switch (e)
//         {
//             case JSApi.GetType.Arg:
//                 {
//                     IntPtr jsObj = JSApi.JSh_ArgvObject(JSMgr.cx, vc.vp, vc.currIndex++);
//                     if (jsObj == IntPtr.Zero)
//                         return null;
// 
//                     jsval val = new jsval();
//                     JSApi.JSh_GetUCProperty(JSMgr.cx, jsObj, "__nativeObj", -1, ref val);
//                     IntPtr __nativeObj = JSApi.JSh_GetJsvalObject(ref val);
//                     if (__nativeObj != IntPtr.Zero)
//                     {
//                         object csObj = JSMgr.getCSObj(__nativeObj);
//                         return csObj;
//                     }
//                     else
//                     {
//                         return new CSRepresentedObject(jsObj);
//                     }
//                 }
//                 break;
//             case eGetType.GetARGVRefOut:
//                 {
//                     jsval val = new jsval();
//                     JSApi.JSh_SetJsvalUndefined(ref val);
//                     getJSValueOfParam(ref val, vc.currIndex++);
// 
//                     IntPtr jsObj = JSApi.JSh_GetJsvalObject(ref val);
//                     if (jsObj == IntPtr.Zero)
//                         return null;
// 
//                     JSApi.JSh_GetUCProperty(JSMgr.cx, jsObj, "__nativeObj", -1, ref val);
//                     IntPtr __nativeObj = JSApi.JSh_GetJsvalObject(ref val);
//                     if (__nativeObj != IntPtr.Zero)
//                     {
//                         object csObj = JSMgr.getCSObj(__nativeObj);
//                         return csObj;
//                     }
//                     else
//                     {
//                         return new CSRepresentedObject(jsObj);
//                     }
//                 }
//                 break;
//             case eGetType.Jsval:
//                 {
//                     // 通过 vc.valTemp 传递值
//                     jsval val = new jsval();
//                     JSApi.JSh_SetJsvalUndefined(ref val);
// 
//                     IntPtr jsObj = JSApi.JSh_GetJsvalObject(ref vc.valTemp);
//                     if (jsObj == IntPtr.Zero)
//                         return null;
// 
//                     JSApi.JSh_GetUCProperty(JSMgr.cx, jsObj, "__nativeObj", -1, ref val);
//                     IntPtr __nativeObj = JSApi.JSh_GetJsvalObject(ref val);
//                     if (__nativeObj != IntPtr.Zero)
//                     {
//                         object csObj = JSMgr.getCSObj(__nativeObj);
//                         return csObj;
//                     }
//                     else
//                     {
//                         return new CSRepresentedObject(jsObj);
//                     }
//                 }
//                 break;
//             default:
//                 Debug.LogError("Not Supported");
//                 break;
//         }
//         return null;
    }

     /*
     * for concrete type e.g. setInt32 setString
     * they know type 
     * 
     * but for T parameters   type is known until run-time
     * so this method need a 'Type' argument
     * it is passed through mTempObj
     */
    public object getByType(JSApi.GetType eType)
    {
        int e = (int)eType;
        Type type = (Type)mTempObj;
        if (type.IsByRef)
            type = type.GetElementType();

        if (type == typeof(string))
            return JSApi.getStringS(e);
        else if (type.IsEnum)
            return JSApi.getEnum(e);
        else if (type.IsPrimitive)
        {
            if (type == typeof(System.Boolean))
                return JSApi.getBooleanS(e);
            else if (type == typeof(System.Char))
                return JSApi.getChar(e);
            else if (type == typeof(System.Byte))
                return JSApi.getByte(e);
            else if (type == typeof(System.SByte))
                return JSApi.getSByte(e);
            else if (type == typeof(System.UInt16))
                return JSApi.getUInt16(e);
            else if (type == typeof(System.Int16))
                return JSApi.getInt16(e);
            else if (type == typeof(System.UInt32))
                return JSApi.getUInt32(e);
            else if (type == typeof(System.Int32))
                return JSApi.getInt32(e);
            else if (type == typeof(System.UInt64))
                return JSApi.getUInt64(e);
            else if (type == typeof(System.Int64))
                return JSApi.getInt64(e);
            else if (type == typeof(System.Single))
                return JSApi.getSingle(e);
            else if (type == typeof(System.Double))
                return JSApi.getDouble(e);
            else if (type == typeof(System.IntPtr))
                return JSApi.getIntPtr(e);
            else
                Debug.LogError("Unknown primitive type" + type.Name);
        }
        else if (type == typeof(Vector2))
        {
            return JSApi.getVector2S(e);
        }
        else if (type == typeof(Vector3))
        {
            return JSApi.getVector3S(e);
        }
        else
        {
            return JSApi.getObject(e);
        }
        return null;
    }
    public object getWhatever(int e)
    {
        var tag = JSApi.getTag(e);
        if (jsval.isNullOrUndefined(tag))
            return null;
        else if (jsval.isBoolean(tag))
            return JSApi.getBooleanS(e);
        else if (jsval.isInt32(tag))
            return JSApi.getInt32(e);
        else if (jsval.isDouble(tag))
            return JSApi.getSingle(e);
        else if (jsval.isString(tag))
            return JSApi.getStringS(e);
        else if (jsval.isObject(tag))
        {
            if (JSApi.isVector2S(e))
            {
                return JSApi.getVector2S(e);
            }
            else if (JSApi.isVector3S(e))
            {
                return JSApi.getVector3S(e);
            }
            else
            {
                return getObject(e);
            }
        }
        return null;
    }
    public object getDelegate(eGetType e)
    {
        switch (e)
        {
            case eGetType.GetARGV:
                {
                    // TODO 检查 index要++
                    int jsObjID = JSApi.getObject((int)JSApi.GetType.Arg);
                    if (jsObjID == 0)
                        return null;

                    object csObj = JSMgr.getCSObj(jsObjID);
                    return csObj;

//                     IntPtr jsObj = JSApi.JSh_ArgvObject(JSMgr.cx, vc.vp, vc.currIndex++);
//                     if (jsObj == IntPtr.Zero)
//                         return null;
// 
//                     object csObj = JSMgr.getCSObj(jsObj);
//                     return csObj;
                }
                break;
//             case eGetType.GetARGVRefOut:
//                 {
//                     jsval val = new jsval();
//                     JSApi.JSh_SetJsvalUndefined(ref val);
//                     getJSValueOfParam(ref val, vc.currIndex++);
// 
//                     IntPtr jsObj = JSApi.JSh_GetJsvalObject(ref val);
//                     if (jsObj == IntPtr.Zero)
//                         return null;
// 
//                     JSApi.JSh_GetUCProperty(JSMgr.cx, jsObj, "__nativeObj", -1, ref val);
//                     IntPtr __nativeObj = JSApi.JSh_GetJsvalObject(ref val);
//                     if (__nativeObj == IntPtr.Zero)
//                         return null;
// 
//                     object csObj = JSMgr.getCSObj(__nativeObj);
//                     return csObj;
//                 }
//                 break;
//             case eGetType.Jsval:
//                 {
//                     // 通过 vc.valTemp 传递值
//                     jsval val = new jsval();
//                     JSApi.JSh_SetJsvalUndefined(ref val);
// 
//                     IntPtr jsObj = JSApi.JSh_GetJsvalObject(ref vc.valTemp);
//                     if (jsObj == IntPtr.Zero)
//                         return null;
//
//                     object csObj = JSMgr.getCSObj(jsObj);
//                     return csObj;
//                 }
//                 break;
            default:
                Debug.LogError("Not Supported");
                break;
        }
        return null;
    }
    #endregion


    public enum eSetType
    {
        SetRval,
        UpdateARGVRefOut,
        Jsval,
        //GetJSFUNRET
    }

    #region Set Operation

    // for generic type
    // type is assigned during runtime
    public void setWhatever(int e, object obj)
    {
//         Type type = (Type)mTempObj;
//         if (type.IsByRef)
//             type = type.GetElementType();

        // ?? TODO use mTempObj or not?

        // TODO check
        if (obj == null)
        {
            JSApi.setUndefined(e);
            return;
        }
        Type type = obj.GetType();

        if (type == typeof(string))
            JSApi.setStringS(e, (string)obj);
        else if (type.IsEnum)
            JSApi.setEnum(e, (int)obj);
        else if (type.IsPrimitive)
        {
            if (type == typeof(System.Boolean))
                JSApi.setBooleanS(e, (bool)obj);
            else if (type == typeof(System.Char))
                JSApi.setChar(e, (char)obj);
            else if (type == typeof(System.Byte))
                JSApi.setByte(e, (Byte)obj);
            else if (type == typeof(System.SByte))
                JSApi.setSByte(e, (SByte)obj);
            else if (type == typeof(System.UInt16))
                JSApi.setUInt16(e, (UInt16)obj);
            else if (type == typeof(System.Int16))
                JSApi.setInt16(e, (Int16)obj);
            else if (type == typeof(System.UInt32))
                JSApi.setUInt32(e, (UInt32)obj);
            else if (type == typeof(System.Int32))
                JSApi.setInt32(e, (Int32)obj);
            else if (type == typeof(System.UInt64))
                JSApi.setUInt64(e, (UInt64)obj);
            else if (type == typeof(System.Int64))
                JSApi.setInt64(e, (Int64)obj);
            else if (type == typeof(System.Single))
                JSApi.setSingle(e, (Single)obj);
            else if (type == typeof(System.Double))
                JSApi.setDouble(e, (Double)obj);
            else
                Debug.LogError("Unknown primitive type");
        }
        else if (type == typeof(Vector3))
        {
            JSApi.setVector3S(e, (Vector3)obj);
        }
        else if (type == typeof(Vector2))
        {
            JSApi.setVector3S(e, (Vector2)obj);
        }
        else
        {
            setObject(e, obj);
        }
    }
    // 如果是 UpdateRefARGV，之前要设置 currIndex
    // setObject 情况是：有CS对象
    // 操作：
    // JS对象已存在：返回那个JS对象
    // JS对象不存在：新建一个 JS 对象，并与现有CS对象关联起来
    public int setObject(/* JSApi.SetType */int e, object csObj)
    {
        int jsObjID = 0;
        if (csObj != null)
        {
            Type csType = csObj.GetType();
            if (csType.IsClass)
            {
                jsObjID = JSMgr.getJSObj(csObj);
            }
            if (jsObjID == 0)
            {
                if (csObj is CSRepresentedObject)
                {
                    jsObjID = ((CSRepresentedObject)csObj).jsObjID;
                }
                else
                {
                    string typeName = JSNameMgr.GetJSTypeFullName(csType);
                    jsObjID = JSApi.createJSClassObject(typeName);
                    if (jsObjID != 0)
                        JSMgr.addJSCSRel(jsObjID, csObj);
                    else
                        Debug.LogError("Return a \"" + typeName + "\" to JS failed. Did you forget to export that class?");
                }
            }
        }

        JSApi.setObject(e, jsObjID);
        return jsObjID;
    }
    // TODO what?
//     public void setDelegate(eSetType e, object csObj)
//     {
//         switch (e)
//         {
//             case eSetType.Jsval:
//             case eSetType.SetRval:
//                 {
//                     JSApi.JSh_SetJsvalUndefined(ref vc.valReturn);
//                     if (csObj != null)
//                     {
//                         IntPtr jsObj = IntPtr.Zero;
//                         Type csType = csObj.GetType();
//                         if (csType.IsClass && (jsObj = JSMgr.getJSObj(csObj)) != IntPtr.Zero)
//                         {
//                             JSApi.JSh_SetJsvalObject(ref vc.valReturn, jsObj);
//                         }
//                         else
//                         {
//                             //
//                             // 返回给JS的对象：需要 prototype
//                             // 他包含的__nativeObj：需要 finalizer，需要 csObj 对应
//                             //
//                             string typeName = JSNameMgr.GetJSTypeFullName(csType);
//                             IntPtr jstypeObj = JSDataExchangeMgr.GetJSObjectByname(typeName);
//                             if (jstypeObj != IntPtr.Zero)
//                             {
//                                 jsObj = JSApi.JSh_NewObjectAsClass(JSMgr.cx, jstypeObj, "ctor", null /*JSMgr.mjsFinalizer*/);
// 
//                                 // __nativeObj
//                                 IntPtr __nativeObj = JSApi.JSh_NewMyClass(JSMgr.cx, JSMgr.mjsFinalizer);
//                                 JSMgr.addJSCSRelation(jsObj, __nativeObj, csObj);
// 
//                                 // jsObj.__nativeObj = __nativeObj
//                                 jsval val = new jsval();
//                                 JSApi.JSh_SetJsvalObject(ref val, __nativeObj);
//                                 JSApi.JSh_SetUCProperty(JSMgr.cx, jsObj, "__nativeObj", -1, ref val);
// 
//                                 JSApi.JSh_SetJsvalObject(ref vc.valReturn, jsObj);
//                             }
//                             else
//                             {
//                                 Debug.LogError("Return a \"" + typeName + "\" to JS failed. Did you forget to export that class?");
//                             }
//                         }
//                     }
// 
//                     if (e == eSetType.Jsval)
//                         vc.valTemp = vc.valReturn;
//                     else if (e == eSetType.SetRval)
//                         JSApi.JSh_SetRvalJSVAL(JSMgr.cx, vc.vp, ref vc.valReturn);
//                 }
//                 break;
//             case eSetType.UpdateARGVRefOut:
//                 {
//                     jsval val = new jsval(); val.asBits = 0;
//                     IntPtr argvJSObj = JSApi.JSh_ArgvObject(JSMgr.cx, vc.vp, vc.currIndex);
//                     if (argvJSObj != IntPtr.Zero)
//                     {
//                         bool success = false;
// 
//                         IntPtr jsObj = IntPtr.Zero;
//                         Type csType = csObj.GetType();
//                         if (csType.IsClass && (jsObj = JSMgr.getJSObj(csObj)) != IntPtr.Zero)
//                         {
//                             // 3)
//                             // argvObj.Value = jsObj
//                             //
//                             JSApi.JSh_SetJsvalObject(ref val, jsObj);
//                             JSApi.JSh_SetUCProperty(JSMgr.cx, argvJSObj, "Value", -1, ref val);
//                             success = true;
//                         }
//                         else
//                         {
//                             // csObj must not be null
//                             IntPtr jstypeObj = JSDataExchangeMgr.GetJSObjectByname(JSNameMgr.GetTypeFullName(csObj.GetType()));
//                             if (jstypeObj != IntPtr.Zero)
//                             {
//                                 // 1)
//                                 // jsObj: prototype  
//                                 // __nativeObj: csObj + finalizer
//                                 // 
//                                 jsObj = JSApi.JSh_NewObjectAsClass(JSMgr.cx, jstypeObj, "ctor", null /*JSMgr.mjsFinalizer*/);
//                                 // __nativeObj
//                                 IntPtr __nativeObj = JSApi.JSh_NewMyClass(JSMgr.cx, JSMgr.mjsFinalizer);
//                                 JSMgr.addJSCSRelation(jsObj, __nativeObj, csObj);
// 
//                                 //
//                                 // 2)
//                                 // jsObj.__nativeObj = __nativeObj
//                                 //
//                                 JSApi.JSh_SetJsvalObject(ref val, __nativeObj);
//                                 JSApi.JSh_SetUCProperty(JSMgr.cx, jsObj, "__nativeObj", -1, ref val);
// 
//                                 // 3)
//                                 // argvObj.Value = jsObj
//                                 //
//                                 JSApi.JSh_SetJsvalObject(ref val, jsObj);
//                                 JSApi.JSh_SetUCProperty(JSMgr.cx, argvJSObj, "Value", -1, ref val);
//                                 success = true;
//                             }
//                             else
//                             {
//                                 Debug.LogError("Return a \"" + JSNameMgr.GetTypeFullName(csObj.GetType()) + "\" to JS failed. Did you forget to export that class?");
//                             }
//                         }
// 
//                         if (!success)
//                         {
//                             JSApi.JSh_SetJsvalUndefined(ref val);
//                             JSApi.JSh_SetUCProperty(JSMgr.cx, argvJSObj, "Value", -1, ref val);
//                         }
//                     }
//                 }
//                 break;
//             default:
//                 Debug.LogError("Not Supported");
//                 break;
//         }
//     }

    #endregion

    
    // return true if don't generate default constructor
//    public static bool DontGenDefaultConstructor(Type type)
//    {
//        bool bDontGenDefaultConstructor =
//            // type.GetConstructors().Length == 0 && 
//            type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).Length > 0;
//        return bDontGenDefaultConstructor;
//    }

    static Dictionary<string, Type> typeCache = new Dictionary<string,Type>();
    public static Type GetTypeByName(string typeName, Type defaultType = null)
    {
        Type t = null;
        if (!typeCache.TryGetValue(typeName, out t))
        {
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                t = a.GetType(typeName);
                if (t != null)
                    break;
            }
            typeCache[typeName] = t; // perhaps null
            //if (t == null)
            //{
            //    Debug.LogError("GetType of \"" + typeName + "\" is null. Did you export that class to JavaScript?");
            //}
        }
        if (t == null)
        {
            return defaultType;// typeof(CSRepresentedObject);
        }
        return t;
    }


    /*
    // Runtime Only
    // type: class type
    // methodName: method name
    // TCount: generic parameter count
    // vc: JSVCall instance
    public static MethodInfo MakeGenericConstructor(Type type, int TCount, int paramCount, JSVCall vc)
    {
        // Get generic method by name and param count.
        ConstructorInfo conT = JSDataExchangeMgr.GetGenericConstructorInfo(type, TCount, paramCount);
        if (conT == null)
        {
            return null;
        }

        // get T types
        Type[] genericTypes = new Type[TCount];
        for (int i = 0; i < TCount; i++)
        {
            // Get generic types from js.
            System.Type t = JSDataExchangeMgr.GetTypeByName(vc.datax.getString(JSDataExchangeMgr.eGetType.GetARGV));
            genericTypes[i] = t;
            if (t == null)
            {
                return null;
            }
        }

        // Make generic method.
        MethodInfo method = methodT.MakeGenericMethod(genericTypes);
        return method;
    }
    // Runtime Only
    // called by MakeGenericConstructor
    // get generic Constructor by matching TCount,paramCount, if more than 1 match, return null.
    static ConstructorInfo GetGenericConstructorInfo(Type type, int TCount, int paramCount)
    {
        ConstructorInfo[] constructors = type.GetConstructors();
        if (constructors == null || constructors.Length == 0)
        {
            return null;
        }

        ConstructorInfo con = null;
        for (int i = 0; i < constructors.Length; i++)
        {
            if (constructors[i].IsGenericMethodDefinition &&
                constructors[i].GetGenericArguments().Length == TCount &&
                constructors[i].GetParameters().Length == paramCount)
            {
                if (con == null)
                    con = constructors[i];
                else
                {
                    Debug.LogError("More than 1 Generic Constructor found!!! " + GetTypeFullName(type) + "." + name);
                    return null;
                }
            }
        }
        if (con == null)
        {
            Debug.LogError("No generic constructor found! " + GetTypeFullName(type));
        }
        return con;
    }*/
    public static ConstructorInfo makeGenericConstructor(Type type, ConstructorID constructorID)
    {
        int tCount = type.GetGenericArguments().Length;
        Type[] genericTypes = new Type[tCount];
        for (int i = 0; i < tCount; i++)
        {
            string typeName = JSApi.getStringS((int)JSApi.GetType.Arg);
            System.Type t = JSDataExchangeMgr.GetTypeByName(typeName, typeof(CSRepresentedObject));
            genericTypes[i] = t;
            if (t == null)
            {
                return null;
            }
        }

        var exactType = type.MakeGenericType(genericTypes);
        var exactConstructor = GenericTypeCache.getConstructor(exactType, constructorID);
        return exactConstructor;
    }

    // Runtime Only
    // type: class type
    // methodName: method name
    // TCount: generic parameter count
    // vc: JSVCall instance
    // TODO
    // delete
    public static MethodInfo makeGenericMethod(Type type, MethodID methodID, int TCount)
    {
        MethodInfo methodT = GenericTypeCache.getMethod(type, methodID);
        if (methodT == null)
        {
            return null;
        }

        Type[] genericTypes = new Type[TCount];
        for (int i = 0; i < TCount; i++)
        {
            string typeName = JSApi.getStringS((int)JSApi.GetType.Arg);
            System.Type t = JSDataExchangeMgr.GetTypeByName(typeName, typeof(CSRepresentedObject));
            genericTypes[i] = t;
            if (t == null)
            {
                return null;
            }
        }

        MethodInfo method = methodT.MakeGenericMethod(genericTypes);
        return method;
    }
    //
    // 
    //
    public static Type[] RecursivelyGetGenericParameters(Type type, List<Type> lst = null)
    {
        if (lst == null)
            lst = new List<Type>();

        if (type.ContainsGenericParameters)
        {
            if (type.IsGenericParameter)
            {
                lst.Add(type);
            }
            else if (type.HasElementType)
            {
                RecursivelyGetGenericParameters(type.GetElementType(), lst);
            }
            else if (type.IsGenericType)
            {
                Type[] genericArguments = type.GetGenericArguments();
                for (int i = 0; i < genericArguments.Length; i++)
                {
                    RecursivelyGetGenericParameters(genericArguments[i], lst);
                }
            }
        }
        return lst.ToArray();
    }
//     public static string GetMetatypeKeyword(Type type, out bool isWhatever)
//     {
//         string str = GetMetatypeKeyword(type);
//         isWhatever = (str == "Whatever");
//         return str;
//     }
    public static string GetMetatypeKeyword(Type type)
    {
        string ret = string.Empty;
        if (type.IsArray)
        {
            Debug.LogError("Array should not call GetMetatypeKeyword()");
            return ret;
        }

        if (type == typeof(string))
            ret = "JSApi.getStringS";
        else if (type.IsEnum)
            ret = "JSApi.getEnum";
        else if (type.IsPrimitive)
        {
            if (type == typeof(System.Boolean))
                ret = "JSApi.getBooleanS";
            else if (type == typeof(System.Char))
                ret = "JSApi.getChar";
            else if (type == typeof(System.Byte))
                ret = "JSApi.getByte";
            else if (type == typeof(System.SByte))
                ret = "JSApi.getSByte";
            else if (type == typeof(System.UInt16))
                ret = "JSApi.getUInt16";
            else if (type == typeof(System.Int16))
                ret = "JSApi.getInt16";
            else if (type == typeof(System.UInt32))
                ret = "JSApi.getUInt32";
            else if (type == typeof(System.Int32))
                ret = "JSApi.getInt32";
            else if (type == typeof(System.UInt64))
                ret = "JSApi.getUInt64";
            else if (type == typeof(System.Int64))
                ret = "JSApi.getInt64";
            else if (type == typeof(System.Single))
                ret = "JSApi.getSingle";
            else if (type == typeof(System.Double))
                ret = "JSApi.getDouble";
            else if (type == typeof(System.IntPtr))
                ret = "JSApi.getIntPtr";
            else
                Debug.LogError("444 Unknown primitive type");
        }
        else if (type == typeof(System.Object) || type.IsGenericParameter)
            ret = "JSMgr.vCall.datax.getWhatever";
        else if (type == typeof(Vector3))
            ret = "JSApi.getVector3S";
        else if (type == typeof(Vector2))
            ret = "JSApi.getVector2S";
        else
            ret = "JSMgr.vCall.datax.getObject";

        return ret;
    }

    public delegate T DGetV<T>();
    public static T GetJSArg<T>(DGetV<T> del) { return del(); }
}

public class JSDataExchange_Arr
{
    public Type elementType = null;

    public string Get_GetParam(Type t)
    {
        elementType = t.GetElementType();
        if (elementType.IsArray) { 
            //...error
        }
        StringBuilder sb = new StringBuilder();
        string getVal = JSDataExchangeMgr.GetMetatypeKeyword(elementType);

        var arrayFullName = string.Empty;
        var elementFullName = string.Empty;
        if (elementType.IsGenericParameter)
        {
            arrayFullName = "object[]";
            elementFullName = "object";
        }
        else
        {
            arrayFullName = JSNameMgr.GetTypeFullName(t);
            elementFullName = JSNameMgr.GetTypeFullName(elementType);
        }
        sb.AppendFormat("JSDataExchangeMgr.GetJSArg<{0}>(() =>\n", arrayFullName)
            .Append("    [[\n")
        .AppendFormat("    int jsObjID = JSApi.getObject((int)JSApi.GetType.Arg);\n")
        .AppendFormat("    int length = JSApi.getArrayLength(jsObjID);\n")
        .AppendFormat("    var ret = new {0}[length];\n", elementFullName)
        .AppendFormat("    for (var i = 0; i < length; i++) [[\n")
        .AppendFormat("        JSApi.getElement(jsObjID, i);\n")
        .AppendFormat("        ret[i] = ({0}){1}((int)JSApi.GetType.SaveAndRemove);\n", elementFullName, getVal)
        .AppendFormat("    ]]\n")
        .AppendFormat("    return ret;\n")
        .AppendFormat("]])\n");

//         sb.AppendFormat("JSDataExchangeMgr.GetJSArg<{0}>(() => [[\n", arrayFullName)
//         .AppendFormat("    IntPtr jsObj = JSApi.JSh_ArgvObject(JSMgr.cx, vc.vp, vc.currIndex++);\n")
//         .AppendFormat("    int length = JSApi.JSh_GetArrayLength(JSMgr.cx, jsObj);\n")
//         .AppendFormat("    var ret = new {0}[length];\n", elementFullName)
//         .AppendFormat("    for (var i = 0; i < length; i++) [[\n")
//         .AppendFormat("        JSApi.JSh_GetElement(JSMgr.cx, jsObj, (uint)i, ref vc.valTemp);\n")
//         .AppendFormat("        ret[i] = ({0}){1}((int)JSApi.GetType.Jsval);\n", elementFullName, getVal)
//         .AppendFormat("    ]]\n")
//         .AppendFormat("    return ret;\n")
//         .AppendFormat("]])\n");

        sb.Replace("[[", "{");
        sb.Replace("]]", "}");

        return sb.ToString(); 
    }

    public string Get_GetJSReturn()
    {
        return "null";
    }
    public string Get_Return(string expVar)
    {
        if (elementType == null)
        {
            Debug.LogError("JSDataExchange_Arr elementType == null !!");
            return "";
        }

        StringBuilder sb = new StringBuilder();
        string getValMethod = JSDataExchangeMgr.GetMetatypeKeyword(elementType).Replace("get", "set");

        if (elementType.ContainsGenericParameters)
        {
            sb.AppendFormat("    var arrRet = (Array){0};\n", expVar)
            .AppendFormat("    for (int i = 0; i < arrRet.Length; i++)\n")
            .Append("    [[\n")
            .AppendFormat("        {0}((int)JSApi.SetType.SaveAndTempTrace, arrRet.GetValue(i));\n", getValMethod)
            .AppendFormat("        JSApi.moveSaveID2Arr(i);\n")
            .AppendFormat("    ]]\n")
            .AppendFormat("    JSApi.setArrayS((int)JSApi.SetType.Rval, arrRet.Length, true);");
        }
        else
        {
            sb.AppendFormat("    var arrRet = ({0}[]){1};\n", JSNameMgr.GetTypeFullName(elementType), expVar)
            .AppendFormat("    for (int i = 0; i < arrRet.Length; i++)\n")
            .Append("    [[\n")
            .AppendFormat("        {0}((int)JSApi.SetType.SaveAndTempTrace, arrRet[i]);\n", getValMethod)
            .AppendFormat("        JSApi.moveSaveID2Arr(i);\n")
            .AppendFormat("    ]]\n")
            .AppendFormat("    JSApi.setArrayS((int)JSApi.SetType.Rval, arrRet.Length, true);");
        }

        sb.Replace("[[", "{");
        sb.Replace("]]", "}");

        return sb.ToString();
    }
}

/*
 what's this?
 example:
 if we have a js object Message, like:
 var Message = 
 {
    id: 4,
    str: "hello, world"
 }
 
 when it comes to C# (e.g. store in a C# version List<>), there is no C# type matching it
 so we make a CSRepresentedObject2 to wrap it
 
 but in C# we can only get it, return it, no any other operation can be performed
 
 */
public class CSRepresentedObject
{
    public CSRepresentedObject(int jsObjID)
    {
        this.jsObjID = jsObjID;
    }
    public int jsObjID;
}