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
            foreach(var it in state_vectors)
            {
                if(SharedStateSwitch.quState == x)
                    return it.ToString();
                x++;
            }
            return null;
        }
    }
}

