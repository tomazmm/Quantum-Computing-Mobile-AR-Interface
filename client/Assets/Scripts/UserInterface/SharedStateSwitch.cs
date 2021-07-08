using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class SharedStateSwitch : MonoBehaviour
    {
        public static void enableDisablePositioning(bool state)
        {
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("SetPositionScreen").gameObject.SetActive(state);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("SetPositionScreen").gameObject.SetActive(state);
            GameObject.Find("Markers").transform.Find("Indicator").gameObject.SetActive(state);
        }

        public static void enableDisableToggleMenuButton(bool state)
        {
            GameObject.Find("Canvas").transform.Find("ToggleMenu").gameObject.SetActive(state);
        }

        public static void enableDisableMenu(bool state)
        {
            Canvas.toggleMenu = state;
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").gameObject.SetActive(state);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").gameObject.SetActive(state);
            Canvas.toggleMenu = !state;
        }


    }
}

