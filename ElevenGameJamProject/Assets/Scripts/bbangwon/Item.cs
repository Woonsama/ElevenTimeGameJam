using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace eleven.game
{
    public class Item : ObjectBase
    {        
        public int Score;

        private void Start()
        {
            tag = "Item";

            transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}