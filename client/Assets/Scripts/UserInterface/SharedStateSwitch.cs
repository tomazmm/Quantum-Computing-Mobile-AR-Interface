using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMP
{
    public class SharedStateSwitch : MonoBehaviour
    {
        public static int quState;
        public static string typeOfResult = "States";
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

        //public static void enableDisableToggleMenuButton(bool state)
        //{
        //    GameObject.Find("Canvas").transform.Find("ToggleMenu").gameObject.SetActive(state);
        //}

        public static void enableDisableMenu(bool state)
        {
            Canvas.toggleMenu = state;
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").gameObject.SetActive(state);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").gameObject.SetActive(state);
            Canvas.toggleMenu = !state;
        }

        public static void enableDisableContent(bool state)
        {
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("Content").gameObject.SetActive(state);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("Content").gameObject.SetActive(state);
        }

        public static void enableDisableNotification(bool state)
        {
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("ResultNotification").gameObject.SetActive(state);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("ResultNotification").gameObject.SetActive(state);
        }

        public static void enableDisableBottomMenuNavigation(bool state)
        {
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("BottomTabs").gameObject.SetActive(state);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("BottomTabs").gameObject.SetActive(state);
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

        public static void enableDisableNotificationPreviousButton(bool state)
        {
            string stateOfButton;
            Color color;
            if (state)
            {
                stateOfButton = "";
                color = new Color(50 / 255f, 50 / 255f, 50 / 255f);
            }
                
            else
            {
                stateOfButton = "Disabled";
                color = new Color(222 / 255f, 222 / 255f, 222 / 255f);
            }
                

            Sprite sprite;
            Texture2D activeImageTexture;
            activeImageTexture = Resources.Load("Images/UIIcons/Previous" + stateOfButton) as Texture2D;
            sprite = Sprite.Create(activeImageTexture, new Rect(0.0f, 0.0f, activeImageTexture.width, activeImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);


            var obj_p = GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("ResultNotification").transform.Find("Previous");
            var obj_l = GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("ResultNotification").transform.Find("Previous");

            obj_p.transform.Find("Button").GetComponent<Button>().enabled = state;           
            obj_p.transform.Find("Button").GetComponent<Image>().sprite = sprite;
            obj_p.Find("Text").gameObject.GetComponent<Text>().color = color;

            obj_l.transform.Find("Button").GetComponent<Button>().enabled = state;
            obj_l.transform.Find("Button").GetComponent<Image>().sprite = sprite;
            obj_l.Find("Text").gameObject.GetComponent<Text>().color = color;

        }

        public static void enableDisableNotificationNextButton(bool state)
        {
            string stateOfButton;
            Color color;
            if (state)
            {
                stateOfButton = "";
                color = new Color(50 / 255f, 50 / 255f, 50 / 255f);
            }

            else
            {
                stateOfButton = "Disabled";
                color = new Color(222 / 255f, 222 / 255f, 222 / 255f);
            }


            Sprite sprite;
            Texture2D activeImageTexture;
            activeImageTexture = Resources.Load("Images/UIIcons/Next" + stateOfButton) as Texture2D;
            sprite = Sprite.Create(activeImageTexture, new Rect(0.0f, 0.0f, activeImageTexture.width, activeImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);


            var obj_p = GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("ResultNotification").transform.Find("Next");
            var obj_l = GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("ResultNotification").transform.Find("Next");

            obj_p.transform.Find("Button").GetComponent<Button>().enabled = state;
            obj_p.transform.Find("Button").GetComponent<Image>().sprite = sprite;
            obj_p.Find("Text").gameObject.GetComponent<Text>().color = color;

            obj_l.transform.Find("Button").GetComponent<Button>().enabled = state;
            obj_l.transform.Find("Button").GetComponent<Image>().sprite = sprite;
            obj_l.Find("Text").gameObject.GetComponent<Text>().color = color;

        }

        public static void changeResultPresentation(string type)
        {
            var obj_p = GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("ResultNotification");
            var obj_l = GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("ResultNotification");

            if (type.Contains("End"))
            {
                obj_p.transform.Find("Result").GetComponent<Text>().text = Parser.showMeasurementResult();
                obj_l.transform.Find("Result").GetComponent<Text>().text = Parser.showMeasurementResult();
            }
            else
            {
                obj_p.transform.Find("Result").GetComponent<Text>().text = ResultNotification.stateVectorsResult.ToString();
                obj_l.transform.Find("Result").GetComponent<Text>().text = ResultNotification.stateVectorsResult.ToString();
            }
            
        }

        public static void changeResultStateText()
        {
            var obj_p = GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("ResultNotification");
            var obj_l = GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("ResultNotification");

            //Debug.Log(ResultNotification.stateVectorsResult);
            if (ResultNotification.stateVectorsResult != null)
            {
                obj_p.transform.Find("Result").GetComponent<Text>().text = ResultNotification.stateVectorsResult.ToString();
                obj_l.transform.Find("Result").GetComponent<Text>().text = ResultNotification.stateVectorsResult.ToString();
            }
            
            obj_p.transform.Find("State").GetComponent<Text>().text = "State\n" + quState;
            obj_l.transform.Find("State").GetComponent<Text>().text = "State\n" + quState;
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
                else
                {
                    if(Gestures.gate == WorldObject.Gates.CNotgate || (Gestures.gate == WorldObject.Gates.Toffoligate)){
                        EstablishGateInWorldObject.closeFunction();
                    }
                    _gate.GetComponent<Button>().enabled = false;
                }
            }
            foreach (Transform _gate in gates_l.transform)
            {
                if (!_gate.name.Contains("CNotGate") && !_gate.name.Contains("ToffoliGate"))
                {
                    _gate.GetComponent<Button>().enabled = true;
                }
                else
                {
                    if (Gestures.gate == WorldObject.Gates.CNotgate || (Gestures.gate == WorldObject.Gates.Toffoligate)){
                        EstablishGateInWorldObject.closeFunction();
                    }
                    _gate.GetComponent<Button>().enabled = false;
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
                else
                {
                    if (Gestures.gate == WorldObject.Gates.Toffoligate){
                        EstablishGateInWorldObject.closeFunction();
                    }
                    _gate.GetComponent<Button>().enabled = false;
                }
            }
            foreach (Transform _gate in gates_l.transform)
            {
                if (!_gate.name.Contains("ToffoliGate"))
                {
                    _gate.GetComponent<Button>().enabled = true;
                }
                else
                {
                    if (Gestures.gate == WorldObject.Gates.Toffoligate)
                    {
                        EstablishGateInWorldObject.closeFunction();
                    }
                    _gate.GetComponent<Button>().enabled = false;
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

        public static void setAllButtonsInactive()
        {
            Sprite sprite;
            Texture2D activeImageTexture;

            foreach (Transform child in GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("BottomTabs").transform)
            {
                activeImageTexture = Resources.Load("Images/UIIcons/" + child.name) as Texture2D;
                sprite = Sprite.Create(activeImageTexture, new Rect(0.0f, 0.0f, activeImageTexture.width, activeImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                //activeImage.sprite = sprite;
                //Debug.Log(activeImage);
                if (Canvas.isBoardActive)
                {
                    child.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;
                    child.Find("Text").gameObject.GetComponent<Text>().color = new Color(50 / 255f, 50 / 255f, 50 / 255f);
                }
                if (child.name.Contains("SetEnvironmentTab"))
                {
                    child.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;
                    child.Find("Text").gameObject.GetComponent<Text>().color = new Color(50 / 255f, 50 / 255f, 50 / 255f);
                }
                //child.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;
            }
            foreach (Transform child in GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("BottomTabs").transform)
            {
                activeImageTexture = Resources.Load("Images/UIIcons/" + child.name) as Texture2D;
                sprite = Sprite.Create(activeImageTexture, new Rect(0.0f, 0.0f, activeImageTexture.width, activeImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                //activeImage.sprite = sprite;
                //Debug.Log(activeImage);
                if (Canvas.isBoardActive)
                {
                    child.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;
                    child.Find("Text").gameObject.GetComponent<Text>().color = new Color(50 / 255f, 50 / 255f, 50 / 255f);
                }
                if (child.name.Contains("SetEnvironmentTab"))
                {
                    child.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;
                    child.Find("Text").gameObject.GetComponent<Text>().color = new Color(50 / 255f, 50 / 255f, 50 / 255f);
                }
            }
        }

        public static void setButtonActive(string navigationTab)
        {
            Sprite sprite;
            Texture2D activeImageTexture;
            
            activeImageTexture = Resources.Load("Images/UIIcons/" + navigationTab + "Active") as Texture2D;
            sprite = Sprite.Create(activeImageTexture, new Rect(0.0f, 0.0f, activeImageTexture.width, activeImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            //activeImage.sprite = sprite;
            //Debug.Log(activeImage);
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("BottomTabs").transform.Find(navigationTab).transform.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("BottomTabs").transform.Find(navigationTab).transform.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;

            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("BottomTabs").transform.Find(navigationTab).transform.Find("Text").gameObject.GetComponent<Text>().color = new Color(45 / 255f, 145 / 255f, 252 / 255f);
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("BottomTabs").transform.Find(navigationTab).transform.Find("Text").gameObject.GetComponent<Text>().color = new Color(45 / 255f, 145 / 255f, 252 / 255f);

        }

        public static void disableNavigationButtons()
        {
            Sprite sprite;
            Texture2D activeImageTexture;

            foreach (Transform child in GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("BottomTabs").transform)
            {
                if (!child.name.Contains("SetEnvironmentTab"))
                {
                    activeImageTexture = Resources.Load("Images/UIIcons/" + child.name + "Disabled") as Texture2D;
                    sprite = Sprite.Create(activeImageTexture, new Rect(0.0f, 0.0f, activeImageTexture.width, activeImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    //activeImage.sprite = sprite;
                    //Debug.Log(child.Find("Text").gameObject.GetComponent<Text>().color);
                    child.Find("Text").gameObject.GetComponent<Text>().color = new Color(222/255f, 222 / 255f, 222 / 255f);
                    child.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;
                }
            }
            foreach (Transform child in GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("BottomTabs").transform)
            {
                if (!child.name.Contains("SetEnvironmentTab"))
                {
                    activeImageTexture = Resources.Load("Images/UIIcons/" + child.name + "Disabled") as Texture2D;
                    sprite = Sprite.Create(activeImageTexture, new Rect(0.0f, 0.0f, activeImageTexture.width, activeImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    //activeImage.sprite = sprite;
                    //Debug.Log(activeImage);
                    child.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;
                    child.Find("Text").gameObject.GetComponent<Text>().color = new Color(222 / 255f, 222 / 255f, 222 / 255f);
                }
            }
        }

        public static void enableNavigationButtons()
        {
            Sprite sprite;
            Texture2D activeImageTexture;

            foreach (Transform child in GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("Menu").transform.Find("BottomTabs").transform)
            {
                if (!child.name.Contains("SetEnvironmentTab"))
                {
                    activeImageTexture = Resources.Load("Images/UIIcons/" + child.name) as Texture2D;
                    sprite = Sprite.Create(activeImageTexture, new Rect(0.0f, 0.0f, activeImageTexture.width, activeImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    //activeImage.sprite = sprite;
                    child.Find("Text").gameObject.GetComponent<Text>().color = new Color(50 / 255f, 50 / 255f, 50 / 255f);
                    child.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;
                }
            }
            foreach (Transform child in GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("BottomTabs").transform)
            {
                if (!child.name.Contains("SetEnvironmentTab"))
                {
                    activeImageTexture = Resources.Load("Images/UIIcons/" + child.name) as Texture2D;
                    sprite = Sprite.Create(activeImageTexture, new Rect(0.0f, 0.0f, activeImageTexture.width, activeImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    //activeImage.sprite = sprite;
                    //Debug.Log(activeImage);
                    child.Find("Icon").gameObject.GetComponent<Image>().sprite = sprite;
                    child.Find("Text").gameObject.GetComponent<Text>().color = new Color(50 / 255f, 50 / 255f, 50 / 255f);
                }
            }
        }

    }
}

