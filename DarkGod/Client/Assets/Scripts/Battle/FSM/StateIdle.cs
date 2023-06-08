/****************************************************
	文件：StateIdle.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/13 14:01   		
	功能：待机状态
*****************************************************/

using UnityEngine;

public class StateIdle : IState {
    public void Enter(EntityBase entity, params object[] args) {
        entity.currentAniState = AniState.Idle;
        entity.SetDir(Vector2.zero);
        entity.skEndCB = -1;
        //PECommon.Log("Enter StateIdle.");
    }

    public void Exit(EntityBase entity, params object[] args) {
        //PECommon.Log("Exit StateIdle.");
    }

    public void Process(EntityBase entity, params object[] args) {
        if (entity.nextSkillID != 0) {
            entity.Attack(entity.nextSkillID);
        }
        else {
            if (entity.entityType == EntityType.Player)
            {
                entity.canRisSkill = true;
            }
            if (entity.GetDirInput() != Vector2.zero) {
                entity.Move();
                entity.SetDir(entity.GetDirInput());
            }
            else {
                entity.SetBlend(Constants.BlendIdle);
            }
            //PECommon.Log("Process StateIdle.");
        }
    }
}
