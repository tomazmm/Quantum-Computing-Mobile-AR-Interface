import os
from flask_restful import Resource
from flask import request
from qiskit import QuantumCircuit, transpile
from qiskit.providers.aer import Aer


class CircuitIntermediate(Resource):
    def post(self):
        try:
            qasm_str = request.get_data().decode('utf-8')
            circ = QuantumCircuit.from_qasm_str(qasm_str)

            # circ = CircuitIntermediate._add_save_state_vectors(circ)

            circ.save_vectorstate()

            backend = Aer.get_backend(name="aer_simulator")

            # circ.measure_active()

            circ.draw("mpl", filename="image")

            # Transpile for simulator
            circ = transpile(circ, backend)

            # Run and get statevector
            result = backend.run(circ).result()

        except Exception as e:
            return e.args[0], 400

    @staticmethod
    def _add_save_state_vectors(circuit: QuantumCircuit) -> QuantumCircuit:
        print(*circuit.data, sep="\n")




        return circuit



