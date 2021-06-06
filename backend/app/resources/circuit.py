import os
from flask_restful import Resource
from flask import request

errors = {
    "CircuitDoesNotExist": {

    }
}


class Circuit(Resource):
    algos_path = os.getcwd() + "/app/common/algorithms/"

    def get(self):
        algo = request.args.get("algorithm")
        if algo is None:
            return os.listdir(self.algos_path), 200
        else:
            algo += ".qasm"
            for file in os.listdir(self.algos_path):
                if file == algo:
                    # return send_file(self.algos_path + file, as_attachment=True)
                    return {
                        "status": 200,
                        "qasm": open(self.algos_path + file, "r").read()
                    }

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
