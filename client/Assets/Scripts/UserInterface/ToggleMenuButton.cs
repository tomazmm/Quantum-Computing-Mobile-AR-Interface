using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class ToggleMenuButton : MonoBehaviour
    {
        public void menuState()
        {
            SharedStateSwitch.enableDisableMenu(Canvas.toggleMenu);
        }
    }
}

