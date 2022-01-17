using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMPPC
{
    public class ToggleMenuButton : MonoBehaviour
    {
        public void menuState()
        {
            SharedStateSwitch.enableDisableMenu(Canvas.toggleMenu);
        }
    }
}

