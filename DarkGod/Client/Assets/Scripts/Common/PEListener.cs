/****************************************************
    文件：PEListener.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/13 14:01   	
	功能：UI事件监听插件
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PEListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    public Action<object> onClick;
    public Action<PointerEventData> onClickDown;
    public Action<PointerEventData> onClickUp;
    public Action<PointerEventData> onDrag;

    public object args;

    public void OnPointerClick(PointerEventData eventData) {
        if (onClick != null) {
            onClick(args);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (onClickDown != null) {
            onClickDown(eventData);
        }
    }
    public void OnPointerUp(PointerEventData eventData) {
        if (onClickUp != null) {
            onClickUp(eventData);
        }
    }
    public void OnDrag(PointerEventData eventData) {
        if (onDrag != null) {
            onDrag(eventData);
        }
    }

}