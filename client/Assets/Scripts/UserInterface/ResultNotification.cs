using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMP
{
    public class ResultNotification : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            changeState();
        }

        public void closeNotification(GameObject gObject)
        {
            gObject.SetActive(false);
            SharedStateSwitch.quState = 0;
        }

        public void nextState()
        {
            SharedStateSwitch.quState++;
            changeState();
        }

        public void previousState()
        {
            SharedStateSwitch.quState--;
            changeState();
        }

        private void changeState()
        {
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

            //TODO: disable next if there is no more states in list left.
        }
    }
}

