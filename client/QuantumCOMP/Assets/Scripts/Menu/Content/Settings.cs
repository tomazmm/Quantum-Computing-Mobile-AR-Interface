using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
public class Settings : Content
    {
        private void Start()
        {
            objectName = "Settings";
            nameOfSection = Section.Settings;
            subscribeToSectionEvent();
            gameObject.SetActive(false);
        }    
    }
}


