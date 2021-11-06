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

        [SerializeField]
        GroundMapManager groundMapManager;

        private void Awake()
        {
            PauseScroll();
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            ResumeScroll();
            groundMapManager.Init();
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