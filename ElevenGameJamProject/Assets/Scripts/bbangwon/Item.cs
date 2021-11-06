using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eleven.game
{
    public class Item : ObjectBase
    {        
        public int Score;

        private void Start()
        {
            tag = "Item";
        }
    }
}