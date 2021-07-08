using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class EstablishWorldObjects : MonoBehaviour
    {
        public void closePositioning()
        {
            SharedStateSwitch.enableDisableMenu(true);
            SharedStateSwitch.enableDisableToggleMenuButton(true);
            SharedStateSwitch.enableDisablePositioning(false);
        }

        public void confirmPositioning()
        {
            SharedStateSwitch.enableDisableMenu(true);
            SharedStateSwitch.enableDisableToggleMenuButton(true);
            SharedStateSwitch.enableDisablePositioning(false);

            //TODO: confirm
        }
    }
}

