using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
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
        private void setScreenState()
        {
            if (Screen.width <= Screen.height)
                screenState = ScreenRotation.Portrait;
            else
                screenState = ScreenRotation.Landscape;

            foreach (Transform child in gameObject.transform)
            {
                if (screenState == ScreenRotation.Portrait)
                {
                    if (child.name.Contains("Portrait"))
                        child.gameObject.SetActive(true);
                    if (child.name.Contains("Landscape"))
                        child.gameObject.SetActive(false);
                }
                else
                {
                    if (child.name.Contains("Portrait"))
                        child.gameObject.SetActive(false);
                    if (child.name.Contains("Landscape"))
                        child.gameObject.SetActive(true);
                }
            }           
        }

        private void Start()
        {
            SharedStateSwitch.enableDisablePositioning(false);
            SharedStateSwitch.enableDisableMenu(true);
            SharedStateSwitch.enableDisableContent(false);
            SharedStateSwitch.enableDisableBottomMenuNavigation(true);
            SharedStateSwitch.disableNavigationButtons();
            SharedStateSwitch.enableDisableNotification(false);
            section = 0;
            isMenuActive = false;
            isBoardActive = false;
            //TODO: make async function for very begining, which will set board and sphere
            Content.Beginning();
            setScreenState();
        }

        private void OnRectTransformDimensionsChange()
        {
            setScreenState();
        }
    }
}

