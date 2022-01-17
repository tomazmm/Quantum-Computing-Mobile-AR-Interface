using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMPPC
{
public class Run : Content
    {
        private void Start()
        {
            objectName = "Run";
            nameOfSection = Section.Run;
            subscribeToSectionEvent();
            //gameObject.SetActive(false);
        }

        private void Update()
        {
            if(Canvas.section == (int)Section.Run)
            {
                var qasm = Interpreter.boardToQasm();
                ApiConnector.runCircuit(qasm);

                foreach (Transform child in GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("Content").transform)
                {
                    if (child.name.Contains(objectName))
                        child.gameObject.SetActive(false);
                }
                SharedStateSwitch.setAllButtonsInactive();
                Canvas.isMenuActive = false;
                Canvas.section = (int)Section.None;
            }         
        }
    }
}


