import os
from flask_restful import Resource
from flask import request
from qiskit import QuantumCircuit, Aer, execute


class Circuit(Resource):
    def post(self):
        try:
            qasm_str = request.get_data().decode('utf-8')

            qc = QuantumCircuit.from_qasm_str(qasm_str)
            backend = Aer.get_backend(name="aer_simulator")
            job = execute(qc, backend)

            return job.result().get_counts()
        except Exception as e:
            return e.args[0], 400
