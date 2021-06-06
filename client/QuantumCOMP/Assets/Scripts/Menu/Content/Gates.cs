using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class Gates : MonoBehaviour
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
            if (section.Equals(Section.Gates))
            {
                foreach (Transform child in GameObject.Find("Content").transform)
                {
                    if (child.name.Contains("Gates"))
                        child.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform child in GameObject.Find("Content").transform)
                {
                    if (child.name.Contains("Gates"))
                        child.gameObject.SetActive(false);
                }
            }

        }
    }
}

