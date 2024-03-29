using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace QuantomCOMPPC
{
    public static class WorldObject
    {
        public enum EnvironmentObject
        {
            Board,
            BlochSphere,
            ProbabilitiesGraph
        }

        public enum Gates
        {
            //TODO: INSERT GATES
            None,
            Hgate,
            Notgate,
            CNotgate,
            Toffoligate,
            Measurementgate,
            Resetgate,
        }
        public static List<string> listOfWObjects = new List<string>() { "Board", "Sphere" };
        public static List<string> listOfGates = new List<string>(){"None","H gate", "NOT gate", "CNOT gate", "Toffoli gate", "Measurement" };
    }


    public class Content : MonoBehaviour
    {
        public delegate void SetPositionToWObject(WorldObject.EnvironmentObject wObject);
        public static event SetPositionToWObject OnSelectedWorldObjectEvent;

        public delegate void SetPositionToWObjectGates(WorldObject.Gates wObject);
        public static event SetPositionToWObjectGates OnSelectedWorldObjectGatesEvent;

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
                foreach (Transform child in GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("Content").transform)
                {
                    if (child.name.Contains(objectName))
                        child.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform child in GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("Menu").transform.Find("Content").transform)
                {
                    if (child.name.Contains(objectName))
                        child.gameObject.SetActive(false);
                }
            }
        }

        public static void Beginning()
        {

        }


        protected void selectWorldObject(WorldObject.EnvironmentObject wObject)
        {
            OnSelectedWorldObjectEvent(wObject);
        }

        protected void selectWorldObjectGate(WorldObject.Gates wObject)
        {
            OnSelectedWorldObjectGatesEvent(wObject);
        }
    }
}

