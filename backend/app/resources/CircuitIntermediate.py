import os
from flask_restful import Resource
from flask import request
from qiskit import QuantumCircuit, transpile
from qiskit.providers.aer import Aer
from qiskit.providers.aer.library import SaveStatevector
from qiskit.providers.aer.library.default_qubits import default_qubits


class CircuitIntermediate(Resource):
    def post(self):
        try:
            qasm_str = request.get_data().decode('utf-8')
            circ = QuantumCircuit.from_qasm_str(qasm_str)

            data_copy = circ.data.copy()
            qubits = default_qubits(circ)
            try:
                i = 0
                while data_copy:
                    circuit_element = data_copy[i]
                    if type(circuit_element[0]) is not SaveStatevector:
                        sv = SaveStatevector(len(qubits), label=f"sv-{i}")
                        data_copy.insert(i + 1, (sv, qubits, []))
                    i += 1
            except IndexError:
                circ.data = data_copy

            # Transpile for simulator
            simulator = Aer.get_backend('aer_simulator')
            circ = transpile(circ, simulator)

            # Run and get statevector
            result_data = simulator.run(circ).result().data()

            ret = {
                "count": result_data["counts"],
                "state_vectors": {}
            }
            for el in result_data:
                if el == "counts": continue
                ret["state_vectors"][el] = str(result_data[el])

            return ret

        except Exception as e:
            return e.args[0], 400
