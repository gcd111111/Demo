/****************************************************
	文件：GuideSys.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/02 21:19   	
	功能：引导业务系统
*****************************************************/

using PEProtocol;

public class GuideSys
{
    private static GuideSys instance = null;
    public static GuideSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GuideSys();
            }
            return instance;
        }
    }
    private CacheSvc cacheSvc = null;
    private CfgSvc cfgSvc = null;

    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        PECommon.Log("GuideSys Init Done.");
    }

    public void ReqGuide(MsgPack pack)
    {
        ReqGuide data = pack.msg.reqGuide;

        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspGuide
        };

        PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);
        GuideCfg gc = cfgSvc.GetGuideCfg(data.guideId);

        //更新引导ID
        if (pd.guideId == data.guideId)
        {
            //检测是否为智者点拨任务
            if (pd.guideId == 1001)
            {
                TaskSys.Instance.CalcTaskPrgs(pd,1);
            }
            pd.guideId += 1;

            //更新玩家数据
            pd.coin += gc.coin;
            PECommon.CalcExp(pd, gc.exp);

            if (!cacheSvc.UpdatePlayerData(pd.id, pd))
            {
                msg.err = (int)ErrorCode.UpdateDBError;
            }
            else
            {
                msg.rspGuide = new RspGuide
                {
                    guideId = pd.guideId,
                    coin = pd.coin,
                    lv = pd.lv,
                    exp = pd.exp
                };
            }
        }
        else
        {
            msg.err = (int)ErrorCode.ServerDataError;
        }
        pack.session.SendMsg(msg);
    }

    
}