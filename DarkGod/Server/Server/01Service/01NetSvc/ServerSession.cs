﻿/****************************************************
	文件：ServerSession.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/02 12:43   
	功能：网络会话连接
*****************************************************/

using PENet;
using PEProtocol;

public class ServerSession : PESession<GameMsg> {
    public int sessionID = 0;

    protected override void OnConnected() {
        sessionID = ServerRoot.Instance.GetSessionID();
        PECommon.Log("SessionID:" + sessionID + " Client Connect");
    }

    protected override void OnReciveMsg(GameMsg msg) {
        PECommon.Log("SessionID: " + sessionID + "   RcvPack CMD:" + ((CMD)msg.cmd).ToString());
        NetSvc.Instance.AddMsgQue(this, msg);
    }

    protected override void OnDisConnected() {
        LoginSys.Instance.ClearOfflineData(this);
        PECommon.Log("SessionID:" + sessionID + " Client Offline");
    }
}