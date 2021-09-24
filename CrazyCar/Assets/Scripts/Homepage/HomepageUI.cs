﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using TinyMessenger;

public class HomepageUI : MonoBehaviour {
    public Button avatarBtn;
    public Image avatarImage;
    public Image vipImage;
    public Button joinGameBtn;
    public Text starText;
    public Text nickNameText;
    public Button optionOpenBtn;
    public Button optionCloseBtn;
    public GameObject optionBtnsGO;
    public Button infoBtn;
    public Button questionBtn;
    public Button exitBtn;
    public Button psrofileBtn;
    public Button changeCarBtn;
    public Button rankBtn;
    public Button settingBtn;

    private TinyMessageSubscriptionToken homepageToken;

    private void Start() {
        nickNameText.text = GameController.manager.userInfo.name;

        avatarBtn.onClick.AddListener(() => {
            UIManager.manager.ShowPage(UIPageType.AvatarUI);
        });

        joinGameBtn.onClick.AddListener(() => {
            UIManager.manager.ShowPage(UIPageType.TimeTrialDetailUI);
        });
        //--------- option ---------
        optionBtnsGO.SetActiveFast(false);
        optionCloseBtn.gameObject.SetActiveFast(false);
        optionOpenBtn.gameObject.SetActiveFast(true);
        optionOpenBtn.onClick.AddListener(() => {
            optionBtnsGO.SetActiveFast(true);
            optionOpenBtn.gameObject.SetActiveFast(false);
            optionCloseBtn.gameObject.SetActiveFast(true);
        });
        optionCloseBtn.onClick.AddListener(() => {
            optionBtnsGO.SetActiveFast(false);
            Util.DelayExecuteWithSecond(0.25f, () => {
                optionCloseBtn.gameObject.SetActiveFast(false);
                optionOpenBtn.gameObject.SetActiveFast(true);
            });
        });
        infoBtn.onClick.AddListener(() => {
            GameController.manager.infoConfirmAlert.ShowWithText(title: "Version", content: "当前版本为：" + Application.version);
        });
        questionBtn.onClick.AddListener(() => {
            GameController.manager.infoConfirmAlert.ShowWithText(title: "Content", content: "作者：TastSong 邮箱地址：TastSong@163.com");
        });
        exitBtn.onClick.AddListener(() => {
            Application.Quit();
        });
        //--------- option ---------

        // --------- Bottom Btns ---------
        psrofileBtn.onClick.AddListener(() => {
            UIManager.manager.ShowPage(UIPageType.ProfileUI);
        });
        settingBtn.onClick.AddListener(() => {
            UIManager.manager.ShowPage(UIPageType.SettingsUI);
        });
        rankBtn.onClick.AddListener(() => {
            UIManager.manager.ShowPage(UIPageType.RankUI);
        });
        changeCarBtn.onClick.AddListener(() => {
            UIManager.manager.ShowPage(UIPageType.ChangeCarUI);
        });
        // --------- Bottom Btns ---------

        UpdataUI();
        homepageToken = GameController.manager.tinyMsgHub.Subscribe<HomepageUIMessage>((m) => { UpdataUI(); });
    }

    private void UpdataUI() {
        avatarImage.sprite = GameController.manager.resourceManager.GetAvatarResource(GameController.manager.userInfo.aid);
        starText.text = GameController.manager.userInfo.star.ToString();
        vipImage.gameObject.SetActiveFast(GameController.manager.userInfo.isVIP);
    }

    private void OnDestroy() {
        GameController.manager.tinyMsgHub.Unsubscribe(homepageToken);
    }
}
