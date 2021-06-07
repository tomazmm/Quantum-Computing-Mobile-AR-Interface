using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public static class WorldObject
    {
        public enum EnvironmentObject
        {
            Board = 0,
            BlochSphere = 1
        }

        public enum Gates
        {
            //TODO: INSERT GATES
            xx = 10,
            yy = 11
        }
    }


    public class Content : MonoBehaviour
    {
        //TODO: add eventlistener function to onSelectedWorldObjectEvent
        public delegate void SetPositionToWObject(int wObject);
        public static event SetPositionToWObject OnSelectedWorldObjectEvent;

        protected string objectName = null;
        protected Section nameOfSection;     

        protected void subscribeToSectionEvent()
        {
            Menu.OnSelectedSectionEvent += onSelected;
        }

        protected void onSelected(Section section)
        {
            if (section.Equals(nameOfSection))
            {
                foreach (Transform child in GameObject.Find("Content").transform)
                {
                    if (child.name.Contains(objectName))
                        child.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform child in GameObject.Find("Content").transform)
                {
                    if (child.name.Contains(objectName))
                        child.gameObject.SetActive(false);
                }
            }
        }

        protected void selectWorldObject(int wObject)
        {
            OnSelectedWorldObjectEvent(wObject);
        }
    }
}

