/****************************************************
    文件：TriggerData.cs
	作者：gcd
    邮箱: 2042484168@qq.com
    日期：2023/5/14 11:43:23
	功能：地图触发数据
*****************************************************/

using UnityEngine;

public class TriggerData : MonoBehaviour 
{
    public int triggerWave;
    public MapMgr mapMgr;
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (mapMgr != null)
            {
                mapMgr.TriggerMonsterBorn(this,triggerWave);
            }
        }
    }
}