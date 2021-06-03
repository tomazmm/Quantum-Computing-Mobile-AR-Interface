from flask_restful import Resource
from flask import request

errors = {
    "CircuitDoesNotExist": {

    }
}

class Circuit(Resource):
    @staticmethod
    def get():
        algo = request.args.get("algorithm")
        if algo is None:
            return "Not Implemented: Return list of all algorithms.", 200
        return algo





####
# import functools
#
# from flask import (
#     Blueprint, request
# )
# from qiskit import QuantumCircuit, Aer, execute
#
# bp = Blueprint('circuit', __name__, url_prefix='/circuit')
#
# qasm_str = """OPENQASM 2.0;
# include "qelib1.inc";
# qreg q[2];
# creg c[2];
# h q[0];
# cx q[0],q[1];
# measure q -> c;
# """
#
#
#
#
#
# @bp.route('/', methods=["GET"])
# def circuit():
#     algo = request.args.get("algorithm")
#     if algo is None:
#         return "Not Implemented: Return list of all algorithms.", 500
#     return algo
#
#
# @bp.route('/run', methods=["GET"])
# def runQASMCircuit():
#     try:
#         qc = QuantumCircuit.from_qasm_str(qasm_str)
#         # If you want to read from file, use instead
#         # qc = QuantumCircuit.from_qasm_file("/path/to/file.qasm")
#
#         # You can choose other backend also.
#         backend = Aer.get_backend("qasm_simulator")
#
#         # Execute the circuit and show the result.
#         job = execute(qc, backend)
#         result = job.result()
#         return result.get_counts()
#     except Exception as e:
#         return e.args[0], 400

