using UnityEngine;

namespace QuantomCOMP
{
    public class SetEnvironment : Content
    {

        private void Start()
        {
            objectName = "SetEnvironment";
            nameOfSection = Section.Environment;
            subscribeToSectionEvent();
        }     

        public void setBoardWithQBits()
        {
            selectWorldObject((int)WorldObject.EnvironmentObject.Board);
        }

        public void setBlochSphere()
        {
            selectWorldObject((int)WorldObject.EnvironmentObject.BlochSphere);
        }

    }
}

