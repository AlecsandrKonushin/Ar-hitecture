using System;
using System.Collections.Generic;
//using AppodealStack.Monetization.Api;
//using AppodealStack.Monetization.Common;
using Logs;
using SaveSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class AdManager : Singleton<AdManager>//, IAppodealInitializationListener
    {
        private const string APPODEAL_KEY = "3238fb5847926b09fc819b4415d7ce21d83afd5165f28ae2";
        private const float timeInterstitial = 120f;

        [HideInInspector]
        public UnityEvent InitEvent = new UnityEvent(), EndShowInterstitialEvent = new UnityEvent(), ShowRewardEvent = new UnityEvent();

        private float currentTimeInterstitial;
        private bool timeGo, timeFinished;

        public bool SetCanShowAd { set => timeGo = value; }

        public bool CanShowInterstitial
        {
            get => true;
                //Appodeal.IsLoaded(AppodealAdType.Interstitial) &&
                //Appodeal.CanShow(AppodealAdType.Interstitial, "default") &&
                //!Appodeal.IsPrecache(AppodealAdType.Interstitial);
        }

        public bool CanShowInterstitialShop
        {
            get => true;
                //Appodeal.IsLoaded(AppodealAdType.Interstitial) &&
                //Appodeal.CanShow(AppodealAdType.Interstitial, "default") &&
                //!Appodeal.IsPrecache(AppodealAdType.Interstitial) &&
                //timeFinished;
        }

        public bool CanShowReward
        {
            get => true;
                //Appodeal.IsLoaded(AppodealAdType.RewardedVideo) &&
                //Appodeal.CanShow(AppodealAdType.RewardedVideo, "default") &&
                //!Appodeal.IsPrecache(AppodealAdType.RewardedVideo);
        }

        private void Update()
        {
            if (timeGo)
            {
                currentTimeInterstitial -= Time.deltaTime;

                if(currentTimeInterstitial <= 0)
                {
                    timeFinished = true;
                    timeGo = false;
                    currentTimeInterstitial = timeInterstitial;
                }
            }
        }

        public void Init(bool isTestAd)
        {
            currentTimeInterstitial = timeInterstitial;

            //if (isTestAd)
            //{
            //    Appodeal.SetTesting(isTestAd);
            //}

            //int adTypes = AppodealAdType.Interstitial | AppodealAdType.RewardedVideo;

            //Appodeal.Initialize(APPODEAL_KEY, adTypes, this);

            //Appodeal.Cache(AppodealAdType.Interstitial);
            //Appodeal.Cache(AppodealAdType.RewardedVideo);

            //AppodealCallbacks.Interstitial.OnLoaded += OnInterstitialLoaded;
            //AppodealCallbacks.Interstitial.OnFailedToLoad += OnInterstitialLoadFailed;
            //AppodealCallbacks.Interstitial.OnShown += OnInterstitialShown;
            //AppodealCallbacks.Interstitial.OnFailedToLoad += OnInterstitialFailedShow;
            //AppodealCallbacks.Interstitial.OnClosed += OnInterstitialClosed;
            //AppodealCallbacks.Interstitial.OnClicked += OnInterstitialCLicked;
            //AppodealCallbacks.Interstitial.OnExpired += OnInterstitialExpired;

            //AppodealCallbacks.RewardedVideo.OnLoaded += OnRewardLoaded;
            //AppodealCallbacks.RewardedVideo.OnFailedToLoad += OnRewardLoadFailed;
            //AppodealCallbacks.RewardedVideo.OnShown += OnRewardShown;
            //AppodealCallbacks.RewardedVideo.OnFailedToLoad += OnRewardFailedShow;
            //AppodealCallbacks.RewardedVideo.OnClosed += OnRewardClosed;
            //AppodealCallbacks.RewardedVideo.OnClicked += OnRewardCLicked;
            //AppodealCallbacks.RewardedVideo.OnExpired += OnRewardExpired;
        }

        public void OnInitializationFinished(List<string> errors)
        {
            if(errors != null && errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    LogManager.LogError($"Appodeal error {error}");
                }
            }

            InitEvent?.Invoke();
        }        

        public void ShowInterstitial()
        {
            //if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
            //{
            //    Appodeal.Show(AppodealAdType.Interstitial);
            //}
            //else
            //{
            //    LogManager.Log($"Interstitial not ready");
            //}
        }

        public void ShowReward()
        {
            //if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
            //{
            //    Appodeal.Show(AppodealShowStyle.RewardedVideo);
            //}
            //else
            //{
            //    LogManager.Log($"Reward not ready");
            //}
        }

        private void EndShowInterstitial()
        {
            if (!timeGo)
            {
                timeFinished = false;
                timeGo = true;
            }
        }



        //#region EVENTS_INTERSTITIAL

        //private void OnInterstitialLoaded(object sender, AdLoadedEventArgs e)
        //{
        //    LogManager.Log($"Interstitial Loaded");
        //}

        //private void OnInterstitialLoadFailed(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Interstitial load Failed");
        //}

        //private void OnInterstitialShown(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Interstitial shown");

        //    Appodeal.Cache(AppodealAdType.Interstitial);
        //    EndShowInterstitialEvent?.Invoke();
        //    EndShowInterstitial();
        //}

        //private void OnInterstitialFailedShow(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Interstitial failed show");

        //    Appodeal.Cache(AppodealAdType.Interstitial);
        //    EndShowInterstitialEvent?.Invoke();
        //    EndShowInterstitial();
        //}

        //private void OnInterstitialClosed(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Interstitial closed");
        //    EndShowInterstitialEvent?.Invoke();
        //    EndShowInterstitial();
        //}

        //private void OnInterstitialCLicked(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Interstitial clicked");
        //}

        //private void OnInterstitialExpired(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Interstitial expiredLogManager");

        //    Appodeal.Cache(AppodealAdType.Interstitial);
        //    EndShowInterstitialEvent?.Invoke();
        //}

        //#endregion

        //#region EVENTS_REWARD_VIDEO

        //private void OnRewardLoaded(object sender, AdLoadedEventArgs e)
        //{
        //    LogManager.Log($"Reward Loaded");
        //}

        //private void OnRewardLoadFailed(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Reward load Failed");
        //}

        //private void OnRewardShown(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Reward shown");

        //    Appodeal.Cache(AppodealAdType.RewardedVideo);
        //}

        //private void OnRewardFailedShow(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Reward failed show");

        //    Appodeal.Cache(AppodealAdType.RewardedVideo);
        //}

        //private void OnRewardClosed(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Reward closed");

        //    ShowRewardEvent?.Invoke();
        //}

        //private void OnRewardCLicked(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Reward clicked");
        //}

        //private void OnRewardExpired(object sender, EventArgs e)
        //{
        //    LogManager.Log($"Reward expired");

        //    Appodeal.Cache(AppodealAdType.RewardedVideo);
        //}

        //#endregion
    }
}