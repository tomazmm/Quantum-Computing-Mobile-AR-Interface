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
        private ScreenRotation screenState;
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
                    if (child.name == "Portrait")
                        child.gameObject.SetActive(true);
                    if (child.name == "Landscape")
                        child.gameObject.SetActive(false);
                }
                else
                {
                    if (child.name == "Portrait")
                        child.gameObject.SetActive(false);
                    if (child.name == "Landscape")
                        child.gameObject.SetActive(true);
                }
            }
        }

        private void Start()
        {
            toggleMenu = false;
            setScreenState();
        }

        private void OnRectTransformDimensionsChange()
        {
            setScreenState();
        }
    }
}

