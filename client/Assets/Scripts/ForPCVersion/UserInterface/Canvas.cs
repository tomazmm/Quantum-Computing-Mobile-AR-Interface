using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMPPC
{
    public enum ScreenRotation
    {
        Portrait,
        Landscape
    }

    public class Canvas : MonoBehaviour
    {
        public static bool toggleMenu;
        public static ScreenRotation screenState;

        public static int section;
        public static bool isMenuActive;
        public static bool isBoardActive;
       

        private void Start()
        {
            SharedStateSwitch.enableDisablePositioning(false);
            SharedStateSwitch.enableDisableMenu(true);
            //SharedStateSwitch.enableDisableContent(false);
            SharedStateSwitch.enableDisableBottomMenuNavigation(true);
            SharedStateSwitch.disableNavigationButtons();
            SharedStateSwitch.enableDisableNotification(false);
            SharedStateSwitch.quState = 0;
            
            section = 0;
            isMenuActive = false;
            isBoardActive = false;
            //TODO: make async function for very begining, which will set board and sphere
            Content.Beginning();
        }
    }
}

