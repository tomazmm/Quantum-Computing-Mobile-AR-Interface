using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class SharedStateSwitch : MonoBehaviour
    {
        public static void enableDisablePositioning(bool state)
        {
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("SetPositionScreen").transform.Find("Content").gameObject.SetActive(state);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("SetPositionScreen").transform.Find("Content").gameObject.SetActive(state);
            GameObject.Find("Markers").transform.Find("Indicator").gameObject.SetActive(state);
        }

        public static void enableDisableGatePositioning(bool state)
        {
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("SetGates").transform.Find("Content").gameObject.SetActive(state);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("SetGates").transform.Find("Content").gameObject.SetActive(state);
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

        public static void enableDisableQubitsMenu(bool state)
        {
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("SetPositionScreen").transform.Find("Content").transform.Find("Qubits").gameObject.SetActive(state);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("SetPositionScreen").transform.Find("Content").transform.Find("Qubits").gameObject.SetActive(state);
        }

    }
}

