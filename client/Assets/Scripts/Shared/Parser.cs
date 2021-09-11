using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class Parser
    {
        public static JToken counts { get; set; }
        public static JToken state_vectors { get; set; }
        public static JToken sorted_sv_keys { get; set; }
        private static List<string> lastInStateGates = new List<string>();
        private static int lastState = 0;
        public static void parseThrough(string downloadHandeler)
        {
            counts = JObject.Parse(downloadHandeler)["counts"];
            state_vectors = JObject.Parse(downloadHandeler)["state_vectors"];
            sorted_sv_keys = JObject.Parse(downloadHandeler)["sorted_sv_keys"];
            setAllGatesWithoutState();
        }

        private static void setAllGatesWithoutState()
        {
            foreach(Qbit _qbit in QbitsBoard.listOfQbits)
            {
                foreach(QbitArea _qbitArea in _qbit.areas)
                {
                    if(_qbitArea.qbitGate != null)
                    {
                        _qbitArea.qbitGate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/"+_qbitArea.qbitGate.name +"glow", typeof(Material)) as Material;
                        _qbitArea.usedinState = false;
                        if (_qbitArea.qbitGate.transform.childCount > 0)
                        {
                            foreach (Transform child in _qbitArea.qbitGate.transform)
                            {
                                if (child.name.Contains("Measurement"))
                                {
                                    child.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/MeasurementLineglow", typeof(Material)) as Material;
                                }
                                else
                                {
                                    child.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/Betweenlineglow", typeof(Material)) as Material;                                  
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void cleanList()
        {
            lastInStateGates.Clear();
            lastState = 0;
        }

        public static void setAllGatesWithState()
        {
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                foreach (QbitArea _qbitArea in _qbit.areas)
                {
                    if (_qbitArea.qbitGate != null)
                    {
                        if(_qbitArea.isConfirmed)
                            _qbitArea.qbitGate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + _qbitArea.qbitGate.name + "conf", typeof(Material)) as Material;
                        else
                            _qbitArea.qbitGate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + _qbitArea.qbitGate.name, typeof(Material)) as Material;
                        _qbitArea.usedinState = false;
                        if (_qbitArea.qbitGate.transform.childCount > 0)
                        {
                            foreach (Transform child in _qbitArea.qbitGate.transform)
                            {
                                if (child.name.Contains("Measurement"))
                                {
                                    child.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/MeasurementLine", typeof(Material)) as Material;
                                }
                                else
                                {
                                    child.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/Betweenline", typeof(Material)) as Material;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static string getState()
        {
            var x = 0;
            if (sorted_sv_keys == null)
                return null;
            foreach (var it in sorted_sv_keys)
            {
                if (SharedStateSwitch.quState == x)
                {
                    var result = state_vectors.SelectToken(it.ToString());
                    var gate = getNameOfGate(it.ToString()).ToString();
                    if (lastState > SharedStateSwitch.quState)
                        decreaseGateMaterial();
                    else
                        increaseGateMaterial(gate, it.ToString());
                    lastState = SharedStateSwitch.quState;
                    return gate + " " + result;
                }
                x++;
            }
            return null;
        }

        public static string showMeasurementResult()
        {
            string endResult = null;
            foreach(var it in counts)
            {
                endResult += it.ToString() + "  ";
            }
            return endResult;
        }

        public static void decreaseGateMaterial()
        {

            var last = lastInStateGates[lastInStateGates.Count - 1].Split('-');
            var lastQbit = int.Parse(last[0]);
            var lastArea = int.Parse(last[1]);
            lastInStateGates.RemoveAt(lastInStateGates.Count - 1);
            QbitsBoard.listOfQbits[lastQbit].areas[lastArea].usedinState = false;
            QbitsBoard.listOfQbits[lastQbit].areas[lastArea].qbitGate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + QbitsBoard.listOfQbits[lastQbit].areas[lastArea].qbitGate.name + "glow", typeof(Material)) as Material;
            if (QbitsBoard.listOfQbits[lastQbit].areas[lastArea].qbitGate.transform.childCount > 0)
            {
                foreach (Transform child in QbitsBoard.listOfQbits[lastQbit].areas[lastArea].qbitGate.transform)
                {
                    if (child.name.Contains("Measurement"))
                    {
                        child.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/MeasurementLineglow", typeof(Material)) as Material;
                    }
                    else
                    {
                        child.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/Betweenlineglow", typeof(Material)) as Material;
                    }
                }
            }
            if (QbitsBoard.listOfQbits[lastQbit].areas[lastArea].positionsOfConnectedQbits.Count != 0)
            {
                foreach (var it in QbitsBoard.listOfQbits[lastQbit].areas[lastArea].positionsOfConnectedQbits)
                {
                    QbitsBoard.listOfQbits[it.qbit].areas[it.area - 1].qbitGate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + QbitsBoard.listOfQbits[lastQbit].areas[lastArea].qbitGate.name + "addglow", typeof(Material)) as Material;
                }
            }            
        }

        public static void increaseGateMaterial(string gate, string resultGate)
        {
            int mainGatePosition = 0;
            if (resultGate != null)
            {
                var listOfGate = resultGate.Split('-');
                if (listOfGate.Length == 4)
                {
                    mainGatePosition = int.Parse(listOfGate[3]);
                }
                else if(listOfGate.Length == 5)
                {
                    mainGatePosition = int.Parse(listOfGate[4]);
                }
                else if (listOfGate.Length == 6)
                {
                    mainGatePosition = int.Parse(listOfGate[5]);
                }
            }
            var y = 0;
            foreach(QbitArea _qbitArea in QbitsBoard.listOfQbits[mainGatePosition].areas)
            {
                if(_qbitArea.qbitGate != null)
                {
                    if (_qbitArea.qbitGate.name.Contains(gate) && !_qbitArea.usedinState)
                    {
                        _qbitArea.usedinState = true;
                        lastInStateGates.Add(mainGatePosition.ToString() +"-" + y.ToString());
                        _qbitArea.qbitGate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + _qbitArea.qbitGate.name + "conf", typeof(Material)) as Material;
                        if (_qbitArea.qbitGate.transform.childCount > 0)
                        {
                            foreach (Transform child in _qbitArea.qbitGate.transform)
                            {
                                if (child.name.Contains("Measurement"))
                                {
                                    child.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/MeasurementLine", typeof(Material)) as Material;
                                }
                                else
                                {
                                    child.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/Betweenline", typeof(Material)) as Material;
                                }
                            }
                        }
                        if (_qbitArea.positionsOfConnectedQbits.Count != 0)
                        {
                            foreach (var it in _qbitArea.positionsOfConnectedQbits)
                            {
                                QbitsBoard.listOfQbits[it.qbit].areas[it.area - 1].qbitGate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + _qbitArea.qbitGate.name + "addconf", typeof(Material)) as Material;
                            }
                        }
                        break;
                    }
                }
                y++;
            }
        }

        public static WorldObject.Gates getNameOfGate(string gate)
        {
            //TODO: add gates
            if (gate.Contains("h"))
                return WorldObject.Gates.Hgate;
            else if (gate.Contains("measure"))
                return WorldObject.Gates.Measurementgate;
            else if (gate.Contains("cx"))
                return WorldObject.Gates.CNotgate;
            else if (gate.Contains("x"))
                return WorldObject.Gates.Notgate;
            else
                return WorldObject.Gates.None;
        }
    }
}

