using System.Linq;
using UnityEngine;

namespace QuantomCOMPPC
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
            for (var i = 0; i < QbitsBoard.listOfQbits.First().areas.Count; i++) 
            {
                for (var j = 0; j < QbitsBoard.listOfQbits.Count; j++)
                {
                    var gate = QbitsBoard.listOfQbits[j].areas[i];
                    
                    if (!gate.qbitGate) continue;
                    
                    if (gate.qbitGate.name == WorldObject.Gates.Hgate.ToString())
                    {
                        qasm += $"h q[{j}];\n";
                    }
                    else if (gate.qbitGate.name == WorldObject.Gates.Notgate.ToString())
                    {
                        qasm += $"x q[{j}];\n";
                    }
                    else if (gate.qbitGate.name == WorldObject.Gates.CNotgate.ToString())
                    {
                        if(gate.isMainArea)
                        {
                            qasm += $"cx q[{gate.positionsOfConnectedQbits.First().qbit}],q[{j}];\n";
                        }
                    }
                    else if (gate.qbitGate.name == WorldObject.Gates.Measurementgate.ToString())
                    {
                        qasm += $"measure q[{j}] -> c[{j}];\n";
                    }
                }
            }
            
            return qasm;
        }
        
        
    }
}