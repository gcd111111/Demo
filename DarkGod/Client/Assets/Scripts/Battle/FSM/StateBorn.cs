/****************************************************
	文件：StateBorn.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/13 14:01   		
	功能：出生状态
*****************************************************/

public class StateBorn : IState {
    public void Enter(EntityBase entity, params object[] args) {
        entity.currentAniState = AniState.Born;
    }

    public void Exit(EntityBase entity, params object[] args) {
    }

    public void Process(EntityBase entity, params object[] args) {
        //播放出生动画
        entity.SetAction(Constants.ActionBorn);
        TimerSvc.Instance.AddTimeTask((int tid) => {
            entity.SetAction(Constants.ActionDefault);
        }, 500);
    }
}