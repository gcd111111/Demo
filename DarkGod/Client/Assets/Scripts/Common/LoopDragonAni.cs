/****************************************************
    文件：LoopDragonAni.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/13 14:01   	
	功能：飞龙循环动画
*****************************************************/

using UnityEngine;

public class LoopDragonAni : MonoBehaviour {
    private Animation ani;

    private void Awake() {
        ani = transform.GetComponent<Animation>();
    }

    private void Start() {
        if (ani != null) {
            InvokeRepeating("PlayDragonAni", 0, 20);
        }
    }

    private void PlayDragonAni() {
        if (ani != null) {
            ani.Play();
        }
    }
}