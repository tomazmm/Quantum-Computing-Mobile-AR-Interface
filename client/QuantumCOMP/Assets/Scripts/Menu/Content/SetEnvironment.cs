using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuantomCOMP
{
    public class SetEnvironment : MonoBehaviour
    {

        void Start()
        {
            subscribeToSectionEvent();
        }

        private void subscribeToSectionEvent()
        {
            Menu.OnSelectedSectionEvent += onSelected;
        }

        private void onSelected(Section section)
        {
            if (section.Equals(Section.Environment))
            {
                foreach (Transform child in GameObject.Find("Content").transform)
                {
                    if (child.name.Contains("SetEnvironment"))
                        child.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform child in GameObject.Find("Content").transform)
                {
                    if (child.name.Contains("SetEnvironment"))
                        child.gameObject.SetActive(false);
                }
            }

        }


    }
}

