using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
public class Settings : MonoBehaviour
{
        void Start()
        {            
            subscribeToSectionEvent();
            gameObject.SetActive(false);
        }

        private void subscribeToSectionEvent()
        {
            Menu.OnSelectedSectionEvent += onSelected;
        }

        private void onSelected(Section section)
        {
            if (section.Equals(Section.Settings))
            {
                foreach (Transform child in GameObject.Find("Content").transform)
                {
                    if (child.name.Contains("Settings"))
                        child.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform child in GameObject.Find("Content").transform)
                {
                    if (child.name.Contains("Settings"))
                        child.gameObject.SetActive(false);
                }
            }

        }
    }
}


