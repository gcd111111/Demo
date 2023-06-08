/****************************************************
	文件：IState.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/13 14:01   		
	功能：状态接口
*****************************************************/

public interface IState {
    void Enter(EntityBase entity, params object[] args);//可变参数

    void Process(EntityBase entity, params object[] args);

    void Exit(EntityBase entity, params object[] args);
}

public enum AniState {
    None,
    Born,
    Idle,
    Move,
    Attack,
    Hit,
    Die
}
