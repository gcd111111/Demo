/****************************************************
	文件：FubenSys.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/07 20:05   	
	功能：副本战斗业务系统
*****************************************************/
using Google.Protobuf.WellKnownTypes;
using PEProtocol;

public class FubenSys
{
    private static FubenSys instance = null;
    public static FubenSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FubenSys();
            }
            return instance;
        }
    }
    private CacheSvc cacheSvc = null;
    private CfgSvc cfgSvc = null;
    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc=CfgSvc.Instance;
        PECommon.Log("FubenSys Init Done.");
    }
    public void ReqFBFight(MsgPack pack)
    {
        ReqFBFight data = pack.msg.reqFBFight;

        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspFBFight
        };
        PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);
        int power = cfgSvc.GetMapCfg(data.fbid).power;

        if (pd.fuben < data.fbid)
        {
            msg.err = (int)ErrorCode.ClientDataError;
        }
        else if (pd.power < power)
        {
            msg.err = (int)ErrorCode.LackPower;
        }
        else
        {
            pd.power -= power;
            if (cacheSvc.UpdatePlayerData(pd.id, pd))
            {
                RspFBFight rspFBFigh = new RspFBFight
                {
                    fbid = data.fbid,
                    power = pd.power,
                };
                msg.rspFBFight = rspFBFigh;
            }
            else
            {
                msg.err = (int)ErrorCode.UpdateDBError;
            }
        }
        pack.session.SendMsg(msg);
    }
    public void ReqFBFightEnd(MsgPack pack)
    {
        ReqFBFightEnd data = pack.msg.reqFBFightEnd;

        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspFBFightEnd
        };

        //校验战斗是否合法
        if (data.win)
        {
            if (data.costtime > 0&&data.resthp>0)
            {
                //根据副本ID获取相应的奖励
                MapCfg rd = cfgSvc.GetMapCfg(data.fbid);
                PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);

                TaskSys.Instance.CalcTaskPrgs(pd,2);

                pd.coin += rd.coin;
                pd.crystal += rd.crystal;
                PECommon.CalcExp(pd,rd.exp);

                if (pd.fuben == data.fbid)
                {
                    pd.fuben += 1;
                }
                if (!cacheSvc.UpdatePlayerData(pd.id, pd))
                {
                    msg.err = (int)ErrorCode.UpdateDBError;
                }
                else
                {
                    RspFBFightEnd rspFBFight = new RspFBFightEnd
                    {
                        win = data.win,
                        fbid = data.fbid,
                        resthp = data.resthp,
                        costtime = data.costtime,

                        coin = pd.coin,
                        lv=pd.lv,
                        exp= pd.exp,
                        crystal= pd.crystal,
                        fuben=pd.fuben
                    };
                    msg.rspFBFightEnd = rspFBFight;
                }
            }
        }
        else
        {
            msg.err=(int)ErrorCode.ClientDataError;
        }
        pack.session.SendMsg(msg);
    }
}

