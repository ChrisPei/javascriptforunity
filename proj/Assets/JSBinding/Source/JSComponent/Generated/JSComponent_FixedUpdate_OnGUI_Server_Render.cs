﻿//
// Automatically generated by JSComponentGenerator.
//
using UnityEngine;

public class JSComponent_FixedUpdate_OnGUI_Server_Render : JSComponent
{
    int idFixedUpdate;
    int idOnGUI;
    int idOnConnectedToServer;
    int idOnDisconnectedFromServer;
    int idOnFailedToConnect;
    int idOnFailedToConnectToMasterServer;
    int idOnMasterServerEvent;
    int idOnNetworkInstantiate;
    int idOnPlayerConnected;
    int idOnPlayerDisconnected;
    int idOnSerializeNetworkView;
    int idOnServerInitialized;
    int idOnPostRender;
    int idOnPreCull;
    int idOnPreRender;
    int idOnRenderImage;
    int idOnRenderObject;
    int idOnWillRenderObject;

    protected override void initMemberFunction()
    {
        base.initMemberFunction();
        idFixedUpdate = JSApi.getObjFunction(jsObjID, "FixedUpdate");
        idOnGUI = JSApi.getObjFunction(jsObjID, "OnGUI");
        idOnConnectedToServer = JSApi.getObjFunction(jsObjID, "OnConnectedToServer");
        idOnDisconnectedFromServer = JSApi.getObjFunction(jsObjID, "OnDisconnectedFromServer");
        idOnFailedToConnect = JSApi.getObjFunction(jsObjID, "OnFailedToConnect");
        idOnFailedToConnectToMasterServer = JSApi.getObjFunction(jsObjID, "OnFailedToConnectToMasterServer");
        idOnMasterServerEvent = JSApi.getObjFunction(jsObjID, "OnMasterServerEvent");
        idOnNetworkInstantiate = JSApi.getObjFunction(jsObjID, "OnNetworkInstantiate");
        idOnPlayerConnected = JSApi.getObjFunction(jsObjID, "OnPlayerConnected");
        idOnPlayerDisconnected = JSApi.getObjFunction(jsObjID, "OnPlayerDisconnected");
        idOnSerializeNetworkView = JSApi.getObjFunction(jsObjID, "OnSerializeNetworkView");
        idOnServerInitialized = JSApi.getObjFunction(jsObjID, "OnServerInitialized");
        idOnPostRender = JSApi.getObjFunction(jsObjID, "OnPostRender");
        idOnPreCull = JSApi.getObjFunction(jsObjID, "OnPreCull");
        idOnPreRender = JSApi.getObjFunction(jsObjID, "OnPreRender");
        idOnRenderImage = JSApi.getObjFunction(jsObjID, "OnRenderImage");
        idOnRenderObject = JSApi.getObjFunction(jsObjID, "OnRenderObject");
        idOnWillRenderObject = JSApi.getObjFunction(jsObjID, "OnWillRenderObject");
    }

    void FixedUpdate()
    {
        callIfExist(idFixedUpdate);
    }
    void OnGUI()
    {
        callIfExist(idOnGUI);
    }
    void OnConnectedToServer()
    {
        callIfExist(idOnConnectedToServer);
    }
    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        callIfExist(idOnDisconnectedFromServer, info);
    }
    void OnFailedToConnect(NetworkConnectionError error)
    {
        callIfExist(idOnFailedToConnect, error);
    }
    void OnFailedToConnectToMasterServer(NetworkConnectionError info)
    {
        callIfExist(idOnFailedToConnectToMasterServer, info);
    }
    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        callIfExist(idOnMasterServerEvent, msEvent);
    }
    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        callIfExist(idOnNetworkInstantiate, info);
    }
    void OnPlayerConnected(NetworkPlayer player)
    {
        callIfExist(idOnPlayerConnected, player);
    }
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        callIfExist(idOnPlayerDisconnected, player);
    }
    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        callIfExist(idOnSerializeNetworkView, stream, info);
    }
    void OnServerInitialized()
    {
        callIfExist(idOnServerInitialized);
    }
    void OnPostRender()
    {
        callIfExist(idOnPostRender);
    }
    void OnPreCull()
    {
        callIfExist(idOnPreCull);
    }
    void OnPreRender()
    {
        callIfExist(idOnPreRender);
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        callIfExist(idOnRenderImage, src, dest);
    }
    void OnRenderObject()
    {
        callIfExist(idOnRenderObject);
    }
    void OnWillRenderObject()
    {
        callIfExist(idOnWillRenderObject);
    }

}