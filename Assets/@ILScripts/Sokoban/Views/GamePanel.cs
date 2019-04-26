﻿using GameKit;
using IL.Zero;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zero;

namespace IL
{
    class GamePanel : AView
    {
        Button _btnBack;
        Button _btnReback;
        Joystick _js;

        protected override void OnInit()
        {
            StageMgr.Ins.Switch<GameStage>();
            _btnBack = GetChildComponent<Button>("BtnBack");
            _btnReback = GetChildComponent<Button>("BtnReback");
            _js = GetChildComponent<Joystick>("Joystick");
        }

        protected override void OnDestroy()
        {
            StageMgr.Ins.Clear();
        }

        protected override void OnEnable()
        {
            _btnBack.onClick.AddListener(OnClickBack);
            _btnReback.onClick.AddListener(OnClickReback);
            _js.onValueChange += OnValueChange;
        }

        private void OnValueChange(Vector2 value)
        {
            EDir newDir;
            if (value == Vector2.zero)
            {
                newDir = EDir.NONE;
            }
            else
            {
                value = value.normalized;
                if (Math.Abs(value.x) > Math.Abs(value.y))
                {
                    //横向移动
                    if (value.x < 0)
                    {
                        newDir = EDir.LEFT;
                    }
                    else
                    {
                        newDir = EDir.RIGHT;
                    }
                }
                else
                {
                    //纵向移动
                    if (value.y < 0)
                    {
                        newDir = EDir.DOWN;
                    }
                    else
                    {
                        newDir = EDir.UP;
                    }
                }
            }

            Log.CI(Log.COLOR_YELLOW, "移动方向:{0}", newDir);
        }

        private void OnClickReback()
        {
            
        }

        private void OnClickBack()
        {
            MsgWin.Show("退出关卡？", true, () => {
                UIPanelMgr.Ins.Switch<MenuPanel>(); 
            });
        }
    }
}
