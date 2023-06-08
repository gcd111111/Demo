/****************************************************
	文件：TimerSvc.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/13 14:01   	
	功能：计时服务
*****************************************************/

using System;

public class TimerSvc : SystemRoot {
    public static TimerSvc Instance = null;

    private PETimer pt;

    public void InitSvc() {
        Instance = this;
        pt = new PETimer();

        //设置日志输出
        pt.SetLog((string info) => {
            PECommon.Log(info);
        });

        PECommon.Log("Init TimerSvc...");
    }

    public void Update() {
        pt.Update();
    }

    public int AddTimeTask(Action<int> callback, double delay, PETimeUnit timeUnit = PETimeUnit.Millisecond, int count = 1) {
        return pt.AddTimeTask(callback, delay, timeUnit, count);
    }

    public double GetNowTime() {
        return pt.GetMillisecondsTime();
    }
    public void DelTask(int tid)
    { 
        pt.DeleteTimeTask(tid);
    }
}