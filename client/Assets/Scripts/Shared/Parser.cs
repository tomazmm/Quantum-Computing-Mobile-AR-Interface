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
        public static void parseThrough(string downloadHandeler)
        {           
            counts = JObject.Parse(downloadHandeler)["counts"];
            state_vectors = JObject.Parse(downloadHandeler)["state_vectors"];
            sorted_sv_keys = JObject.Parse(downloadHandeler)["sorted_sv_keys"];
        }

        public static string getState()
        {
            var x = 0;
            foreach(var it in sorted_sv_keys)
            {
                if(SharedStateSwitch.quState == x)
                {
                    var result = state_vectors.SelectToken(it.ToString());
                    return getNameOfGate(it.ToString()).ToString() + " " + result;
                }                    
                x++;
            }
            return null;
        }

        public static WorldObject.Gates getNameOfGate(string gate)
        {
            //TODO: add gates
            if (gate.Contains("h"))
                return WorldObject.Gates.Hgate;
            else if (gate.Contains("measure"))
                return WorldObject.Gates.Measurementgate;
            else if(gate.Contains("x"))
                return WorldObject.Gates.CNotgate;
            else
                return WorldObject.Gates.None;
        }
    }
}

