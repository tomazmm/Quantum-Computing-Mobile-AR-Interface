using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMP
{
    public class ResultNotification : MonoBehaviour
    {
        public static string stateVectorsResult;
        // Start is called before the first frame update
        void Start()
        {
            changeState();
        }

        public void closeNotification(GameObject gObject)
        {
            SharedStateSwitch.typeOfResult = "States";
            Parser.setAllGatesWithState();
            Parser.cleanList();
            gObject.SetActive(false);
            SharedStateSwitch.quState = 0;
        }

        public void nextState()
        {
            SharedStateSwitch.typeOfResult = "States";
            SharedStateSwitch.quState++;           
            changeState();
        }

        public void previousState()
        {
            SharedStateSwitch.typeOfResult = "States";
            SharedStateSwitch.quState--;
            changeState();
        }

        public void switchStateResult()
        {
            if (SharedStateSwitch.typeOfResult.Contains("States"))
                SharedStateSwitch.typeOfResult = "End";
            else
                SharedStateSwitch.typeOfResult = "States";
                
            SharedStateSwitch.changeResultPresentation(SharedStateSwitch.typeOfResult);
        }

        public static void changeState()
        {
            stateVectorsResult = Parser.getState();
            SharedStateSwitch.changeResultStateText();
        }


        // Update is called once per frame
        void Update()
        {
            if (SharedStateSwitch.quState == 0)
            {
                SharedStateSwitch.enableDisableNotificationPreviousButton(false);
            }
            else
            {
                SharedStateSwitch.enableDisableNotificationPreviousButton(true);
            }
            
            if (Parser.sorted_sv_keys.Count() - 1 > SharedStateSwitch.quState)
            {
                SharedStateSwitch.enableDisableNotificationNextButton(true);
            }
            else
            {
                SharedStateSwitch.enableDisableNotificationNextButton(false);
            }
        }      
    }
}

