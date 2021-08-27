import os
from flask_restful import Resource
from flask import request
from qiskit import QuantumCircuit, Aer, transpile
from qiskit.providers.aer.library import SaveStatevector
from qiskit.providers.aer.library.default_qubits import default_qubits


class Circuit(Resource):

    def post(self):
        try:
            qasm_str = request.get_data().decode('utf-8')
            circ = QuantumCircuit.from_qasm_str(qasm_str)

            # Add SaveStateVector after each gate for intermediate results
            i = 0
            qubits = default_qubits(circ)
            while len(circ.data) != i:
                circuit_element = circ.data[i]
                if type(circuit_element[0]) is not SaveStatevector:
                    # create sv label
                    label = f"sv-{i}-{circuit_element[0].name}"
                    for qbit in circuit_element[1]:
                        label += f"-{qbit.index}"
                    # add sv to circuit
                    sv = SaveStatevector(len(qubits), label=label)
                    circ.data.insert(i + 1, (sv, qubits, []))
                i += 1

            # Transpile for  and run circuit
            simulator = Aer.get_backend('aer_simulator')
            circ = transpile(circ, simulator)
            result_data = simulator.run(circ).result().data()

            ret = {
                "counts": result_data["counts"],
                "state_vectors": {}
            }
            for el in result_data:
                if el == "counts": continue
                ret["state_vectors"][el] = str(result_data[el])

            return ret
        except Exception as e:
            return e.args[0], 400






