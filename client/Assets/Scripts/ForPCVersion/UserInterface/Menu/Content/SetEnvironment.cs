using UnityEngine;

namespace QuantomCOMPPC
{
    public class SetEnvironment : Content
    {

        private void Start()
        {
            objectName = "SetEnvironment";
            nameOfSection = Section.Environment;
            subscribeToSectionEvent();
            SharedStateSwitch.enableDisableContent(false);
        }     

        public void setBoardWithQBits()
        {
            selectWorldObject(WorldObject.EnvironmentObject.Board);
        }

        public void setBlochSphere()
        {
            selectWorldObject(WorldObject.EnvironmentObject.BlochSphere);
        }

        public void setProbabilitiesGraph()
        {
            selectWorldObject(WorldObject.EnvironmentObject.ProbabilitiesGraph);
        }

    }
}

