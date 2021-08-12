using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMP{

    public enum Section
    {
        Environment,
        Gates,
        Run,
        None,
    }

    public class Menu : MonoBehaviour
    {

        public delegate void SelectSection(Section section);
        public static event SelectSection OnSelectedSectionEvent;

        public void selectSection(int _section)
        {         
            if (!Canvas.isMenuActive)
            {
                activateContent(true);
            }
            else if (Canvas.isMenuActive && _section == Canvas.section)
            {
                activateContent(false);
            }
            Canvas.section = _section;
            SharedStateSwitch.setAllButtonsInactive();
            switch (Canvas.section)
            {
                case 0:                  
                    if(Canvas.isMenuActive)
                        SharedStateSwitch.setButtonActive("SetEnvironmentTab");
                    OnSelectedSectionEvent(Section.Environment);
                    break;
                case 1:
                    if (QbitsBoard.listOfQbits.Count() > 0)
                    {
                        if (Canvas.isMenuActive)
                            SharedStateSwitch.setButtonActive("GatesTab");
                        OnSelectedSectionEvent(Section.Gates);
                    }
                    else
                        activateContent(false);    
                    break;
                case 2:
                    if (QbitsBoard.listOfQbits.Count() > 0)
                    {
                        if (Canvas.isMenuActive)
                            SharedStateSwitch.setButtonActive("RunTab");
                        OnSelectedSectionEvent(Section.Run);
                    }
                    else
                        activateContent(false);                    
                    break;
            }
        }

        private void activateContent(bool v)
        {
            SharedStateSwitch.enableDisableContent(v);
            Canvas.isMenuActive = v;
        }

        private void Start()
        {
            
        }
    }
}

