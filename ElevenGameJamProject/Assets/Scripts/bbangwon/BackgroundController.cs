using System.Collections;
using System.Collections.Generic;
using Tang3.Common.Management;
using UnityEngine;

namespace eleven.game
{

    public class BackgroundController : SingletonMonoBase<BackgroundController>
    {
        [SerializeField]
        SwitchScrollMap cloud;

        [SerializeField]
        SwitchScrollMap mountain;

        [SerializeField]
        SwitchScrollMap ice;

        [SerializeField]
        ScrollMap gridScroller;
                
        public GroundMapManager groundMapManager;

        private void Awake()
        {
            PauseScroll();

#if UNITY_ANDROID
            int windowModeIndex = PlayerPrefs.GetInt("Window", 0);
            int resolutionModeIndex = PlayerPrefs.GetInt("Resolution", 0);
            int resolutionX = 1920, resolutionY = 1080;

            if (windowModeIndex == 0 && resolutionModeIndex == 0)
            {
                resolutionX = 1920;
                resolutionY = 1080;
            }
            else if (windowModeIndex == 0 && resolutionModeIndex == 1)
            {
                resolutionX = 1280;
                resolutionY = 720;
            }

            Screen.SetResolution(resolutionX, resolutionY, true);
#endif
        }

        public void Init()
        {
            groundMapManager.Init();            
        }

        public void StartGame()
        {            
            ResumeScroll();
            gridScroller.ResetScroll();
            groundMapManager.NewPlayer();
        }

        public void PauseScroll()
        {
            cloud.Scroll = false;
            mountain.Scroll = false;
            ice.Scroll = false;
            gridScroller.Scroll = false;
        }

        public void ResumeScroll()
        {
            cloud.Scroll = true;
            mountain.Scroll = true;
            ice.Scroll = true;
            gridScroller.Scroll = true;
        }
    }
}