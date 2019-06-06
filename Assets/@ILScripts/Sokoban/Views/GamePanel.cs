using GameKit;
using IL.Zero;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace IL
{
    class GamePanel : AView
    {
        Button _btnBack;
        Button _btnReback;
        Joystick _js;
        GameStage _stage;

        protected override void OnInit()
        {
            _stage = StageMgr.Ins.Switch<GameStage>();
            _btnBack = GetChildComponent<Button>("BtnBack");
            _btnReback = GetChildComponent<Button>("BtnReback");
            _js = GetChildComponent<Joystick>("Joystick");
            _js.camera = GameObject.Find("UICamera").GetComponent<Camera>();
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
                    newDir = value.x < 0 ? EDir.LEFT : EDir.RIGHT;
                }
                else
                {
                    //纵向移动
                    newDir = value.y < 0 ? EDir.DOWN : EDir.UP;
                }
            }

            //Log.CI(Log.COLOR_YELLOW, "移动方向:{0}", newDir);
            _stage.MoveRole(newDir);
        }

        private void OnClickReback()
        {
            _stage.Revoke();
        }

        private void OnClickBack()
        {
            MsgWin.Show("Exit？", true, () => { Global.Ins.menu.ShowMenu(true); });
        }
    }
}
