/****************************************************
    文件：TaskWnd.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/13 14:01   	
	功能：任务奖励界面
*****************************************************/

using PEProtocol;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskWnd : WindowRoot {
    public Transform scrollTrans;

    private PlayerData pd = null;
    private List<TaskRewardData> trdLst = new List<TaskRewardData>();

    protected override void InitWnd() {
        base.InitWnd();

        pd = GameRoot.Instance.PlayerData;
        RefreshUI();
    }

    public void RefreshUI() {
        trdLst.Clear();

        List<TaskRewardData> todoLst = new List<TaskRewardData>();
        List<TaskRewardData> doneLst = new List<TaskRewardData>();

        //1|0|0
        for (int i = 0; i < pd.taskArr.Length; i++) {
            string[] taskInfo = pd.taskArr[i].Split('|');
            TaskRewardData trd = new TaskRewardData {
                ID = int.Parse(taskInfo[0]),
                prgs = int.Parse(taskInfo[1]),
                taked = taskInfo[2].Equals("1")
            };

            if (trd.taked) {
                doneLst.Add(trd);
            }
            else {
                todoLst.Add(trd);
            }
        }

        trdLst.AddRange(todoLst);
        trdLst.AddRange(doneLst);

        for (int i = 0; i < scrollTrans.childCount; i++) {
            Destroy(scrollTrans.GetChild(i).gameObject);
        }

        for (int i = 0; i < trdLst.Count; i++) {
            GameObject go = resSvc.LoadPrefab(PathDefine.TaskItemPrefab);
            go.transform.SetParent(scrollTrans);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.name = "taskItem_" + i;

            TaskRewardData trd = trdLst[i];
            TaskRewardCfg trf = resSvc.GetTaskRewardCfg(trd.ID);

            SetText(GetTrans(go.transform, "txtName"), trf.taskName);
            SetText(GetTrans(go.transform, "txtPrg"), trd.prgs + "/" + trf.count);
            SetText(GetTrans(go.transform, "txtExp"), "奖励：    经验" + trf.exp);
            SetText(GetTrans(go.transform, "txtCoin"), "金币" + trf.coin);
            Image imgPrg = GetTrans(go.transform, "prgBar/prgVal").GetComponent<Image>();
            float prgVal = trd.prgs * 1.0f / trf.count;
            imgPrg.fillAmount = prgVal;

            Button btnTake = GetTrans(go.transform, "btnTake").GetComponent<Button>();
            //btnTake.onClick.AddListener(ClickTakeBtn);
            btnTake.onClick.AddListener(() => {
                ClickTakeBtn(go.name);
            });

            Transform transComp = GetTrans(go.transform, "imgComp");
            if (trd.taked) {
                btnTake.interactable = false;
                SetActive(transComp);
            }
            else {
                SetActive(transComp, false);
                if (trd.prgs == trf.count) {
                    btnTake.interactable = true;
                }
                else {
                    btnTake.interactable = false;
                }
            }

        }
    }


    private void ClickTakeBtn(string name) {
        string[] nameArr = name.Split('_');
        int index = int.Parse(nameArr[1]);
        GameMsg msg = new GameMsg {
            cmd = (int)CMD.ReqTakeTaskReward,
            reqTakeTaskReward = new ReqTakeTaskReward {
                rid = trdLst[index].ID
            }
        };

        netSvc.SendMsg(msg);

        TaskRewardCfg trc = resSvc.GetTaskRewardCfg(trdLst[index].ID);
        int coin = trc.coin;
        int exp = trc.exp;
        GameRoot.AddTips(Constants.Color("获得奖励：", TxtColor.Blue) + Constants.Color(" 金币 +" + coin + " 经验 +" + exp, TxtColor.Green));
    }

    public void ClickCloseBtn() {
        SetWndState(false);
    }
}