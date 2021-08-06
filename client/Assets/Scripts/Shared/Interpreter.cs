using System.Linq;
using UnityEngine;

namespace QuantomCOMP
{
    public static class Interpreter
    {
        private const string QasmProlog = "OPENQASM 2.0; \ninclude \"qelib1.inc\"; \n \n";
        /*
         * Function translates the Board Circuit into the OpenQASM 2.0 Code.
         * https://en.wikipedia.org/wiki/OpenQASM
         */
        public static string boardToQasm()
        {
            // Adds the necessary prolog code to the QASM file/string
            var qasm = QasmProlog;
            qasm += $"qreg q[{QbitsBoard.listOfQbits.Count}];\n";
            qasm += $"creg c[{QbitsBoard.listOfQbits.Count}];\n \n";
            
            // Iterate the gates on the board and translates it to the corresponding QASM code
            for (int i = 0; i < QbitsBoard.listOfQbits.Count; i++)
            {
                var qbit = QbitsBoard.listOfQbits[i];
                foreach (var gate in qbit.areas)
                {
                    if (!gate.qbitGate) break;
                    
                    if (gate.qbitGate.name == WorldObject.Gates.Hgate.ToString())
                    {
                        qasm += $"h q[{i}];";
                    }
                    else if (gate.qbitGate.name == WorldObject.Gates.Notgate.ToString())
                    {
                        qasm += $"x q[{i}];";
                    }
                }   
            }
            
            return qasm;
        }
        
        
    }
}