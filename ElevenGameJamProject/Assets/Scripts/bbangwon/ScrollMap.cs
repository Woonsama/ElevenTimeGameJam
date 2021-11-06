using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eleven.game
{
    public class ScrollMap : MonoBehaviour
    {
        public bool Scroll = true;

        public float Speed;

        // Update is called once per frame
        void Update()
        {
            if (Scroll)
            {
                transform.Translate(Vector3.left * Speed * Time.deltaTime);
            }
        }

        public void ResetScroll()
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
