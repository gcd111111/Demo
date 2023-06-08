/****************************************************
	文件：StateMove.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/13 14:01   	 	
	功能：移动状态
*****************************************************/

public class StateMove : IState {
    public void Enter(EntityBase entity, params object[] args) {
        entity.currentAniState = AniState.Move;
        //PECommon.Log("Enter StateMove.");
    }

    public void Exit(EntityBase entity, params object[] args) {
        //PECommon.Log("Exit StateMove.");
    }

    public void Process(EntityBase entity, params object[] args) {
        entity.SetBlend(Constants.BlendMove);
        //PECommon.Log("Process StateMove.");
    }
}
