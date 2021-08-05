using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public static void enableDisableQubitsAcceptGatesButton(bool state)
        {
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("SetGates").transform.Find("Content").transform.Find("AcceptButton").gameObject.SetActive(state);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("SetGates").transform.Find("Content").transform.Find("AcceptButton").gameObject.SetActive(state);
        }

        public static void enableOneAreaGatesButtons()
        {
            GameObject gates_p = GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("Content").transform.Find("Gates").gameObject;
            GameObject gates_l = GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("Content").transform.Find("Gates").gameObject;
            foreach (Transform _gate in gates_p.transform)
            {
                //Debug.Log(_gate.name);
                if(!_gate.name.Contains("CNotGate") && !_gate.name.Contains("ToffoliGate"))
                {
                    _gate.GetComponent<Button>().enabled = true;
                }
            }
            foreach (Transform _gate in gates_l.transform)
            {
                if (!_gate.name.Contains("CNotGate") && !_gate.name.Contains("ToffoliGate"))
                {
                    _gate.GetComponent<Button>().enabled = true;
                }
            }
        }

        public static void enableTwoAreaGatesButtons()
        {
            GameObject gates_p = GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("Content").transform.Find("Gates").gameObject;
            GameObject gates_l = GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("Content").transform.Find("Gates").gameObject;
            foreach (Transform _gate in gates_p.transform)
            {
                if (!_gate.name.Contains("ToffoliGate"))
                {
                    _gate.GetComponent<Button>().enabled = true;
                }
            }
            foreach (Transform _gate in gates_l.transform)
            {
                if (!_gate.name.Contains("ToffoliGate"))
                {
                    _gate.GetComponent<Button>().enabled = true;
                }
            }
        }

        public static void enableThreeAreaGatesButtons()
        {
            GameObject gates_p = GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("Content").transform.Find("Gates").gameObject;
            GameObject gates_l = GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("Content").transform.Find("Gates").gameObject;
            foreach (Transform _gate in gates_p.transform)
            {
                _gate.GetComponent<Button>().enabled = true;
            }
            foreach (Transform _gate in gates_l.transform)
            {
                _gate.GetComponent<Button>().enabled = true;
            }
        }

        public static void disableAllAreaGatesButtons()
        {
            GameObject gates_p = GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("Content").transform.Find("Gates").gameObject;
            GameObject gates_l = GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("Content").transform.Find("Gates").gameObject;
            foreach (Transform _gate in gates_p.transform)
            {
                _gate.GetComponent<Button>().enabled = false;
            }
            foreach (Transform _gate in gates_l.transform)
            {
                _gate.GetComponent<Button>().enabled = false;
            }
        }

    }
}

